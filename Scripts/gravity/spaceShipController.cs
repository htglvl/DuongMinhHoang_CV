using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class spaceShipController : MonoBehaviour
{
    private Rigidbody rb;
    public Button launchButton;
    public FixedJoystick joystick;
    public float force = 10, rotateForce;
    private Vector3 direction, rotation;
    private int turnForward, turnBackward, turnRight, turnLeft;
    [HideInInspector]
    public int up;
    public ParticleSystem fire3;
    public bool isTestingMobile = true;
    public float launchButtonPressed;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.isMobilePlatform);
        rb = GetComponent<Rigidbody>();
        fire3.Stop();
        if (Application.isMobilePlatform || isTestingMobile)
        {
            joystick.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
            launchButton.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
        }
        else
        {
            joystick.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
            launchButton.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isMobilePlatform || isTestingMobile)
        {
            turnForward = joystick.Vertical == 1 ? 1 : 0;
            turnBackward = joystick.Vertical == -1 ? 1 : 0;
            turnRight = joystick.Horizontal == 1 ? 1 : 0;
            turnLeft = joystick.Horizontal == -1 ? 1 : 0;
            up = launchButtonPressed == 1 ? 1 : 0;
        }
        else
        {
            up = Input.GetKey(KeyCode.Space) ? 1 : 0;
            turnForward = Input.GetKey(KeyCode.W) ? 1 : 0;
            turnBackward = Input.GetKey(KeyCode.S) ? 1 : 0;
            turnRight = Input.GetKey(KeyCode.D) ? 1 : 0;
            turnLeft = Input.GetKey(KeyCode.A) ? 1 : 0;
        }

    }
    void FixedUpdate()
    {
        if (up == 1)
        {
            fire3.Play();
        }
        else
        {
            fire3.Stop();
        }
        direction = (transform.up * up).normalized;
        rotation = (-transform.forward * turnForward +
                    transform.forward * turnBackward +
                    transform.right * turnRight +
                    -transform.right * turnLeft).normalized;

        // if (Input.GetKey(KeyCode.Space))
        // {
        //     direction = Vector3.up;

        // }
        // if (Input.GetKey(KeyCode.W))
        // {
        //     rb.AddForce(Camera.main.transform.forward * );
        // }
        rb.AddForce(direction * force);
        rb.AddTorque(rotation * rotateForce);
    }

    public void setDownState()
    {
        launchButtonPressed = 1;
    }
    public void setUpState()
    {
        launchButtonPressed = 0;
    }
}
