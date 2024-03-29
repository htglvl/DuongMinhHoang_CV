using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using TMPro;

public class spaceShipController : MonoBehaviour
{
    private Rigidbody rb;
    public Button launchButton, exploreButton;
    public FixedJoystick joystick;
    public float force = 10, rotateForce;
    private Vector3 direction, rotation;
    private int turnForward, turnBackward, turnRight, turnLeft;
    [HideInInspector]
    public int up;
    public ParticleSystem fire3;
    public bool isTestingMobile = true;
    public float launchButtonPressed;
    public AudioSource rocketThrust;


    public TMP_Text tutorialText;
    public Canvas rocketSelfCanvas;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fire3.Stop();
        if (Application.isMobilePlatform || isTestingMobile)
        {
            joystick.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
            launchButton.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
            exploreButton.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
            tutorialText.text = " Use the controls to move around";
        }
        else
        {
            joystick.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
            launchButton.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
            exploreButton.gameObject.SetActive(Application.isMobilePlatform || isTestingMobile);
            tutorialText.text = " Use WASD and Space to move around";
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
            rocketSelfCanvas.gameObject.SetActive(false);
            rocketThrust.volume = 1;
            if (!rocketThrust.isPlaying)
            {
                rocketThrust.Play();
            }
            Debug.Log("playing");
        }
        else
        {
            fire3.Stop();
            if (rocketThrust.isPlaying)
            {
                rocketThrust.volume = 0.5f;
            }
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
