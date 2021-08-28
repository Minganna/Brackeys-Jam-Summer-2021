using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLevelEntry : MonoBehaviour
{

    public string levelName,levelToCheck,displayName;

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
        if (PlayerPrefs.GetString("CurrentLevel") == levelName)
        {
            PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
            PlayerController.instance.gameObject.SetActive(true);
            if(FindObjectOfType<LevelWorldResetPosition>())
            {
                FindObjectOfType<LevelWorldResetPosition>().PlayerPos = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canLoadLevel&&levelUnlocked&&PlayerController.instance.canPunch)
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
            {
                PlayerPrefs.SetString("CurrentLevel", levelName);
                SceneManager.LoadScene(levelName);
            }
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            canLoadLevel = true;
            LevelWorldManager.instance.lNamePanel.SetActive(true);
            LevelWorldManager.instance.lNameText.text = displayName;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = false;
            LevelWorldManager.instance.lNamePanel.SetActive(false);
        }
    }
}
