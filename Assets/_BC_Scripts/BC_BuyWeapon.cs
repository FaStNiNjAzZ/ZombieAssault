using UnityEngine;
using UnityEngine.UI;

public class BC_BuyWeapon : MonoBehaviour
{
    public GameObject newWeaponPrefab; // Prefab of the new weapon

    public BC_Weapon_Manager weaponManager;

    public Text text;
    public int int_cost;

    private void Start()
    {
        text.text = "Press F to buy for " + int_cost.ToString();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && collider.GetComponent<pc_stats>().GetCurrency() >= int_cost)
            {
                collider.GetComponent<pc_stats>().AddCurrency(-int_cost);

                // Destroy all child GameObjects of GunPosition
                DestroyChildrenOfGunPosition();

                // Instantiate and place the new weapon prefab as a child of GunPosition
                InstantiateNewWeapon();


                text.gameObject.SetActive(false);
            }
        }

    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player")) text.gameObject.SetActive(false);
    }




    void DestroyChildrenOfGunPosition()
    {
        Destroy(weaponManager.weapons[weaponManager.GetCurrentWeaponIndex()]);
    }

    void InstantiateNewWeapon()
    {
        // Check if newWeaponPrefab is assigned
        if (newWeaponPrefab != null && weaponManager.gunPositons[weaponManager.GetCurrentWeaponIndex()] != null)
        {
            // Instantiate the new weapon prefab as a child of GunPosition
            GameObject newWeapon = Instantiate(newWeaponPrefab, weaponManager.gunPositons[weaponManager.GetCurrentWeaponIndex()].position, weaponManager.gunPositons[weaponManager.GetCurrentWeaponIndex()].rotation, weaponManager.gunPositons[weaponManager.GetCurrentWeaponIndex()]);

            // Assign the instantiated weapon to the weapons array in the weaponManager
            weaponManager.weapons[weaponManager.GetCurrentWeaponIndex()] = newWeapon;

            newWeapon.GetComponent<BC_Modular_Weapon_Script_v2>().gunHolder = weaponManager.gunPositons[weaponManager.GetCurrentWeaponIndex()];

        }
        else
        {
            Debug.LogWarning("NewWeaponPrefab or GunPosition is not assigned.");
        }
    }
}
