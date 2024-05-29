
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pc_stats : MonoBehaviour
{
    [Header("Main Stats")]
    [SerializeField] private float fl_health;
    [SerializeField] private float fl_max_health;

    [SerializeField] private float fl_stamina;
    [SerializeField] private float fl_max_stamina;
    [SerializeField] private float fl_fire_rate_modifier = 1;
    [SerializeField] private bool bl_cool_down_stamina = false;
    [SerializeField] private float cooldownTimer;
    [SerializeField] private float current_cooldownTimer;

    [SerializeField] private float fl_damage;

    [Header("Movement")]
    [SerializeField] private float fl_walk_speed;
    [SerializeField] private float fl_run_speed;
    [SerializeField] private float fl_crouch_speed;
    public bool canSprint = true;
    public bool isSprinting = false;

    
    [Header("Currency")]
    [SerializeField] public string str_currency;
    [SerializeField] private int int_player_currency;
    public Text score;
    public Text health;

    private void Start()
    {
        current_cooldownTimer = cooldownTimer;
        health.text = "Health: " + fl_health;
    }

    private void FixedUpdate()
    {
        UpdateCooldown();
    }

    public float GetHealth()
    {
        return fl_health;
    }
    public void SetHealth(float newHealth)
    {
        fl_health += newHealth;
        fl_health = Mathf.Clamp(fl_health, 0, fl_max_health);
        // Check if the player has less than 0 health if so kill the player
        health.text =  "Health: " + fl_health;
        if(fl_health <= 0)
        {
            KillPlayer();
        }
    }

    public float GetMaxHealth()
    {
        return fl_max_health;
    }
    public void SetMaxHealth(float newMaxHealth)
    {
        fl_max_health += newMaxHealth;
    }

    public bool GetRegenStamina()
    {
        return bl_cool_down_stamina;
    }
    public void SetRegenStamina(bool newCoolDownStamina)
    {
        bl_cool_down_stamina = newCoolDownStamina;
    }

    public float GetStamina()
    {
        return fl_stamina;
    }
    public void SetStamina(float newStamina)
    {
        fl_stamina = newStamina;
        fl_stamina = Mathf.Clamp(fl_stamina, 0, fl_max_stamina);

        if(fl_stamina < 1)
        {
            bl_cool_down_stamina = true;
        }
    }
    public float GetMaxStamina()
    {
        return fl_max_stamina;
    }
    public void SetMaxStamina(float newMaxStamina)
    {
        fl_max_stamina = newMaxStamina;
    }

    public float GetWalkSpeed()
    {
        return fl_walk_speed;
    }
    public void SetWalkSpeed(float newSpeed)
    {
        fl_walk_speed = newSpeed;
    }
    public float GetRunSpeed()
    {
        return fl_run_speed;
    }
    public void SetRunSpeed(float newRunSpeed)
    {
        fl_run_speed = newRunSpeed;
    }
    void UpdateCooldown()
    {
        if (bl_cool_down_stamina)
        {
            current_cooldownTimer -= Time.deltaTime;

            if (current_cooldownTimer <= 0f)
            { 
                bl_cool_down_stamina = false;
                current_cooldownTimer = cooldownTimer;
            }
        }
    }

    public float GetCrouchSpeed()
    {
        return fl_crouch_speed;
    }
    public void SetCrouchSpeed(float newCrouchSpeed)
    {
        fl_crouch_speed = newCrouchSpeed;
    }

    public float GetRateOfFire()
    {
        return fl_fire_rate_modifier;
    }

    public void SetRateOfFire(float newRateOfFire)
    {
        fl_fire_rate_modifier += newRateOfFire;
    }

    public int GetCurrency()
    {
        return int_player_currency;
    }
    public void AddCurrency(int newCurrency)
    {
        int_player_currency += newCurrency;
        score.text = "Score: " + int_player_currency;
    }

    public float GetDamage()
    {
        return fl_damage;
    }

    public void SetDamage(float newDamage) 
    {
        fl_damage += newDamage;
    }
    private void KillPlayer()
    {
        // needs to be worked on
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadSceneAsync(0);
    }
}