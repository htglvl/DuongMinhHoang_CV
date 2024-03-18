using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class rotateAroundPoint : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public Transform target;
    private Vector3 privateTarget, Offset, planetOffset;
    public float range = 5, to3rdCamSpeed = 1f, toPlanetTimeSpeed = 5f;
    private Vector3 previousPosition;
    public Transform ThirdPersonCam;
    public GameObject Rocket;
    public bool explored;
    private Rigidbody RocketRB;
    private rocketRightSideUp rocketRightSideUp;
    private spaceShipController spaceShipController;
    public float planetRange = 200f, scrollSpeed = 2, exploredZoomMobile, exploredZoomDesktop;
    public Vector3 planetOffsetMobile, planetOffsetDesktop;
    private float cameraRange = 120, addedRange = 0, zoom, exploredZoom;
    private bool touchRotateDown, touchRotate, zooming = false;
    public float varianceInDistances = 5.0f, minPinchSpeed = 5.0f, mobileAddedRange = 2f;
    private bool isTouchingJoystick;

    // public TMP_Text pointerText, mouseButtonText, mousebuttonDownText;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        spaceShipController = Rocket.GetComponent<spaceShipController>();
        if (Application.isMobilePlatform || spaceShipController.isTestingMobile)
        {
            planetOffset = planetOffsetMobile;
            exploredZoom = exploredZoomMobile;

        }
        else
        {
            planetOffset = planetOffsetDesktop;
            exploredZoom = exploredZoomDesktop;
            mobileAddedRange = 1f;
        }
        Offset = Vector3.zero;
        RocketRB = Rocket.GetComponent<Rigidbody>();
        rocketRightSideUp = Rocket.GetComponent<rocketRightSideUp>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(EventSystem.current.IsPointerOverGameObject());
        // pointerText.text = "IsPointerOverGameObject " + EventSystem.current.IsPointerOverGameObject().ToString();
        // mouseButtonText.text = "mouse button " + Input.GetMouseButton(0).ToString();
        // mousebuttonDownText.text = "mouse button down " + Input.GetMouseButtonDown(0).ToString();
        // Debug.Log(Input.GetMouseButtonDown(0));
        /*!EventSystem.current.IsPointerOverGameObject() &&*/
        if (Input.touchCount == 1 && !spaceShipController.joystick.FingerIsDown && !zooming)
        {
            touchRotateDown = Touch.activeFingers[0].currentTouch.began;
            touchRotate = Touch.activeFingers[0].currentTouch.inProgress;
        }

        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            zooming = true;
            Vector2 curDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
            Vector2 prevDist = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
            float touchDelta = curDist.magnitude - prevDist.magnitude;
            float speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
            float speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
            if ((touchDelta + varianceInDistances <= 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
            {
                zoom = -1;
            }

            if ((touchDelta + varianceInDistances > 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
            {
                zoom = 1;
            }

        }
        else
        {
            zoom = 0;
        }
        if (Input.touchCount == 0)
        {
            zooming = false;
        }



        if (rocketRightSideUp.inParent == false) //in outer space
        {
            spaceShipController.exploreButton.gameObject.SetActive(false);
            privateTarget = Vector3.Slerp(privateTarget, target.position, toPlanetTimeSpeed);
            cameraRange = Mathf.LerpAngle(cameraRange, range, toPlanetTimeSpeed);
            Offset = Vector3.Slerp(Offset, Vector3.zero, toPlanetTimeSpeed * Time.smoothDeltaTime);
        }
        else //stay in some planet
        {
            spaceShipController.exploreButton.gameObject.SetActive(Application.isMobilePlatform || spaceShipController.isTestingMobile);
            if (explored)
            {
                // Offset = planetOffset;
                Offset = Vector3.Slerp(Offset, planetOffset, Time.deltaTime * toPlanetTimeSpeed);
                cameraRange = Mathf.Lerp(cameraRange, planetRange * rocketRightSideUp.planetPos.localScale.x * exploredZoom * mobileAddedRange, Time.deltaTime * toPlanetTimeSpeed);
            }
            else
            {
                privateTarget = Vector3.Slerp(privateTarget, rocketRightSideUp.planetPos.position, Time.deltaTime * toPlanetTimeSpeed);
                cameraRange = Mathf.Lerp(cameraRange, planetRange * rocketRightSideUp.planetPos.localScale.x * mobileAddedRange, Time.deltaTime * toPlanetTimeSpeed);
                Offset = Vector3.Slerp(Offset, Vector3.zero, Time.smoothDeltaTime * toPlanetTimeSpeed);
            }
        }
        if (RocketRB.velocity.magnitude < 0.3)
        {
            if ((Input.GetMouseButtonDown(0) && !Application.isMobilePlatform) || touchRotateDown)
            {
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            if ((Input.GetMouseButton(0) && !Application.isMobilePlatform) || touchRotate)
            {
                Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
                cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180);
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f || zoom > 0f)
            {
                addedRange += 1f * scrollSpeed;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f || zoom < 0f)
            {
                addedRange -= 1f * scrollSpeed;
            }
        }
        else
        {
            privateTarget = Vector3.Slerp(privateTarget, ThirdPersonCam.position, Time.smoothDeltaTime * toPlanetTimeSpeed);
            cameraRange = Mathf.LerpAngle(cameraRange, 0, Time.deltaTime * toPlanetTimeSpeed * 2);
            //transform.position = Vector3.Lerp(cam.transform.position, ThirdPersonCam.position, Time.deltaTime * to3rdCamSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, ThirdPersonCam.rotation, Time.smoothDeltaTime * to3rdCamSpeed);
        }
        cam.transform.position = privateTarget;
        cam.transform.Translate(new Vector3(0, 0, -cameraRange - addedRange));
        cam.transform.Translate(Offset);
    }

    public void setExplored()
    {
        explored = true;
    }

}
