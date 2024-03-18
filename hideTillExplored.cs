using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideTillExplored : MonoBehaviour
{
    private exploreScript motherPlanet;
    private Vector3 localScale;
    public GameObject BuildingParticle;
    private bool unexplored = true;
    // Start is called before the first frame update
    void Start()
    {
        motherPlanet = transform.parent.gameObject.GetComponent<exploreScript>();
        localScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (motherPlanet.selfPlanetExplored && unexplored)
        {
            unexplored = false;
            Instantiate(BuildingParticle, transform.position, Quaternion.identity);
            LeanTween.scale(gameObject, localScale, 3f);
        }
    }
}
