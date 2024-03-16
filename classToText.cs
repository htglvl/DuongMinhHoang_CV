using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class classToText : MonoBehaviour
{
    [SerializeField] public TextAsset jsonFile;
    [SerializeField] public CV cv;
    // Start is called before the first frame update
    void Start()
    {
        cv = JsonUtility.FromJson<CV>(jsonFile.text);
        mySelfField mySelf = cv.myself;
        List<experienceField> experience = cv.experience;
        List<educationField> education = cv.education;
        skillsField skills = cv.skills;
        List<languagesAndLevel> languages = cv.languages;
        certificationsAndCoursesField certificationsAndCourses = cv.certificationsAndCourses;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
