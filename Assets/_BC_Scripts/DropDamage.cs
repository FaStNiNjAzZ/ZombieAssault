using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HighDamage(other));
        }
    }
    private IEnumerator HighDamage(Collider player)
    {
        pc_stats pc_Stats = player.GetComponent<pc_stats>();
        if(pc_Stats != null ) 
        {
            Destroy(this.gameObject);
            float damage = pc_Stats.GetDamage();
            pc_Stats.SetDamage(damage * 10);
            yield return new WaitForSeconds(30);
            pc_Stats.SetDamage(damage);
            
        }

        
    }
}
