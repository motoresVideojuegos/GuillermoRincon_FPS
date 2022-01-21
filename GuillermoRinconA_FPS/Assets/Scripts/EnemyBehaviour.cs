using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent nav;
    public Transform player;
    public int maxHealth;
    public int currentHealth;
    public int exp;
    public float velocityEnemy;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.transform.position);
    }
}
