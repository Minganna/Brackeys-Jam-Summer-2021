using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelscollectiblemanager : MonoBehaviour
{
    public static Levelscollectiblemanager instance;
    public ChangeMapPointColors[] LevelsPoints;

    public static bool[] level1=new bool[2];
    public static bool[] level2 = new bool[5];
    public static bool[] level3 = new bool[2];
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevel(LevelExit tadpole)
    {
        switch(tadpole.Level)
        {
            case 0:
                {
                    level1[tadpole.tadpoleindex] = true;
                    break;
                }
            case 1:
                {
                    level2[tadpole.tadpoleindex] = true;
                    break;
                }
        }
    }

    public void ChangeMapPointColor(int levelN)
    {
        int collected=0;
        int size=0;
        switch (levelN)
        {
            case 0:
                {
                    foreach (bool achieved in level1)
                    {
                        if (achieved)
                        {
                            collected++;
                        }
                        size++;
                    }
                    break;
                }
            case 1:
                {
                    foreach(bool achieved in level2)
                    {
                        if(achieved)
                        {
                            collected++;
                        }
                        size++;
                    }
                    break;
                }
        }
        if(collected==0)
        {
            Debug.Log("Here " + size);
            LevelsPoints[levelN].UpdateColorDependingonCollectible(0);
        }
        if(collected>0&&collected<size)
        {
            Debug.Log("Here " + size);
            LevelsPoints[levelN].UpdateColorDependingonCollectible(1);
        }
        if(collected==size)
        {
            Debug.Log("Here " + size);
            LevelsPoints[levelN].UpdateColorDependingonCollectible(2);
        }
    }

    public bool checkIfCanBeCollected(LevelExit ordertadpole)
    {
        switch (ordertadpole.Level)
        {
            case 0:
                {
                    if(level1[ordertadpole.tadpoleindex])
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }    
                }
            case 1:
                {
                    if (level2[ordertadpole.tadpoleindex])
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            case 2:
                {
                    if (level3[ordertadpole.tadpoleindex])
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            default:
                {
                    return true;
                }
        }
    }
}
