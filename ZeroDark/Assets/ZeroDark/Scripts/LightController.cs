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
    public bool isThrown;
    public bool isLeading;
    public bool checkBack;
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

        //if (isThrown == false)
        ThrowLight();

        CheckThrow();
    }

    void HoldLight()
    {
        lightSource.transform.position = Vector3.SmoothDamp(lightSource.transform.position, lightHoldPoint.transform.position, ref velocity, lightFollowSpeed);
    }

    void ThrowLight()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f))
            Debug.Log(hit.point);

        if (Input.GetButtonUp("Fire1") && isHolding == true && isThrown == false && isLeading == false)
        {
            StopCoroutine(MouseHoldCheck());

            checkBack = false;
            StartCoroutine(ThrowWait());

            isThrown = true;
            isHolding = false;
            lightSource.transform.GetChild(0).GetComponent<Collider>().enabled = true;

            Vector3 direct = (hit.point - lightSource.transform.position).normalized * throwForce;
            rbShpere.useGravity = true;
            rbShpere.AddForce(direct, ForceMode.Impulse);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            isHolding = true;
            rbShpere.useGravity = false;
            rbShpere.velocity = Vector3.zero;
            lightSource.transform.GetChild(0).GetComponent<Collider>().enabled = false;
        }

        if (Input.GetButton("Fire1") && isLeading == true)
        {
            isHolding = false;
            lightSource.transform.position = Vector3.SmoothDamp(lightSource.transform.position, hit.point + new Vector3(0,0.3f,0), ref velocity, lightFollowSpeed);
        }
        else if(Input.GetButtonUp("Fire1") && isLeading == true)
        {
            isLeading = false;
            isHolding = true;
        }

    }

    void CheckThrow()
    {
        if (Input.GetButtonDown("Fire1") && isHolding == true)
        {
            StartCoroutine(MouseHoldCheck());
        }


        if (isThrown == true && checkBack == true && (lightSource.transform.position - lightHoldPoint.transform.position).sqrMagnitude <= 0.01f)
        {
            isThrown = false;
        }
    }

    IEnumerator MouseHoldCheck()
    {
        yield return new WaitForSeconds(0.7f);
        if(isThrown == false)
            isLeading = true;
    }

    IEnumerator ThrowWait()
    {
        yield return new WaitForSeconds(0.1f);
        checkBack = true;
    }
}
