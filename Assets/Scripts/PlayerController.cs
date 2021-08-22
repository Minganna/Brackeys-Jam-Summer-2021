using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed =10.0f; 
    [SerializeField]
    float jumpForce=5.0f;
    [SerializeField]
    float gravityScale = 5.0f;

    CharacterController charController;

    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        charController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        CharacterMovements();
    }

    //Function used to apply physics
    void FixedUpdate()
    {
        charController.Move((moveDirection * Time.deltaTime));
    }

    //function that control the character movements
    private void CharacterMovements()
    {
        float yStore = moveDirection.y;
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;

        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime*gravityScale;

        
    }
}
