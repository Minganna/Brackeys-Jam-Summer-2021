using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLookingAtyou : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerController.instance.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
    }
}
