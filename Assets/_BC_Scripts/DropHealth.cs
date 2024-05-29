using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pc_stats pc_Stats = other.GetComponent<pc_stats>();
            if (pc_Stats != null) 
            {
                pc_Stats.SetHealth(pc_Stats.GetMaxHealth());
                Destroy(this.gameObject);
            }
        }
    }
}
