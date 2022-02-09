using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsBehaviour : MonoBehaviour
{

    public enum ItemType {Ammo, Health}
    public ItemType item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){

            if(item == ItemType.Ammo){
                AddAmmoPlayer(other.GetComponent<WeaponScript>());
            }else if(item == ItemType.Health){
                AddLifePlayer(other.GetComponent<PlayerScript>());
            }
        }
    }

    private void AddAmmoPlayer(WeaponScript ws){
        ws.addAmmo(3);
    }

    private void AddLifePlayer(PlayerScript ps){
        if(ps.currentHealth < ps.maxHealth){
            ps.addLife(1);
        }
    }
}
