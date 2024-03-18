
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Embed{
    public string Link;
}

[System.Serializable]
public struct mySelfField{
    public string Title;
    public string Name;
    public string Job;
    public string Objective;
    public string Email;
    public Embed Facebook;
    public Embed Linkedin;
    public string Address;
    public string Phone;
    public string DoB;
    public string Nationality;
    public Embed Github;
}

[System.Serializable]
public struct certElem{
    public string title;
    public Embed Link;
}

[System.Serializable]
public struct certField{
    public List<certElem> cert;
}

[System.Serializable]
public struct certificationsAndCoursesField{
    public string title;
    public certField cert;
}

[System.Serializable]
public struct languagesAndLevel{
    public string Language;
    public string Level;
}

[System.Serializable]
public struct skillsField{
    public List<string> Primary;
    public List<string> Other;
}

[System.Serializable]
public struct timeField{
    public string from;
    public string to;
}

[System.Serializable]
public struct educationField{
    public string Place;
    public Embed Website;
    public string Address;
    public string Job;
    public timeField Time;
    public string GPA;
}

[System.Serializable]
public struct projectsAndCompetitionsField{
    public string Name;
    public timeField Time; 
    public string Works;
}

[System.Serializable]
public struct experienceField{
    public string Place;
    public Embed Website;
    public string Address;
    public string Job;
    public timeField Time;
    public List<string> Description;
    public List<string> Works;
}

[System.Serializable]
public struct CV{
    public mySelfField myself;
    public List<experienceField> experience;
    public List<projectsAndCompetitionsField> projectsAndCompetitions;
    public List<educationField> education;
    public skillsField skills;
    public List<languagesAndLevel> languages;
    public certificationsAndCoursesField certificationsAndCourses;
}
