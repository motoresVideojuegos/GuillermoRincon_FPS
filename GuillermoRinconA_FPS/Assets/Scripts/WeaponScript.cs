using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public int maxAmmo;
    public int currentAmmo;

    public bool infiniteAmmo;
    public float fireCad;
    private float shootReload = 0f;

    private void Start() {
       
       infiniteAmmo = false;
       
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
                }else{
                    Reload();
                    Fire();
                    shootReload = 0;
                }
                
            }

            shootReload += 1 * Time.deltaTime;
            
        }

        if(Input.GetKeyDown(KeyCode.R)){
            Reload();
        }
    }

    private void Fire(){
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * 30;
    }

    private void Reload(){
        currentAmmo = maxAmmo;
    }
}
