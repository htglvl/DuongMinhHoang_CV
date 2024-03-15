using UnityEngine;


public class SceneLikeCamera : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float shiftMoveSpeed = 3.0f;
    private float addSpeed;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float zoomSpeed = 10.0f;

    //Cache last pos and rot be able to undo last focus object action.
    Quaternion prevRot = new Quaternion();
    Vector3 prevPos = new Vector3();

    [Header("Axes Names")]
    [SerializeField, Tooltip("Otherwise known as the vertical axis")] private string mouseY = "Mouse Y";
    [SerializeField, Tooltip("AKA horizontal axis")] private string mouseX = "Mouse X";
    [SerializeField, Tooltip("The axis you want to use for zoom.")] private string zoomAxis = "Mouse ScrollWheel";

    [Header("Move Keys")]
    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode upKey = KeyCode.E;
    [SerializeField] private KeyCode downKey = KeyCode.Q;

    [Header("Flat Move"), Tooltip("Instead of going where the camera is pointed, the camera moves only on the horizontal plane (Assuming you are working in 3D with default preferences).")]
    [SerializeField] private KeyCode flatMoveKey = KeyCode.LeftShift;

    [Header("Anchored Movement"), Tooltip("By default in scene-view, this is done by right-clicking for rotation or middle mouse clicking for up and down")]
    [SerializeField] private KeyCode anchoredMoveKey = KeyCode.Mouse2;

    [SerializeField] private KeyCode anchoredRotateKey = KeyCode.Mouse1;

    private void Start()
    {
        SavePosAndRot();
    }
    private void LateUpdate()
    {
        Vector3 move = Vector3.zero;
        
        //Move and rotate the camera
    
        if (Input.GetKey(forwardKey))
            move += Vector3.forward * moveSpeed * addSpeed;
        if (Input.GetKey(backKey))
            move += Vector3.back * moveSpeed * addSpeed;
        if (Input.GetKey(leftKey))
            move += Vector3.left * moveSpeed * addSpeed;
        if (Input.GetKey(rightKey))
            move += Vector3.right * moveSpeed * addSpeed;
        if (Input.GetKey(upKey))
            move += Vector3.up * moveSpeed * addSpeed;
        if (Input.GetKey(downKey))
            move += Vector3.down * moveSpeed * addSpeed;

        //By far the simplest solution I could come up with for moving only on the Horizontal plane - no rotation, just cache y
        if (Input.GetKey(flatMoveKey))
        {
            addSpeed = shiftMoveSpeed;
        }else{
            addSpeed = 1;
        }

        float mouseMoveY = Input.GetAxis(mouseY);
        float mouseMoveX = Input.GetAxis(mouseX);

        //Move the camera when anchored
        if (Input.GetKey(anchoredMoveKey)) 
        {
            move += Vector3.up * mouseMoveY * -moveSpeed;
            move += Vector3.right * mouseMoveX * -moveSpeed;
        }

        //Rotate the camera when anchored
        if (Input.GetKey(anchoredRotateKey)) 
        {
            transform.RotateAround(transform.position, transform.right, mouseMoveY * -rotationSpeed);
            transform.RotateAround(transform.position, Vector3.up, mouseMoveX * rotationSpeed);
        }

        transform.Translate(move);
        
        //Scroll to zoom
        float mouseScroll = Input.GetAxis(zoomAxis);
        transform.Translate(Vector3.forward * mouseScroll * zoomSpeed);
    }

    private void SavePosAndRot() 
    {
        prevRot = transform.rotation;
        prevPos = transform.position;
    }
}
