using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpeed : MonoBehaviour
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
        if (pc_Stats != null)
        {
            Destroy(this.gameObject);
            float walkSpeed = pc_Stats.GetWalkSpeed();
            float runSpeed = pc_Stats.GetRunSpeed();
            pc_Stats.SetWalkSpeed(walkSpeed * 1.2f);
            pc_Stats.SetRunSpeed(runSpeed * 1.2f);
            yield return new WaitForSeconds(30);
            pc_Stats.SetWalkSpeed(walkSpeed);
            pc_Stats.SetRunSpeed(runSpeed);
            
        }


    }
}
