using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class exploreScript : MonoBehaviour
{
    public rocketRightSideUp rocketRightSideUp;
    public spaceShipController spaceShipController;
    public TMP_Text planetTitle;
    private string originalPlanetTitle, addedPlanetExploreText;
    private rotateAroundPoint rotateAroundPoint;
    public GameObject panel;
    public Vector3 mobileOffsetPanel = new Vector3(-1, 1, 0);

    // Start is called before the first frame update
    void Start()
    { if (panel != null) {panel.SetActive(false);}
        rotateAroundPoint = Camera.main.GetComponent<rotateAroundPoint>();
        originalPlanetTitle = planetTitle.text;
        if (Application.isMobilePlatform || spaceShipController.isTestingMobile)
        {
            addedPlanetExploreText = "\nTap to Explore...";
            panel.transform.position += mobileOffsetPanel;
        }
        else
        {
            addedPlanetExploreText = "\nPress Enter to Explore...";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketRightSideUp.inParent)
        {
            if (rocketRightSideUp.planetPos.gameObject == gameObject)
            {
                planetTitle.text = originalPlanetTitle + addedPlanetExploreText;
                if (Input.GetKey(KeyCode.Return))
                {
                    rotateAroundPoint.explored = true;

                }
            }
        }
        else
        {
            planetTitle.text = originalPlanetTitle;
            rotateAroundPoint.explored = false;
        }

        if (panel != null)
        {
            if (rotateAroundPoint.explored && rocketRightSideUp.planetPos.gameObject == gameObject)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }

    }
}
