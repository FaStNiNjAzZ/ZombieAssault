using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BC_Weapon_Manager : MonoBehaviour
{
    public GameObject[] weapons; // Array of weapon GameObjects
    private int currentWeaponIndex = 0; // Index of the currently equipped weapon

    public Transform[] gunPositons;


    void Start()
    {
        // Ensure only the first weapon is initially active
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }

    }

    void Update()
    {
        // Check for weapon switching input
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeaponIndex != 0)
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeaponIndex != 1 && weapons.Length > 1)
        {
            SwitchWeapon(1);
        }
        // Add more key checks for additional weapons as needed
    }

    void SwitchWeapon(int newIndex)
    {
        weapons[currentWeaponIndex].SetActive(false);
        // Activate the new weapon
        weapons[newIndex].SetActive(true);

        // Call the UpdateAnimation method if it exists on the new weapon
        BC_Modular_Weapon_Script_v2 newWeaponScript = weapons[newIndex].GetComponent<BC_Modular_Weapon_Script_v2>();
        if (newWeaponScript != null)
        {
            newWeaponScript.UpdateAnimation();

        }

        // Update the current weapon index
        currentWeaponIndex = newIndex;
    }

    public int GetCurrentWeaponIndex()
    {
        return currentWeaponIndex;
    }
}
