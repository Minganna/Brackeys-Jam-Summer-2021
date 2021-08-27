using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLevelEntry : MonoBehaviour
{

    public string levelName,levelToCheck;

    bool canLoadLevel,levelUnlocked;

    // Start is called before the first frame update
    void Start()
    {

            if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == "")
            {
                if (GetComponent<ChangeMapPointColors>())
                 {
                     GetComponent<ChangeMapPointColors>().UnlockedLevel = true;
                 }
            levelUnlocked = true;
            }
            else
            {
                levelUnlocked = false;
            }
        if (GetComponent<ChangeMapPointColors>())
        {
            GetComponent<ChangeMapPointColors>().checkColor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canLoadLevel&&levelUnlocked)
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene(levelName);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            canLoadLevel = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = false;
        }
    }
}
