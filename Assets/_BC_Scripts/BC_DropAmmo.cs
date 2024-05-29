using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BC_DropAmmo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            BC_Weapon_Manager weaponManager = other.GetComponent<BC_Weapon_Manager>();

            if(weaponManager != null ) 
            {
                foreach(GameObject weapon in weaponManager.weapons) 
                {
                    BC_Modular_Weapon_Script_v2 weaponScript = weapon.GetComponent<BC_Modular_Weapon_Script_v2>();
                    weaponScript.current_ammo = weaponScript.max_ammo;
                    Destroy(this.gameObject);

                }
                
            }
        }
    }
}
