using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class applyCenterOfMass : MonoBehaviour
{
    private Rigidbody rb;
    public Transform centerOfMass;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = centerOfMass.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
