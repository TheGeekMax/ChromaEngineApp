using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSandbox : MonoBehaviour{
    public GameObject textObject;
    public GameObject imageObject;
    int id;

    public void UpdateData(int id){
        this.id = id;
        BlocData bloc = BlocManager.instance.FindBlocDataWithId(id);
        textObject.GetComponent<TextMeshProUGUI>().text = bloc.name;
        imageObject.GetComponent<Image>().sprite = bloc.prefab.GetComponent<SpriteRenderer>().sprite;
    }

    public int GetId(){
        return id;
    }

    public void OnClick(){
        if(SandboxManager.instance.toolId == 0){
            //select this bloc 
            SandboxManager.instance.Select(id);
            SandboxManager.instance.ToggleGui();
        }
    }

}
