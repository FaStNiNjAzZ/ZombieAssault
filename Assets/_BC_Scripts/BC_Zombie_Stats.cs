using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BC_Zombie_Stats : MonoBehaviour
{
    public BC_ZombieManager manager;

    NavMeshAgent agent;
    GameObject PC;

    public float fl_Zombie_Health = 100;
    public float fl_Zombie_Speed = 1;
    public int int_zombie_score = 100;

    public int int_zombie_damage = 10;
    public float zombieAttackSpeed;

    public Animator animator;


    void Start()
    {
        PC = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = fl_Zombie_Speed;
        SetSpeed();
        manager = FindObjectOfType<BC_ZombieManager>().GetComponent<BC_ZombieManager>();


        animator.SetFloat("AttackSpeed", zombieAttackSpeed);
    }
    public void Damage(float fl_Damage)
    {
        fl_Zombie_Health -= fl_Damage;
        StartCoroutine(Hit());
        pc_stats PC_script = PC.GetComponent<pc_stats>();
        PC_script.AddCurrency(10);

        if (fl_Zombie_Health <= 0)
        {

            manager.alive_zombies.Remove(this.gameObject);
            Destroy(this.gameObject);
            if (manager.alive_zombies.Count == 0) manager.StartRound();
            PC_script.AddCurrency(50);
            int randomDrop = Random.Range(0, 100);
            if (randomDrop > 95) 
            {
                GameObject drop = manager.ZombieDrops[Random.Range(0, manager.ZombieDrops.Length)];
                Instantiate(drop, this.transform.position, Quaternion.identity, manager.transform);
            }
        }
    }
    void FixedUpdate()
    {
        // could add function to change the rate of this depending on the distance to the player e.g if the zombie is far away from the player the zombie will update setdestination slower for performance
        agent.SetDestination(PC.transform.position);
    }

    private void SetSpeed() 
    {
        animator.SetFloat("MovementSpeed", fl_Zombie_Speed);
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        agent.speed = 0;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(zombieAttackSpeed);
        Vector3 v3_offset = transform.position - PC.transform.position;
        if (v3_offset.magnitude <= 2f)
        {
            //pc in range, deal damage
            PC.GetComponent<pc_stats>().SetHealth(-int_zombie_damage);
            StartCoroutine(Attack());
        }
        else
        {
            agent.speed = fl_Zombie_Speed;
            StopCoroutine(Attack());
        }
    }

    private IEnumerator Hit()
    {
        agent.speed = 0;
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(1);
        agent.speed = fl_Zombie_Speed;
    }


}
