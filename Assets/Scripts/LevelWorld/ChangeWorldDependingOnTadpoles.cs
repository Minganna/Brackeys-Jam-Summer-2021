using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWorldDependingOnTadpoles : MonoBehaviour
{

    public Transform Bridge;
    public GameObject Water;
    public Texture2D GreenWater;


    // Start is called before the first frame update
    void Awake()
    {
        if(GameManager.instance.getOrderTadPolesN()>3)
        {
            Bridge.rotation = Quaternion.Euler(-90.0f, transform.rotation.eulerAngles.y+180.0f, 0.0f);
        }
        if (GameManager.instance.getOrderTadPolesN() >=5)
        {
            GameManager.instance.SetRotationSun();
        }
        if (GameManager.instance.getOrderTadPolesN() >= 8)
        {
            Water.GetComponent<Renderer>().material.SetTexture("_MainTex", GreenWater);
        }
    }


}
