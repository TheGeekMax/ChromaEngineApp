using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonData : MonoBehaviour{
    public string levelName;
    public string levelCode;
    int stars;
    bool sandbox;
    public GameObject textObject;
    public GameObject imageObject;
    public string description;

    public void UpdateData(string levelName, string levelCode,int stars, LevelbuttonManager.LevelButtonState state, string description = ""){
        this.levelName = levelName;
        this.levelCode = levelCode;
        this.stars = stars;
        this.description = description;

        switch(state){
            case LevelbuttonManager.LevelButtonState.Unlocked:
                textObject.GetComponent<TextMeshProUGUI>().text = levelName;
                imageObject.GetComponent<Image>().sprite = LevelbuttonManager.instance.unlockedSprite;
                this.sandbox = false;
                break;
            case LevelbuttonManager.LevelButtonState.Completed:
                textObject.GetComponent<TextMeshProUGUI>().text = levelName;
                imageObject.GetComponent<Image>().sprite = LevelbuttonManager.instance.completedSprite;
                this.sandbox = false;
                break;
            case LevelbuttonManager.LevelButtonState.SandBox:
                textObject.GetComponent<TextMeshProUGUI>().text = levelName;
                imageObject.GetComponent<Image>().sprite = LevelbuttonManager.instance.sandBoxSprite;
                this.sandbox = true;
                break;
        }
    }

    public void Play(){
        KeepManager.instance.SetSandboxMode(sandbox);
        KeepManager.instance.SetStars(stars);
        KeepManager.instance.SetName(levelName);
        KeepManager.instance.SetDescription(description);
        KeepManager.instance.Play(levelCode);
    }
}
