using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour{
    [Header("GUI Elements")]
    public List<guiData> guiDataList;
    [Header("default open (-1 for none)")]
    public int defaultOpen = -1;

    int currentOpen = -1;

    public bool open = false;

    public static GuiManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start(){
        for(int i = 0; i < guiDataList.Count; i++){
            if(i == defaultOpen){
                guiDataList[i].Element.SetActive(true);
                currentOpen = i;
            }else{
                guiDataList[i].Element.SetActive(false);
            }
        }
    }

    public void Close(){
        if(currentOpen != -1 && guiDataList[currentOpen].closable){
            guiDataList[currentOpen].Element.SetActive(false);
            currentOpen = -1;
            open = false;
        }
    }

    public void Open(string name){
        if(currentOpen != -1 && !guiDataList[currentOpen].closable){
            return;
        }
        for(int i = 0; i < guiDataList.Count; i++){
            if(guiDataList[i].name == name){
                if(currentOpen != -1){
                    guiDataList[currentOpen].Element.SetActive(false);
                }
                guiDataList[i].Element.SetActive(true);
                currentOpen = i;
                open = true;
                return;
            }
        }
    }
}
