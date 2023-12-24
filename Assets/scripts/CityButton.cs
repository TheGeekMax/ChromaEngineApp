using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CityButton : MonoBehaviour{
    public string name;
    public int starcount;
    public TextMeshProUGUI cityName;
    public TextMeshProUGUI cityStars;
    public bool isUnlocked;

    [Header("sprite")]
    public Sprite locked;
    public Sprite unlocked;

    public void SetName(string name){
        this.name = name;
        cityName.text = name;
    }

    public void SetStars(int starcount){
        this.starcount = starcount;
        cityStars.text = starcount.ToString();
        if(Keep.instance.starCount >= starcount){
            //change from Image
            GetComponent<Image>().sprite = unlocked;
            isUnlocked = true;
        }else{
            GetComponent<Image>().sprite = locked;
            isUnlocked = false;
        }
    }

    public void Play(){
        if(isUnlocked)
            LevelbuttonManager.instance.CityOnClick(name);
    }
}
