using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetrotate : MonoBehaviour
{
    public float velocity = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f,velocity,0.0f, Space.Self);
    }
}
