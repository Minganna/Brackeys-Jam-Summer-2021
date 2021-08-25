using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickUp : MonoBehaviour
{
    public int value;
    public int SfxToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            GameManager.instance.AddCollectable(value);
            Destroy(gameObject);
            AudioManager.instance.Sfx(SfxToPlay);
        }
    }
}
