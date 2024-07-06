using System;
using UnityEngine;
using UnityEngine.UI;

public class AndroidButtonManager:MonoBehaviour{
    public static AndroidButtonManager instance;
    
    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GuiManager.instance.open){
                GuiManager.instance.Close();
            }
            else{
                GuiManager.instance.Open("settings");
            }
        }
    }
}