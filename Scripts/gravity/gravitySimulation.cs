using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravitySimulation : MonoBehaviour
{
    [SerializeField] float g = 1f, distancePower = 2;
    static float gcons, distPower;
    public static List<Rigidbody> attractors = new List<Rigidbody>();
    public static List<Rigidbody> attractees = new List<Rigidbody>();
    public static bool isSimulatingLive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gcons = g;
        distPower = distancePower;
        if (isSimulatingLive)
        {
            SimuateGravities();
        }
    }

    public static void SimuateGravities(){
        foreach (Rigidbody attractor in attractors)
        {
            foreach (Rigidbody attractee in attractees)
            {
                if (attractor != attractee)
                {
                    AddGravityForce(attractor, attractee);
                }
            }
        }
    }

    public static void AddGravityForce(Rigidbody attractor, Rigidbody attractee){
        Vector3 difference = attractor.position - attractee.position;
        float massProduct = attractor.mass * attractee.mass;
        float distance = difference.magnitude;
        float forceMagnitude = (massProduct * gcons)/Mathf.Pow(distance,distPower);
        Vector3 forceDirection = difference.normalized;
        attractee.AddForce(forceDirection*forceMagnitude);

    }
}
