using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCopyManager : MonoBehaviour{
    public static LevelCopyManager instance;

    public GameObject textComponent;
    void Awake(){
        if(instance == null){
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    void Update(){
        if(!SandboxManager.instance.sandboxMode){
            return;
        }

        //on copie l'id du niveau et on l'affiche dans le TextComponent
        textComponent.GetComponent<TextMeshProUGUI>().text = GetComponent<ImportManager>().levelCode;
    }

    public void CopyToClipboard(){
        TextEditor te = new TextEditor();
        te.text = GetComponent<ImportManager>().levelCode;
        te.SelectAll();
        te.Copy();
    }
}
