using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[ExecuteInEditMode]
public class GroundPlacer : MonoBehaviour
{
    public GameObject ground;
    public int numberOfColumn = 50, numberOfRow = 50;
    public bool build = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (build)
        {
            build = false;
            for (int i = 0; i < numberOfColumn; i++)
            {
                for (int j = 0; j < numberOfRow; j++)
                {
                    var newGround = Instantiate(ground, new Vector3(i * 5, 0, j * 5), quaternion.identity);
                    newGround.transform.parent = gameObject.transform;
                }
            }
        }

    }
}
