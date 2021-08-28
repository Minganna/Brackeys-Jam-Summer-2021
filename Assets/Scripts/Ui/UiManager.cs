using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    [SerializeField]
    Image BlackScreen;
    public float fadeSpeed = 2.0f;
    public bool fadeToBlack;
    public bool fadeFromBlack;

    public Image[] HealthImages;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI TadPoleText;
    public GameObject pauseScreen,OptionScreen;

    public Slider MusicVolSlider;
    public Slider SfxVolSlider;

    public string levelSelect, mainMenu;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (TadPoleText)
        {
            TadPoleText.text = GameManager.orderTadPole.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeToBlack)
        {
            BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, Mathf.MoveTowards(BlackScreen.color.a, 1.0f, fadeSpeed * Time.deltaTime));
            if(BlackScreen.color.a==1.0f)
            {
                fadeToBlack = false;
            }
        }
        if(fadeFromBlack)
        {
            BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, Mathf.MoveTowards(BlackScreen.color.a, 0.0f, fadeSpeed * Time.deltaTime));
            if (BlackScreen.color.a == 0.0f)
            {
                fadeFromBlack= false;
            }
        }
    }

    public void Resume()
    {
        GameManager.instance.PauseUnPause();
        PlayerController.instance.canPunch = true;
    }
    public void OpenOptions()
    {
        OptionScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        OptionScreen.SetActive(false);
    }
    public void LevelSelect()
    {
        GameManager.instance.PauseUnPause();
        SceneManager.LoadScene(levelSelect);
    }

    public void MainMenu()
    {
        GameManager.instance.PauseUnPause();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(mainMenu);
    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }
    public void SetSfxLevel()
    {
        AudioManager.instance.SetSfxLevel();
    }
}
