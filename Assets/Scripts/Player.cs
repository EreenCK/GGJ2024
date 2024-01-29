using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject limon;
    public GameObject þiþe;
    public GameObject kitap;
    public GameObject balik;
    public Camera playerCamera; 
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    
    [Header("UI")]
    public GameObject etkilesim;
    public GameObject konusma;
    public GameObject QuestionLog;
    private bool question = false;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    private GameObject function;
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion

        if (Input.GetKeyDown(KeyCode.Q) && !question)
        {
            QuestionLog.SetActive(true);
            question = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && question)
        {
            QuestionLog.SetActive(false);
            question = false;
        }



        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Etkilesim"))
        {
            etkilesim.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Etkilesim"))
        {
            etkilesim.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Etkilesim"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                Destroy(other.gameObject);
                etkilesim.SetActive(false);
            }
           
        }
    }
    public void limonObjeac()
    {
        limon.SetActive(true);
    }
    public void limonObjekapa( )
    {
        limon.SetActive(false);
    }
    public void þiþeObjeac( )
    {
        þiþe.SetActive(true);
    }
    public void þiþeObjekapa( )
    {
        þiþe.SetActive(false);
    }
    public void kitapObjeac()
    {
        kitap.SetActive(true);
    }
    public void balikObjeac()
    {
        balik.SetActive(true);
    }
    
}



