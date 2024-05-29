using UnityEngine;
using UnityEngine.UI;

public class BC_Modular_Weapon_Script : MonoBehaviour
{

    [Header("Main Stats")]
    [SerializeField] private bool is_automatic = false;
    [SerializeField] private bool can_fire = true;
    [SerializeField] private bool is_reloading = false;

    public string str_gun_name;
    public Text gun_name;
    public Text Magazine;

    [Header("RPM")]
    [SerializeField] private float rate_of_fire;
    [SerializeField] private float current_rpm;

    [Header("Reloading")]
    [SerializeField] private float reload_speed;
    [SerializeField] private float current_reloading;

    [Header("Bullet Stats")]
    [SerializeField] private float projectile_velocity;
    [SerializeField] private int projectile_amount;
    [SerializeField] private float projectile_damage;

    [Header("Magazine")]
    [SerializeField] private int magazine_capacity;
    [SerializeField] private int current_magazine_size;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform muzzle_pos;
    [SerializeField] private GameObject bullet;
    [SerializeField] Animator animator;
    [SerializeField] private Transform tf_weapon;

    void Start()
    {
        tf_weapon= GetComponent<Transform>();
        animator = GetComponent<Animator>();
        // set aniamtion speed to the speed of the reload

        gun_name = GameObject.Find("GunName").GetComponent<Text>();
        Magazine = GameObject.Find("Magazine").GetComponent<Text>();
    }


    void Update()
    {
        UpdateGUI();
        AimingManager();
        // check if LMB is pressed and magazine size is greater than 0
        if (Input.GetMouseButton(0) && current_magazine_size > 0 && can_fire && is_automatic)
        {
                // controls the speed of the gun firing with rate of fire
                current_rpm -= Time.deltaTime;
            if (current_rpm <= 0)
            {
                Fire();
                current_rpm = rate_of_fire;
            }

        }
        if(Input.GetMouseButtonDown(0) && current_magazine_size > 0 && can_fire && !is_automatic)
        {
            Fire();
        }
        // if there is no bullets in the magazine and player pressed LMB the gun will reload
        if(Input.GetMouseButton(0) && current_magazine_size <= 0)
        {
            is_reloading = true; 
        }
        // if the player pressed r and the current magazine size is not the same as the magazine max capacity the gun will reload
        if (Input.GetKey(KeyCode.R) && current_magazine_size != magazine_capacity)
        {
            is_reloading = true;
        }

        
        if (is_reloading) Reload();
    }

    private void AimingManager()
    {
        if(!is_reloading)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //play aim in animation
                animator.SetBool("isAiming", true);
            }
            if (Input.GetMouseButtonUp(1))
            {
                //play aim out animation
                animator.SetBool("isAiming", false);
            }
        }
        
    }

    private void Fire()
    {
        //create bullet
        // player gun fire sound
        Recoil();

        GameObject newBullet = Instantiate(bullet, muzzle_pos.position, muzzle_pos.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = muzzle_pos.forward * projectile_velocity;
        newBullet.GetComponent<BC_bullet>().damage = projectile_damage;

        //reduce magazine size
        current_magazine_size--;
    }
    private void Recoil()
    {
        animator.SetFloat("RPM", 1 / rate_of_fire);
        animator.SetTrigger("FiredWeapon");
    }

    private void Reload()
    {
        can_fire = false;
        // play reload animation and sound
        animator.SetFloat("ReloadSpeed", 1f / reload_speed);
        animator.SetTrigger("Reload");

        current_reloading -= Time.deltaTime;
        if (current_reloading <= 0)
        {
            current_reloading = reload_speed;
            current_magazine_size = magazine_capacity;
            can_fire = true;
            animator.SetTrigger("Reload");
            is_reloading = false;
        }
    }

    private void UpdateGUI()
    {
        gun_name.text = str_gun_name;
        Magazine.text = current_magazine_size.ToString() + " / " + magazine_capacity.ToString();
    }
}
