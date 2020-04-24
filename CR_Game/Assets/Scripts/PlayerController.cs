using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public CharacterController player;

    public float playerSpeed;
    private Vector3 movePlayer;
    private Vector3 playerInput;

    public float gravity = 9.8f;

    public float fallVelocity;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 CamRight;
    public float jumpForce;
    public Collider coll;

    public float reduction;


    void Start()
    {
        player = GetComponent<CharacterController>();
        coll = GetComponent<Collider>();
        
    }

    
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        
        
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        camDirection();
        

        movePlayer = playerInput.x * CamRight + playerInput.z * camForward;
        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);
    
        
        SetGravity();
        playerSkills();
        player.Move(movePlayer * Time.deltaTime);
        // Debug.Log(player.velocity.magnitude);
        if(player.isGrounded){
            print("is grounded");
        }else{
            print("not grounded");
        }
        
        
    }
    
    void camDirection(){
        camForward = mainCamera.transform.forward;
        CamRight = mainCamera.transform.right;

        camForward.y = 0;
        CamRight.y = 0;

        camForward = camForward.normalized;
        CamRight = CamRight.normalized;
    }

    // bool Grounded(){
    //     return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);
    // }
   
    void playerSkills(){
        // touchingGround = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1);
       
        // if(Input.GetButtonDown("Jump")){
        //     if(Grounded()){
        //         fallVelocity = jumpForce;
        //          movePlayer.y = fallVelocity;
        //     }
        // }
        if(player.isGrounded && Input.GetButtonDown("Jump")){
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
         }
    }
    
    void SetGravity(){
        
        if(player.isGrounded){
            fallVelocity = (-gravity - reduction) * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }else{
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }
}
