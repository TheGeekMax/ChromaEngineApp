using UnityEngine;
using System;

[System.Serializable]
public class SaveData{
    public string[] finished_codes;
    public int stars;

    public SaveData(Keep data){
        finished_codes = data.finished_codes.ToArray();
        stars = data.starCount;
    }
}
