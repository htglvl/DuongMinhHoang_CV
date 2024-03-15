using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.EventSystems;

public class rotateAroundPoint : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public Transform target;
    private Vector3 privateTarget;
    public float range = 5, to3rdCamSpeed = 1f, toPlanetTimeSpeed = 5f;
    private Vector3 previousPosition;
    public Transform ThirdPersonCam;
    public GameObject Rocket;
    private Rigidbody RocketRB;
    private rocketRightSideUp rocketRightSideUp;
    public float planetRange = 200f, scrollSpeed = 2;
    private float cameraRange = 120, addedRange = 0, zoom;
    private bool touchRotateDown, touchRotate;

    public float varianceInDistances = 5.0f, minPinchSpeed = 5.0f;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        RocketRB = Rocket.GetComponent<Rigidbody>();
        rocketRightSideUp = Rocket.GetComponent<rocketRightSideUp>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(EventSystem.current.IsPointerOverGameObject());
        if (Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject() && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchRotateDown = Touch.activeFingers[0].currentTouch.began;
            touchRotate = Touch.activeFingers[0].currentTouch.inProgress;
        }

        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            touchRotateDown = false;
            touchRotate = false; //to prevent rotate when zooming
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

        }else
        {
            zoom = 0;
        }



        if (rocketRightSideUp.inParent == false)
        {
            privateTarget = Vector3.Lerp(privateTarget, target.position, Time.deltaTime * toPlanetTimeSpeed);
            cameraRange = Mathf.Lerp(cameraRange, range, Time.deltaTime * toPlanetTimeSpeed);
        }
        else
        {
            privateTarget = Vector3.Lerp(privateTarget, rocketRightSideUp.planetPos.position, Time.deltaTime * toPlanetTimeSpeed);
            cameraRange = Mathf.Lerp(cameraRange, planetRange, Time.deltaTime * toPlanetTimeSpeed);
        }
        if (RocketRB.velocity.magnitude < 0.3)
        {
            if (Input.GetMouseButtonDown(0) || touchRotateDown)
            {
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0) || touchRotate)
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
            privateTarget = Vector3.Lerp(privateTarget, ThirdPersonCam.position, Time.deltaTime * toPlanetTimeSpeed);
            cameraRange = Mathf.Lerp(cameraRange, 0, Time.deltaTime * toPlanetTimeSpeed * 2);
            // transform.position = Vector3.Lerp(cam.transform.position, ThirdPersonCam.position, Time.deltaTime * to3rdCamSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, ThirdPersonCam.rotation, Time.deltaTime * to3rdCamSpeed);
        }
        cam.transform.position = privateTarget;
        cam.transform.Translate(new Vector3(0, 0, -cameraRange - addedRange));
    }

}
