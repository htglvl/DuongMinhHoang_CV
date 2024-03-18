using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class crosshairAndTitle : MonoBehaviour
{
    public float typingSpeed = 0.01f;

    public rocketRightSideUp rocketPos;
    public RawImage crossHair;
    private TMP_Text title;
    string oriText;
    float originalScaleYCanvas, titleOriWidth;
    public float thresholdCrosshairVector = 0.9f, rangeThreshold = 0.5f;
    [HideInInspector]
    public bool facingCamOrClose;
    public GameObject additionalPanel;
    private float oriAddiScaleX;
    // Update is called once per frame
    private void Awake()
    {
        originalScaleYCanvas = crossHair.transform.localScale.y;
    }
    private void Start()
    {
        title = GetComponentInChildren<TMP_Text>();
        oriText = title.text;
        titleOriWidth = title.rectTransform.rect.width;
        if (additionalPanel != null)
        {
            oriAddiScaleX = additionalPanel.transform.localScale.x;
        }
    }
    void Update()
    {
        float facingCamera = Vector3.Dot(Camera.main.transform.forward, (transform.position - rocketPos.transform.position).normalized);
        facingCamOrClose = (facingCamera > thresholdCrosshairVector || (transform.position - rocketPos.transform.position).magnitude < rangeThreshold) ? true : false;
        if (facingCamOrClose || rocketPos.inParent)
        {
            crossHair.gameObject.SetActive(true);
            if (additionalPanel != null)
            {
                additionalPanel.gameObject.SetActive(true);
            }
            if (crossHair.gameObject.activeSelf == true)
            {
                if (!rocketPos.inParent)
                {
                    title.text = oriText;
                }
                if (crossHair.gameObject.transform.localScale.y != originalScaleYCanvas && !LeanTween.isTweening(crossHair.gameObject))
                {
                    //crossHair.gameObject.transform.localScale = new Vector3(crossHair.gameObject.transform.localScale.x, 0, crossHair.gameObject.transform.localScale.z);
                    LeanTween.scaleY(crossHair.gameObject, originalScaleYCanvas, 0.2f);
                }
                if (additionalPanel != null)
                {
                    if (additionalPanel.transform.localScale.x != oriAddiScaleX  && !LeanTween.isTweening(additionalPanel))
                    {
                        LeanTween.scaleX(additionalPanel, oriAddiScaleX, 0.2f);
                    }
                }
            }
        }
        else
        {
            if (crossHair.gameObject.activeSelf == true)
            {
                title.text = "";
                if (crossHair.gameObject.transform.localScale.y != 0  && !LeanTween.isTweening(crossHair.gameObject))
                {
                    //crossHair.gameObject.transform.localScale = new Vector3(crossHair.gameObject.transform.localScale.x, originalScaleYCanvas, crossHair.gameObject.transform.localScale.z);
                    LeanTween.scaleY(crossHair.gameObject, 0, 0.2f).setOnComplete(() => crossHair.gameObject.SetActive(false));
                }
                if (additionalPanel != null)
                {
                    if (additionalPanel.transform.localScale.x != 0 && !LeanTween.isTweening(additionalPanel))
                    {
                        LeanTween.scaleX(additionalPanel, 0, 0.2f).setOnComplete(() => additionalPanel.gameObject.SetActive(false));
                    }
                }
            }
        }

    }
}
