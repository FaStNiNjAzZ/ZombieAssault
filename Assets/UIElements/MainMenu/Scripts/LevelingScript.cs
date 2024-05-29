using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingScript : MonoBehaviour
{
    [SerializeField]
    //xp for prestige: 357500
    int xp;
    int level;
    int prestige;

    public int menXp;
    public int menLevel;
    public int menPrestige;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xpToLevel();
        levelToPrestige();
        //Debug.Log("XP:" + xp);
        
    }
    

    void xpToLevel()
    {
        if (level < 55)
        {
            if (xp >= Math.Floor((1000 * level + (1000 * Math.Pow(1.1, level - 1))) + (prestige * 226871)))
            {
                level++;
                //Debug.Log("Level" + level);
            }
            else if (xp < Math.Floor((1000 * (level-1) + (1000 * Math.Pow(1.1, (level-1) - 1))) + (prestige * 226871)))
            {
                level--;
               // Debug.Log("Level" + level);
            }
            //Debug.Log(Convert.ToString(((level * ((1 + level / 10)) * 1000) + (prestige * 357500))));
            //Debug.Log(Convert.ToString((((level - 1) * ((1 + level / 10)) * 1000) + (prestige * 357500))));

            //Debug.Log(Convert.ToString(Math.Floor(1000 * level + (1000 * Math.Pow(1.1, level - 1)))));
            //Debug.Log(Convert.ToString(Math.Floor((1000 * (level-1) + (1000 * Math.Pow(1.1, (level-1) - 1))))));
            menLevel = level;
        }
    }

    void levelToPrestige()
    {
        
        if (prestige < 25)
        {
            
            if (level >= 55)
            {
                level = 1;
                prestige++;
                Debug.Log("Prestige" + prestige);
            }
            
        }
        menPrestige = prestige;
    }

    public void LevelUpButton()
    {
        xp += Convert.ToInt32(Math.Floor((1000 * level + (1000 * Math.Pow(1.1, level - 1)))) - xp);
    }

    public void LevelDownButton()
    {
        xp -= Convert.ToInt32(Math.Floor((1000 * level + (1000 * Math.Pow(1.1, level - 1)))) - xp);
    }

}
