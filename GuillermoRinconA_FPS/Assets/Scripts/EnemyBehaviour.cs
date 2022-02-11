using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent nav;
    public Transform player;
    public WeaponScript weaponController;

    public int maxHealth;
    private int currentHealth;
    public int exp;
    public float velocityEnemy;
    public float range;
    public float rangeShoot;

    public int points;
    public float fireVelocity;
    public float fireCad;
    private float shootReload = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = velocityEnemy;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement(){

        if(Vector3.Distance( player.position, transform.position) < range && Vector3.Distance( player.position, transform.position) > rangeShoot){
            nav.isStopped = false;
            nav.SetDestination(player.transform.position);

        }else if(Vector3.Distance( player.position, transform.position) < rangeShoot){
            nav.isStopped = true;
            transform.LookAt(player);
            range = 100f;
            ShootPlayer();
        }else{
            nav.isStopped = true;
        }
    }

    private void ShootPlayer(){
        if (shootReload >= fireCad){
                this.weaponController.Fire();
                shootReload = 0;
        }
        
        shootReload += fireVelocity * Time.deltaTime;
    }

    public void RemoveLife(int dmgTaken){
        currentHealth -= dmgTaken;
        
        if(currentHealth <= 0){
            player.GetComponent<PlayerScript>().addPoints(points);
            Destroy(gameObject);
        }
    }
}
