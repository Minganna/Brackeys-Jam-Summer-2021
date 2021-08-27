using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapPointColors : MonoBehaviour
{
    public int levelN;
    public Texture2D[] Colors;

    // Start is called before the first frame update
    void Start()
    {
        Levelscollectiblemanager.instance.ChangeMapPointColor(levelN);
    }

    public void UpdateColorDependingonCollectible(int texture)
    {
        GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", Colors[texture]);
        
    }


}
