using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class exploreScript : MonoBehaviour
{
    public float typingSpeed = 0.01f;
    public rocketRightSideUp rocketRightSideUp;
    public spaceShipController spaceShipController;
    public TMP_Text planetTitle;
    private string originalPlanetTitle, addedPlanetExploreText;
    private rotateAroundPoint rotateAroundPoint;
    public GameObject panel;
    public Vector3 mobileOffsetPanel = new Vector3(-1, 1, 0);
    private bool previousInParent = false;
    private Coroutine displayCoroutine;
    private crosshairAndTitle crosshairAndTitle;

    float oriPanelScaleY;
    [HideInInspector]
    public bool selfPlanetExplored =false;

    // Start is called before the first frame update
    void Start()
    {
        if (panel != null)
        {
            oriPanelScaleY = panel.transform.localScale.y;
            panel.transform.localScale = new Vector3(panel.transform.localScale.x, 0, panel.transform.localScale.z);
            panel.SetActive(false);
        }
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
        crosshairAndTitle = GetComponent<crosshairAndTitle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketRightSideUp.inParent != previousInParent)
        {
            if (rocketRightSideUp.inParent)
            {
                if (rocketRightSideUp.planetPos.gameObject == gameObject)
                {
                    if (displayCoroutine != null)
                    {
                        StopCoroutine(displayCoroutine);
                    }
                    displayCoroutine = StartCoroutine(DisplayLine(planetTitle, originalPlanetTitle + addedPlanetExploreText));

                }
                previousInParent = true;
            }
            else
            {
                if (crosshairAndTitle.facingCamOrClose)
                {
                    if (displayCoroutine != null)
                    {
                        StopCoroutine(displayCoroutine);
                    }
                    displayCoroutine = StartCoroutine(DisplayLine(planetTitle, originalPlanetTitle));
                }
                else
                {
                    planetTitle.text = "";
                }

                rotateAroundPoint.explored = false;
                previousInParent = false;
            }
        }
        if (rocketRightSideUp.inParent)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                rotateAroundPoint.explored = true;
            }
        }
        else
        {
            rotateAroundPoint.explored = false;
        }


        if (panel != null)
        {
            if (rotateAroundPoint.explored && rocketRightSideUp.planetPos.gameObject == gameObject)
            {
                panel.SetActive(true);
                if (!LeanTween.isTweening(panel) && panel.transform.localScale.y != oriPanelScaleY)
                {
                    panel.transform.localScale = new Vector3(panel.transform.localScale.x, 0, panel.transform.localScale.x);
                    LeanTween.scaleY(panel, oriPanelScaleY, 0.2f);
                }
                selfPlanetExplored = true;
            }
            else
            {
                if (!LeanTween.isTweening(panel) && panel.transform.localScale.y != 0)
                {
                    panel.transform.localScale = new Vector3(panel.transform.localScale.x, oriPanelScaleY, panel.transform.localScale.x);
                    LeanTween.scaleY(panel, 0, 0.2f);
                }
                panel.SetActive(false);
            }
        }

    }
    private IEnumerator DisplayLine(TMP_Text text, string line)
    {
        text.text = "";
        foreach (char letter in line.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return text.text = line;
    }
}
