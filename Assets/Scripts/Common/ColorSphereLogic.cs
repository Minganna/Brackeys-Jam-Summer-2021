using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSphereLogic : MonoBehaviour
{

public void DestroySphereandchangeColor()
    {

        GameManager.instance.SetCameraSketchColorBall();
        GameObject DeathEffect = Resources.Load("Prefabs/Effects/ColorSphereDestroyed", typeof(GameObject)) as GameObject;
        Instantiate(DeathEffect, transform.position+new Vector3(0.0f,1.0f,0.0f),PlayerController.instance.transform.rotation);
        Destroy(gameObject);
    }


}
