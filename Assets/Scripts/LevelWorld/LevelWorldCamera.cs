using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWorldCamera : MonoBehaviour
{
    public Transform Target;
    Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
        Target = PlayerController.instance.gameObject.transform;
        Offset = transform.position - Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Target.position + Offset;
    }
}
