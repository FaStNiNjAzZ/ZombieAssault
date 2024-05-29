using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BC_Modular_Weapon_Script_v2 : MonoBehaviour
{

    [Header("Weapon Stats")]
    public string str_weapon_name;
    public float damage;

    public int max_ammo;
    public int current_ammo;
    public int magazine_capacity;
    private int current_magazine_size;

    public float fire_rate;
    private float next_fire_time;

    public float reload_speed;
    private float next_reload_speed;

    public int projectile_amount;

    public float aimInSpeed;
    public float running_speed;
    public float equip_speed;

    public bool is_automatic;

    public bool can_fire = true;
    public bool is_reloading = false;
    private bool is_aiming = false;
    public bool can_switch = false;

    [Header("Recoil Controller")]
    public float recoil_x;
    public float recoil_y;
    public float recoil_z;

    public float recoil_aim_x;
    public float recoil_aim_y;
    public float recoil_aim_z;

    public float recoil_smoothness;
    public float recoil_return_speed;

    public float gun_recoil;
    public float maxRecoilDistance;

    private Vector3 currentRecoilRotation;
    private Vector3 targetRecoilRotation;

    private Vector3 initialPosition; // Initial position of the weapon


    [Header("Parts")]
    public pc_stats stats;
    private Rigidbody rb;
    [SerializeField] private Transform muzzle_pos;
    public Animator animator;
    private Transform tf_weapon;
    public Transform gunHolder;
    public Transform camera;
    public GameObject gunCasing;

    [Header("Particles")]
    public TrailRenderer bulletTrailPrefab;
    public float bulletSpeed = 100f;
    public ParticleSystem ImpactParticleSystem;
    public ParticleSystem MuzzleFlash;

    private Text gun_name;
    private Text Magazine;

    private void Start()
    {
        tf_weapon = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        camera = GameObject.Find("CameraHolder").transform;

        UpdateAnimation();

        gun_name = GameObject.Find("GunName").GetComponent<Text>();
        Magazine = GameObject.Find("Magazine").GetComponent<Text>();

        stats = this.GetComponentInParent<pc_stats>();

        current_ammo = max_ammo;
        current_magazine_size = magazine_capacity;
        next_reload_speed = reload_speed;

        initialPosition = gunHolder.localPosition;
    }

    private void Update()
    {
        UpdateGUI();
        AimingManager();
        HandleFiring();
        UpdateGunPosition();
        HandleAnimation();
    }

    private void HandleFiring()
    {
        if (Input.GetMouseButton(0) && current_magazine_size > 0 && can_fire && is_automatic && !stats.isSprinting)
        {

            next_fire_time -= Time.deltaTime;
            if (next_fire_time <= 0)
            {
                Fire();
                next_fire_time = fire_rate / stats.GetRateOfFire();
            }

        }
        if (Input.GetMouseButtonDown(0) && current_magazine_size > 0 && can_fire && !is_automatic)
        {
            Fire();
        }

        if (Input.GetMouseButton(0) && current_magazine_size <= 0 && !stats.isSprinting)
        {
            next_reload_speed = reload_speed;
            Reload();
        }

        if (Input.GetKey(KeyCode.R) && current_magazine_size != magazine_capacity && !stats.isSprinting)
        {
            next_reload_speed = reload_speed;
            Reload();
        }


        if (is_reloading && current_ammo > 0) Reload();
    }

    private void AimingManager()
    {
        if (!is_reloading)
        {
            if (Input.GetMouseButtonDown(1) && !stats.isSprinting)
            {
                //play aim in animation
                animator.SetBool("isAiming", true);
                is_aiming = true;
                stats.canSprint = false;
            }
            if (Input.GetMouseButtonUp(1))
            {
                //play aim out animation
                animator.SetBool("isAiming", false);
                is_aiming = false;
                stats.canSprint = true;
            }
        }
    }
    private void Fire()
    {
        animator.SetTrigger("Fire");
        damage += stats.GetDamage();
        MuzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(muzzle_pos.position, muzzle_pos.forward, out hit, 1000))
        {
            CheckHit(hit);

            TrailRenderer trail = Instantiate(bulletTrailPrefab, muzzle_pos.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
        }
        else
        {
            TrailRenderer trail = Instantiate(bulletTrailPrefab, muzzle_pos.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, muzzle_pos.position + muzzle_pos.forward * 100, Vector3.zero, false));
        }

        HandleRecoil();
        Debug.DrawRay(muzzle_pos.position, muzzle_pos.forward * 1000, Color.blue);
        current_magazine_size--;
    }
    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= bulletSpeed * Time.deltaTime;

            yield return null;
        }
        Trail.transform.position = HitPoint;

        if (MadeImpact)
        {
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }

    private void CheckHit(RaycastHit hit)
    {
        
        BC_Zombie_Stats zombie = hit.collider.GetComponent<BC_Zombie_Stats>();
        if (zombie != null)
        {
            //Debug.Log("hit!");
            zombie.Damage(damage); 
        }
    }
    private void Reload()
    {
        is_reloading= true;
        can_fire = false;
        // play reload animation and sound
        animator.SetTrigger("Reload");


        next_reload_speed -= Time.deltaTime;
        if (next_reload_speed <= 0)
        {
            if (current_ammo >= magazine_capacity)
            {
                current_ammo -= (magazine_capacity - current_magazine_size);
                current_magazine_size = magazine_capacity;
                
            }
            else
            {
                current_magazine_size = current_ammo;
                current_ammo = 0;
            }
            is_reloading = false;   
            can_fire = true;
            animator.SetBool("Reload", false);
        }
    }

    private void UpdateGunPosition()
    {
        // Smoothly interpolate recoil rotation
        targetRecoilRotation = Vector3.Lerp(targetRecoilRotation, Vector3.zero, recoil_return_speed * Time.deltaTime);
        currentRecoilRotation = Vector3.Lerp(currentRecoilRotation, targetRecoilRotation, recoil_smoothness * Time.deltaTime);
        camera.localRotation = Quaternion.Euler(currentRecoilRotation);

        gunHolder.localPosition = Vector3.Lerp(gunHolder.localPosition, initialPosition, recoil_return_speed * Time.deltaTime);
    }

    private void HandleRecoil()
    {
        if(is_aiming)
        {
            targetRecoilRotation += new Vector3(recoil_aim_x, Random.Range(-recoil_aim_y, recoil_aim_y), Random.Range(-recoil_aim_z, recoil_aim_z));
        }
        else
        {
            targetRecoilRotation += new Vector3(recoil_x, Random.Range(-recoil_y, recoil_y), Random.Range(-recoil_z, recoil_z));
        }
        Vector3 recoilDirection = -gunHolder.forward;
        gunHolder.position += recoilDirection * gun_recoil;
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

    }
    public void UpdateAnimation()
    {
        animator.SetTrigger("Equip");

        animator.SetFloat("EquipSpeed", 1f / equip_speed);
        animator.SetFloat("ReloadSpeed", 1f / reload_speed);
        animator.SetFloat("AimInSpeed", 1f / aimInSpeed);
        animator.SetFloat("RPM", 1 / fire_rate);


    }

    private void HandleAnimation()
    {
        if(stats.isSprinting) 
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
    private void UpdateGUI()
    {
        gun_name.text = str_weapon_name;
        Magazine.text = current_magazine_size.ToString() + " / " + current_ammo.ToString();
    }


}
