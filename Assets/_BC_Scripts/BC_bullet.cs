using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BC_bullet : MonoBehaviour
{
    [SerializeField] private float timer;
    public float damage;

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            BC_Zombie_Stats zombieScript = collision.gameObject.GetComponent<BC_Zombie_Stats>();
            zombieScript.Damage(damage);
        }
        Destroy(this.gameObject);
    }
}
