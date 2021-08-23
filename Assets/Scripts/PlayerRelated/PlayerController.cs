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
    [SerializeField]
    float rotateSpeed = 10.0f;

    bool StartingGrounded=false;

    [SerializeField]
    GameObject playerModel;

    Animator PlayerAnimator;

    CharacterController charController;
    Camera theCam;

    Vector3 moveDirection;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<CharacterController>())
        {
            charController = this.GetComponent<CharacterController>();
        }
        if(Camera.main)
        {
            theCam = Camera.main;
        }
        if(playerModel.GetComponent<Animator>())
        {
            PlayerAnimator = playerModel.GetComponent<Animator>();
        }
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
        if(Input.GetAxisRaw("Horizontal")!=0|| Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, theCam.transform.rotation.eulerAngles.y, 0.0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.z));
            if(playerModel)
            {
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

        }
        PlayerAnimator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        PlayerAnimator.SetBool("Grounded", charController.isGrounded);
        
    }

    //function that control the character movements
    private void CharacterMovements()
    {
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right* Input.GetAxisRaw("Horizontal"));
        moveDirection.Normalize();
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;

        if(charController.isGrounded)
        {
            
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
            
        }
        else
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        }
       

        
    }
}
