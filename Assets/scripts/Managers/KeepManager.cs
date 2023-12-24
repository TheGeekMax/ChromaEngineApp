using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeepManager : MonoBehaviour{
    [HideInInspector]
    public GameObject keepObject;

    //text qui affiche le nom du niveau
    public TextMeshProUGUI starNameText;

    public static KeepManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start(){
        keepObject = GameObject.Find("Keep");
        starNameText.text = Keep.instance.starCount.ToString();
    }

    public void SetSandboxMode(bool value){
        keepObject.GetComponent<Keep>().SandBoxMode = value;
    }

    public void SetStars(int value){
        keepObject.GetComponent<Keep>().Stars = value;
    }

    public void Play(string levelCode){
        keepObject.GetComponent<Keep>().LevelCode = levelCode;
        keepObject.GetComponent<Keep>().Play();
    }

    public void SetName(string name){
        keepObject.GetComponent<Keep>().Name = name;
    }

    public void SetDescription(string description){
        keepObject.GetComponent<Keep>().Description = description;
    }

}
