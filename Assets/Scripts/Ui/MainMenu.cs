using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstLevel, levelSelect;
    public GameObject ContinueButton;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Continue"))
        {
            ContinueButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Continue", 0);
        SceneManager.LoadScene(firstLevel);
        
    }

    public void Continue()
    {
        if(levelSelect!="")
        {
            SceneManager.LoadScene(levelSelect);
        }
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
