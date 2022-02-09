using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    PoolObjectController bulletPool;
    public Transform firePoint;

    public CanvasController canvas;

    private bool isPlayer;

    public int maxCurrentAmmo;
    public int currentAmmo;

    public int maxBullets;

    public int maxWeaponAmmo;

    public float bulletVelocity;
    public bool infiniteAmmo;
    public float fireCad;
    public float fireVelocity;
    private float shootReload = 0f;

    private void Start() {
       
       infiniteAmmo = false;
       if(GetComponentInParent<PlayerScript>()){
           canvas.setMaxAmmo(maxCurrentAmmo);
       }
       
    }
    
    private void Awake() {
        bulletPool = GetComponent<PoolObjectController>();

        isPlayer = false;
        if(GetComponent<PlayerScript>()){
            isPlayer = true;
            canvas.setCurrentAmmo(currentAmmo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && currentAmmo > 0){

            if (shootReload >= fireCad){
                
                if(infiniteAmmo == false){
                    Fire();
                    --currentAmmo;
                    shootReload = 0;

                    if(isPlayer == true){
                        canvas.setCurrentAmmo(currentAmmo);
                    }
                }else{
                    Reload();
                    Fire();
                    shootReload = 0;
                }
                
            }

            shootReload += fireVelocity * Time.deltaTime;
            
        }

        if(isPlayer == true){
            if(Input.GetKeyDown(KeyCode.R)){
                if(currentAmmo != maxWeaponAmmo){
                    Reload();
                }
                
            }
        }

    }

    public void Fire(){
        GameObject newBullet = bulletPool.getObject();
        newBullet.transform.position = firePoint.position;
        newBullet.transform.rotation = firePoint.rotation;

        newBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletVelocity;
        
    }

    public void Reload(){

        int ammoDiff = maxWeaponAmmo - currentAmmo;

        //Calcular si se necesitan más balas de las disponibles para la recarga
        //Si no hay sificientes balas se suma las balas restantes a la municion actual
        if(maxCurrentAmmo - ammoDiff < 0){
            currentAmmo += maxCurrentAmmo;
            maxCurrentAmmo = 0;
        }else{
            maxCurrentAmmo -= ammoDiff;
            currentAmmo += ammoDiff;
        }

        canvas.setCurrentAmmo(currentAmmo);
        canvas.setMaxAmmo(maxCurrentAmmo);
    }

    public void addAmmo(int ammount){
        maxCurrentAmmo = Mathf.Clamp(maxCurrentAmmo + ammount, 0, maxBullets);
        canvas.setMaxAmmo(maxCurrentAmmo);
    }
}
