using UnityEngine;
using UnityEngine.UI;

public class BC_BuyableArea : MonoBehaviour
{
    public TextMesh text;
    public int int_cost;

    public GameObject[] Spawners;

    private void Start()
    {
        text.text = "Press E to buy for " + int_cost.ToString();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E) && collider.GetComponent<pc_stats>().GetCurrency() >= int_cost) 
            { 
                collider.GetComponent<pc_stats>().AddCurrency(-int_cost);
                // play door buying animation here
                
                text.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player")) text.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach(GameObject spawner in Spawners)
        {
            spawner.GetComponent<BC_ZombieSpawner>().bl_is_active= true;
        }
    }
}
