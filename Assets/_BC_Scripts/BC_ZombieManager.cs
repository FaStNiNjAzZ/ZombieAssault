using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BC_ZombieManager : MonoBehaviour
{
    [SerializeField] BC_ZombieSpawner[] zombie_spawners;
    [SerializeField] GameObject GameObject_Zombie_Prefab;

    public List<GameObject> alive_zombies = new List<GameObject>();

    public float zombie_spawn_timer;

    [SerializeField] int int_Current_Round;
    public int int_Zombies_To_Spawn;
    public Text text;

    public GameObject[] ZombieDrops;

    void Start()
    {
        zombie_spawners = FindObjectsOfType<BC_ZombieSpawner>();
        StartRound();
    }


    public void StartRound()
    {
        int_Current_Round++;
        int_Zombies_To_Spawn = Mathf.RoundToInt(Mathf.Pow(int_Current_Round, 2));
        text.text = "Round: " + int_Current_Round.ToString();
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        for (int i = 0; i < int_Zombies_To_Spawn; i++)
        {
            foreach (BC_ZombieSpawner spawner in zombie_spawners)
            {
                if (spawner != null && spawner.bl_is_active)
                {
                    GameObject zombie = Instantiate(GameObject_Zombie_Prefab, spawner.transform.position, Quaternion.identity, this.transform);
                    alive_zombies.Add(zombie);
                    BC_Zombie_Stats zombieScript = zombie.GetComponent<BC_Zombie_Stats>();
                    zombieScript.fl_Zombie_Speed = 2;
                    zombieScript.fl_Zombie_Health += 0;
                    zombieScript.int_zombie_damage = 10;
                    zombieScript.zombieAttackSpeed = 1.5f;
                    yield return new WaitForSeconds(zombie_spawn_timer); // Adjust delay between zombie spawns
                }
            }
        }
    }
}