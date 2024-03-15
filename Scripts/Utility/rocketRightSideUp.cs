using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketRightSideUp : MonoBehaviour
{
    private spaceShipController controller;
    private Rigidbody rb;

    public float resetThreshold, timerTillReset, resetForce = 3;
    private float privateTimerResetCount;
    [HideInInspector]
    public Transform planetPos;
    public bool inParent = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<spaceShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.velocity.sqrMagnitude);
        if (controller.up == 1 && rb.velocity.sqrMagnitude < resetThreshold)
        {
            privateTimerResetCount += Time.deltaTime;
        }
        else
        {
            privateTimerResetCount = 0;
        }
        if (privateTimerResetCount >= timerTillReset)
        {
            privateTimerResetCount = 0;
            Reset();
        }
    }

    private void Reset()
    {
        Vector3 upVector = transform.position - planetPos.position;
        rb.AddTorque(Vector3.Cross(upVector, transform.TransformDirection(Vector3.up)) * -resetForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        planetPos = collision.gameObject.transform;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (controller.up == 0)
        {
            transform.parent = collision.transform;
            inParent = true;
        }
        else
        {
            transform.parent = null;
            inParent = false;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        transform.parent = null;
        inParent = false;
    }


}
