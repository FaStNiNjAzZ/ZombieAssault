using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BC_Zombie_Attack : MonoBehaviour
{
    public float fl_Zombie_Damage;
    public float fl_Zombie_Attack_Current_Cooldown;
    public float fl_Zombie_Attack_Cooldown;

    void Start()
    {

        fl_Zombie_Attack_Current_Cooldown = fl_Zombie_Attack_Cooldown;
    }

    void OnTriggerStay(Collider PC_Collider)
    {
        if(PC_Collider.gameObject.tag == "Player")
        {
            fl_Zombie_Attack_Current_Cooldown -= 1 * Time.deltaTime;
            if (fl_Zombie_Attack_Current_Cooldown <= 0)
            {
                fl_Zombie_Attack_Current_Cooldown = fl_Zombie_Attack_Cooldown;
                PC_Collider.gameObject.SendMessage("Damage", fl_Zombie_Damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}

