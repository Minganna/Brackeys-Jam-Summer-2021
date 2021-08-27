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

    public float bounceForce = 8.0f;


    [SerializeField]
    GameObject playerModel;
    public GameObject[] playerPieces;

    Animator PlayerAnimator;

    CharacterController charController;
    Camera theCam;

    Vector3 moveDirection;

    public static PlayerController instance;

    public bool canMove=true;

    public bool isKnocking;
    public float knockBackLenght=0.5f;
    private float knockBackCounter;
    public Vector2 knockBackPower;
    public bool canPunch = true;

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
        if(Input.GetButtonDown("Fire1")&&canPunch)
        {
            canMove = false;
            PlayerAnimator.SetTrigger("Punching");
            canPunch = false;
        }
        if(canMove)
        {
            CharacterMovements();
        }
        if(isKnocking)
        {
            Knocking();
        }

    }

    private void Knocking()
    {
        knockBackCounter -= Time.deltaTime;
        float yStore = moveDirection.y;
        moveDirection = (playerModel.transform.forward * -knockBackPower.x);
        moveDirection.y = yStore;

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        charController.Move(moveDirection * Time.deltaTime);

        if (knockBackCounter <= 0)
        {
            isKnocking = false;
            canMove = true;
        }
    }

    //Function used to apply physics
    void FixedUpdate()
    {
        if (canMove)
        {
            charController.Move((moveDirection * Time.deltaTime));
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, theCam.transform.rotation.eulerAngles.y, 0.0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.z));
                if (playerModel)
                {
                    playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                }

            }
            PlayerAnimator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
            PlayerAnimator.SetBool("Grounded", charController.isGrounded);
        }
        else
        {
            PlayerAnimator.SetFloat("Speed", 0);
        }
        
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

    public void KnockBack()
    {
        isKnocking = true;
        canMove = false;
        knockBackCounter = knockBackLenght;
        Debug.Log("Knock Back");
        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }

    public void SetWinningAnim()
    {
       
        PlayerAnimator.SetBool("Won",true);
        playerModel.transform.LookAt(2 * playerModel.transform.position-Camera.main.transform.position);
        playerModel.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y-180, 0.0f);
        canMove = false;
    }
    public void StopWinningAnim()
    {
        PlayerAnimator.SetBool("Won", false);
    }
}
