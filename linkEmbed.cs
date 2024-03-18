using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;


public class linkEmbed : MonoBehaviour
{
    
    public classToText classToText;
    public void Facebook(){
        Application.OpenURL(classToText.cv.myself.Facebook.Link);
    }

    public void Email(){
        Application.OpenURL("mailto:" + classToText.cv.myself.Email);
    }

    public void Linkedin(){
        Application.OpenURL(classToText.cv.myself.Linkedin.Link);
    }

    public void Github(){
        Application.OpenURL(classToText.cv.myself.Github.Link);
    }
    public void Viettel(){
        Application.OpenURL(classToText.cv.experience[0].Website.Link);
    }
    public void Instagram(){
        Application.OpenURL(classToText.cv.experience[1].Website.Link);
    }
    public void FPT(){
        Application.OpenURL(classToText.cv.education[0].Website.Link);
    }
    public void Kaeru(){
        Application.OpenURL(classToText.cv.experience[1].Website.Link);
    }
}
