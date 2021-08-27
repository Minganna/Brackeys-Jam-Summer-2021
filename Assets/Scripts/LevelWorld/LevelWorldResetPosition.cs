using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWorldResetPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
            PlayerController.instance.gameObject.SetActive(true);
        }
    }
}
