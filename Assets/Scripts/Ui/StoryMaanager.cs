using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryMaanager : MonoBehaviour
{
    public Sprite[] BackgroundStories;
    public Image StoryBoard;
    public int counter;

    public void ChangeStoryorLoadLevel()
    {
        if(counter<BackgroundStories.Length)
        {
            StoryBoard.GetComponent<Image>().overrideSprite = BackgroundStories[counter];
            counter++;
            return;
        }
        else
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }
}
