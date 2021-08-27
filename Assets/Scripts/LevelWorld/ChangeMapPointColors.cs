using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapPointColors : MonoBehaviour
{
    public int levelN;
    public Texture2D[] Colors;
    public bool UnlockedLevel=false;

    // Start is called before the first frame update
    public void checkColor()
    {
        if(UnlockedLevel)
        {
            Levelscollectiblemanager.instance.ChangeMapPointColor(levelN);
        }
       
    }

    public void UpdateColorDependingonCollectible(int texture)
    {
        GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", Colors[texture]);
        
    }


}
