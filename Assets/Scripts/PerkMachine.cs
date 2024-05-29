using UnityEngine;

public class PerkMachine : MonoBehaviour
{
    public enum PerkType { Juganog, SpeedCola, DoubleTap, StaminaUp };
    public PerkType perkType;
    public int cost;

    public AudioClip pickupSound; // Sound to play when picking up the perk - we can choose if we do this
    public AudioClip failedPickupSound; // Sound to play when picking up the perk fails - we can choose if we do this

    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            //Display interaction prompt to the player
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            //Hide interaction prompt
        }
    }

    private void Update()
    {
        // Check for player input to interact with the machine
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        pc_stats playerStats = FindObjectOfType<pc_stats>();
        if (playerStats.GetCurrency() >= cost) // if player has enough currency
        {
            playerStats.AddCurrency(-cost); // deduct cost
            switch (perkType)
            {
                case PerkType.Juganog:
                    ApplyJuganog();
                    break;
                case PerkType.SpeedCola:
                    ApplySpeedCola();
                    break;
                case PerkType.DoubleTap:
                    ApplyDoubleTap();
                    break;
                case PerkType.StaminaUp:
                    ApplyStaminaUp();
                    break;
                default:
                    Debug.LogError("Invalid perk type!"); // i added debugs because tracking this was annoying
                    break;
            }

            // Play pickup sound
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
        }
        else
        {
            // Play failed pickup sound
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(failedPickupSound, transform.position);
            }
        }
        

        //Update UI to show perk acquired - we need to add the ui here or the update function
        //GetComponent<Collider>().enabled = false;
        //GetComponent<Renderer>().enabled = false;
    }

    private void ApplyJuganog()
    {
        // Apply Juganog effect (increase player's maximum health)
        pc_stats playerStats = FindObjectOfType<pc_stats>(); // Assuming you have a script for player stats
        if (playerStats != null)
        {
            playerStats.SetHealth(+100);
        }
        else
        {
            Debug.LogError("PlayerStats script not found!");
        }
    }

    private void ApplySpeedCola()
    {
        // Apply Speed Cola effect (speed up player's reload time)
        pc_stats playerController = FindObjectOfType<pc_stats>(); // Assuming you have a script for player controller
        if (playerController != null)
        {
            playerController.SetWalkSpeed(+1);
            playerController.SetRunSpeed(+2);
        }
        else
        {
            Debug.LogError("PlayerController script not found!");
        }
    }

    private void ApplyDoubleTap()
    {
        // Apply Double Tap effect (increase player's rate of fire)
        pc_stats weaponController = FindObjectOfType<pc_stats>(); // Assuming you have a script for weapon controller
        if (weaponController != null)
        {
            weaponController.SetRateOfFire(+0.2f);
        }
        else
        {
            Debug.LogError("WeaponController script not found!");
        }
    }

    private void ApplyStaminaUp()
    {
        // Apply Stamina Up effect (increase player's sprint duration and speed)
        pc_stats playerController = FindObjectOfType<pc_stats>(); // Assuming you have a script for player controller
        if (playerController != null)
        {
            playerController.SetStamina(+100);
        }
        else
        {
            Debug.LogError("PlayerController script not found!");
        }
    }
}