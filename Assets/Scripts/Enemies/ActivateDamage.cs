using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDamage : MonoBehaviour
{

    public GameObject HurtBox;

    public void ActivateDeactivateBox()
    {
        HurtBox.SetActive(!HurtBox.activeInHierarchy);
    }
    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }    
}
