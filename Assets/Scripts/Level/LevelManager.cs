using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public GameObject OrderTadpoletoShow;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if(OrderTadpoletoShow)
        {
            OrderTadpoletoShow.SetActive(false);
        }
    }

    public void showHiddenTadPole()
    {
        OrderTadpoletoShow.SetActive(true);
    }


}
