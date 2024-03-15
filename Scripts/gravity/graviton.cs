using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class graviton : MonoBehaviour
{
    Rigidbody rb;
    public bool IsAttractor
    {
        get
        {
            return isAttractor;
        }
        set
        {
            if (value == true)
            {
                if (!gravitySimulation.attractors.Contains(this.GetComponent<Rigidbody>()))
                {
                    gravitySimulation.attractors.Add(rb);
                }
            }
            else if(value == false){
                gravitySimulation.attractors.Remove(rb);
            }
            isAttractor = value;
        }
    }
    public bool IsAttractee
    {
        get
        {
            return isAttractee;
        }
        set
        {
            if (value == true)
            {
                if (!gravitySimulation.attractees.Contains(this.GetComponent<Rigidbody>()))
                {
                    gravitySimulation.attractees.Add(rb);
                }
            }
            else if (value == false)
            {
                gravitySimulation.attractees.Remove(rb);
            }
            isAttractee = value;
        }
    }//property?
    [SerializeField] bool isAttractor, isAttractee;

    [SerializeField] Vector3 initialVelocity;
    [SerializeField] bool applyInitialVelocityOnStart;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        IsAttractor = isAttractor;
        IsAttractee = isAttractee;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (applyInitialVelocityOnStart)
        {
            ApplyVelocity(initialVelocity);
        }
    }
    private void OnDisable()
    {

    }
    void ApplyVelocity(Vector3 velocity)
    {
        rb.AddForce(initialVelocity, ForceMode.Impulse);
    }
}
