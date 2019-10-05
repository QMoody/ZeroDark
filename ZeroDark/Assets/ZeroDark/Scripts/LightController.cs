using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    #region Variables
    [Header("Gameobjects & Components")] // GAMEOBJECTS & COMPONENTS
    public GameObject lightSource;
    public GameObject lightHoldPoint;
    public Rigidbody rbShpere;
    public PlayerController playerCon;

    public float lightFollowSpeed;

    public float throwForce;

    private Vector3 velocity = Vector3.zero;

    public bool isHolding;
    #endregion

    //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
    
    #region Initialize
    private void Awake()
    {
        rbShpere = lightSource.GetComponent<Rigidbody>();
        playerCon = GetComponent<PlayerController>();
    }

    void Start()
    {
        
    }
    #endregion

    void Update()
    {
        if (isHolding == true)
            HoldLight();

        ThrowLight();
    }

    void HoldLight()
    {
        float step = Time.deltaTime;

        lightSource.transform.position = Vector3.SmoothDamp(lightSource.transform.position, lightHoldPoint.transform.position, ref velocity, lightFollowSpeed);
    }

    void ThrowLight()
    {
        if (Input.GetButtonDown("Fire1") && isHolding == true)
        {
            isHolding = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f))
            {
                Debug.Log(hit.point);
            }

            Vector3 direct = (hit.point - lightSource.transform.position).normalized * throwForce;
            rbShpere.useGravity = true;
            rbShpere.AddForce(direct, ForceMode.Impulse);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            isHolding = true;
            rbShpere.useGravity = false;
        }
    }
}
