using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelWorldManager : MonoBehaviour
{
    public TextMeshProUGUI lNameText;
    public GameObject lNamePanel;
    public static LevelWorldManager instance;

    private void Awake()
    {
        instance = this;
    }

}
