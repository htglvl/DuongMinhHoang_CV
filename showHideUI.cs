using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showHideUI : MonoBehaviour
{
    public Canvas canvas;
    public float threshold = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float facingCamera = Vector3.Dot(Camera.main.transform.forward, canvas.transform.forward);
        //Debug.Log(facingCamera);
        if (facingCamera> threshold)
        {
            canvas.gameObject.SetActive(true);
        }else if (facingCamera < -threshold)
        {
            canvas.gameObject.SetActive(true);
            canvas.transform.Rotate(new Vector3(0, 180f, 0));
        } else
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
