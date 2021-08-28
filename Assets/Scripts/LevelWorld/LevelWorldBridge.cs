using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWorldBridge : MonoBehaviour
{
    public string levelToUnlock;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(levelToUnlock+"_unlocked")==0)
        {
            gameObject.SetActive(false);
        }
    }

}
