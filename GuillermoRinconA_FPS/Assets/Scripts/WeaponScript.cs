using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    PoolObjectController bulletPool;
    public Transform firePoint;

    public CanvasController canvas;

    private bool isPlayer;

    public int maxAmmo;
    public int currentAmmo;

    public float bulletVelocity;
    public bool infiniteAmmo;
    public float fireCad;
    public float fireVelocity;
    private float shootReload = 0f;

    private void Start() {
       
       infiniteAmmo = false;
       
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
                Reload();
            }
        }

    }

    private void Fire(){
        GameObject newBullet = bulletPool.getObject();
        newBullet.transform.position = firePoint.position;
        newBullet.transform.rotation = firePoint.rotation;

        newBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletVelocity;
        
    }

    private void Reload(){
        currentAmmo = maxAmmo;
        canvas.setCurrentAmmo(currentAmmo);
    }
}
