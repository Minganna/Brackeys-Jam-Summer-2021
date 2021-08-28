using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWorldResetPosition : MonoBehaviour
{
    public Vector3 PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = PlayerController.instance.transform.position;
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
            PlayerController.instance.transform.position = PlayerPos;
            PlayerController.instance.gameObject.SetActive(true);
        }
    }
}
