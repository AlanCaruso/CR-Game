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

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 CamRight;

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        player.Move(movePlayer * playerSpeed * Time.deltaTime);
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        camDirection();
        Debug.Log(player.velocity.magnitude);
        movePlayer = playerInput.x * CamRight + playerInput.z * camForward;

        player.transform.LookAt(player.transform.position + movePlayer);
    }
    
    void camDirection(){
        camForward = mainCamera.transform.forward;
        CamRight = mainCamera.transform.right;

        camForward.y = 0;
        CamRight.y = 0;

        camForward = camForward.normalized;
        CamRight = CamRight.normalized;
    }
}
