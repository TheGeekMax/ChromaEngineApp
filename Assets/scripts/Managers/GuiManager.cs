using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour{
    [Header("GUI Elements")]
    public List<guiData> guiDataList;
    [Header("default open (-1 for none)")]
    public int defaultOpen = -1;

    [Header("Animation Data")]
    public float closeTime = 0.5f;

    public float openTime = 0.5f;

    public LeanTweenType openCurve = LeanTweenType.linear;
    public LeanTweenType closeCurve = LeanTweenType.linear;

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
            guiDataList[i].Element.SetActive(true);
            if(i == defaultOpen){
                currentOpen = i;
            }else{
                CloseFromDirection(guiDataList[i],guiDataList[i].dir,true);
            }
        }
    }

    public void Close(){
        if(currentOpen != -1 && guiDataList[currentOpen].closable){
            CloseFromDirection(guiDataList[currentOpen],guiDataList[currentOpen].dir);
            currentOpen = -1;
            open = false;
        }
    }

    public void Open(string name){
        if(currentOpen != -1 && (!guiDataList[currentOpen].closable || guiDataList[currentOpen].name == name)){
            return;
        }
        for(int i = 0; i < guiDataList.Count; i++){
            if(guiDataList[i].name == name){
                if(currentOpen != -1){
                    CloseFromDirection(guiDataList[currentOpen],guiDataList[currentOpen].dir);
                }
                OpenFromDirection(guiDataList[i],guiDataList[i].dir);
                currentOpen = i;
                open = true;
                return;
            }
        }
    }
    
    private void CloseFromDirection(guiData guiData,guiData.direction direction,bool instant = false){
        switch(direction){
            case guiData.direction.left:
                LeanTween.moveX(guiData.Element, guiData.Element.transform.position.x - Screen.width, instant ? 0 : closeTime)
                    .setEase(closeCurve);
                break;
            case guiData.direction.right:
                LeanTween.moveX(guiData.Element,guiData.Element.transform.position.x+Screen.width,instant?0:closeTime)
                    .setEase(closeCurve);
                break;
            case guiData.direction.up:
                LeanTween.moveY(guiData.Element,guiData.Element.transform.position.y+Screen.height,instant?0:closeTime)
                    .setEase(closeCurve);
                break;
            case guiData.direction.down:
                LeanTween.moveY(guiData.Element,guiData.Element.transform.position.y-Screen.height,instant?0:closeTime)
                    .setEase(closeCurve);
                break;
        }
    }
    
    private void OpenFromDirection(guiData guiData,guiData.direction direction,bool instant = false){
        switch(direction){
            case guiData.direction.left:
                LeanTween.moveX(guiData.Element,guiData.Element.transform.position.x+Screen.width,instant?0:openTime)
                    .setEase(openCurve);
                break;
            case guiData.direction.right:
                LeanTween.moveX(guiData.Element,guiData.Element.transform.position.x-Screen.width,instant?0:openTime)
                    .setEase(openCurve);
                break;
            case guiData.direction.up:
                LeanTween.moveY(guiData.Element,guiData.Element.transform.position.y-Screen.height,instant?0:openTime)
                    .setEase(openCurve);
                break;
            case guiData.direction.down:
                LeanTween.moveY(guiData.Element,guiData.Element.transform.position.y+Screen.height,instant?0:openTime)
                    .setEase(openCurve);
                break;
        }
    }
}
