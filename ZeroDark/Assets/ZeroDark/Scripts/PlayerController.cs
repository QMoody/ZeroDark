using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Gameobjects & Components")] // GAMEOBJECTS & COMPONENTS
    public GameObject playerModel;
    public GameObject playerCamera;
    private Rigidbody rbPlayer;
    public Animator anim;

    [Header("Movement Variables")] // MOVEMENT VARIABLES
    public float walkAccelSpeed;
    public float walkSpeed;
    public float runSpeed;
    private float currentForwardSpeed;

    public bool isRunning;

    public float turnSpeed;

    [Header("Camera Variables")] // CAMERA VARIABLES
    private Vector3 camDistance;
    private Vector3 camDistanceTarget;
    #endregion

    //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

    #region Initialize
    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Start()
    {
        camDistanceTarget = transform.InverseTransformPoint(playerCamera.transform.position);
    }
    #endregion

    void Update()
    {
        StandardMovement();
        CameraFollow();
        CharacterAnimations();
    }

    #region Movement
    void StandardMovement()
    {
        float step = Time.deltaTime;
        Vector3 moveDir;

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            currentForwardSpeed = Mathf.MoveTowards(currentForwardSpeed, 1, step * walkAccelSpeed);
        else
            currentForwardSpeed = Mathf.MoveTowards(currentForwardSpeed, 0, step * walkAccelSpeed);

        if (Input.GetButton("Shift"))
        {
            moveDir = transform.forward * -currentForwardSpeed * walkSpeed * runSpeed;
            isRunning = true;
        }
        else
        {
            moveDir = transform.forward * -currentForwardSpeed * walkSpeed;
            isRunning = false;
        }

        rbPlayer.velocity = Vector3.MoveTowards(rbPlayer.velocity, new Vector3(moveDir.x, rbPlayer.velocity.y, moveDir.z), step * 20);

        Vector3 targetDir = new Vector3(-Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step * turnSpeed * currentForwardSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void CameraFollow()
    {
        playerCamera.transform.position = transform.position + camDistanceTarget;
    }
    #endregion

    void CharacterAnimations()
    {
        anim.SetFloat("InputForward", currentForwardSpeed);
        anim.SetBool("Running", isRunning);
    }
}
