using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActive : MonoBehaviour
{
    public GameObject door;
    public Material normalMat;
    public Material litMat;

    public bool isOn = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "LightOrb" && isOn == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetComponent<MeshRenderer>().material = litMat;
            isOn = true;
            door.GetComponent<DoorActive>().SwitchState();
        }
        else if (collision.transform.tag == "LightOrb" && isOn == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetComponent<MeshRenderer>().material = normalMat;
            isOn = false;
            door.GetComponent<DoorActive>().SwitchState();
        }
    }
}
