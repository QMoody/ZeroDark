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

    [Header("Movement Variables")] // MOVEMENT VARIABLES
    public float walkAccelSpeed;
    public float walkSpeed;
    //Vector2 currentAccelSpeed;
    private float currentForwardSpeed;

    public float turnSpeed;

    #endregion

    //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

    #region Initialize
    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }
    #endregion

    void Update()
    {
        StandardMovement();
    }

    #region Movement
    void StandardMovement()
    {
        float step = Time.deltaTime;

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            currentForwardSpeed = Mathf.MoveTowards(currentForwardSpeed, 1, step * walkAccelSpeed);
        else
            currentForwardSpeed = Mathf.MoveTowards(currentForwardSpeed, 0, step * walkAccelSpeed);

        Vector3 moveDir = transform.forward * -currentForwardSpeed * walkSpeed;
        rbPlayer.velocity = Vector3.MoveTowards(rbPlayer.velocity, new Vector3(moveDir.x, rbPlayer.velocity.y, moveDir.z), step * 20);

        //Vector3 targetDir = new Vector3(transform.position.x + Input.GetAxisRaw("Vertical"), transform.position.y, transform.position.z);
        Vector3 targetDir = new Vector3(-Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        Debug.Log(targetDir);

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step * turnSpeed * currentForwardSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        //currentAccelSpeed = new Vector2(Mathf.MoveTowards(currentAccelSpeed.x, 1, step * walkAccelSpeed), Mathf.MoveTowards(currentAccelSpeed.y, 1, step * walkAccelSpeed));
        //Vector2 inputDir = new Vector2(Input.GetAxisRaw("Vertical") * currentAccelSpeed.x, Input.GetAxisRaw("Horizontal") * currentAccelSpeed.y).normalized * walkSpeed;
        //Vector3 moveDir = transform.forward * inputDir.x + transform.right * inputDir.y;
        //rbPlayer.velocity = Vector3.MoveTowards(rbPlayer.velocity, new Vector3(moveDir.x, rbPlayer.velocity.y, moveDir.z), step * 20);
    }
    #endregion
}
