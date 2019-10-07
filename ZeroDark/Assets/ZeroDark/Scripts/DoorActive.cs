using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActive : MonoBehaviour
{
    public float upPoint;
    public float downPoint;
    public bool isDoorUp;
    private Vector3 velocity = Vector3.zero;
    public float doorSpeed;
    Vector3 movePoint;

    private void Start()
    {
        upPoint = transform.GetChild(0).localPosition.y;
        downPoint = transform.GetChild(0).localPosition.y - 1.05f;
        movePoint = new Vector3(transform.GetChild(0).localPosition.x, upPoint, transform.GetChild(0).localPosition.z);
    }

    void Update()
    {
        transform.GetChild(0).localPosition = Vector3.SmoothDamp(transform.GetChild(0).localPosition, movePoint, ref velocity, doorSpeed);
    }

    public void SwitchState()
    {
        isDoorUp = !isDoorUp;
        if (isDoorUp == true)
            movePoint = new Vector3(transform.GetChild(0).localPosition.x, upPoint, transform.GetChild(0).localPosition.z);
        else if (isDoorUp == false)
            movePoint = new Vector3(transform.GetChild(0).localPosition.x, downPoint, transform.GetChild(0).localPosition.z);
    }
}
