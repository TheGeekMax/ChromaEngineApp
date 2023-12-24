using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SandboxManager : MonoBehaviour{
    public bool sandboxMode = false;
    public int toolId = 0;
    // 0 - add blocs
    // 1 - remove blocs
    // 2 - add/remove borders

    public GameObject tools;

    [Header("buttons sprites")]
    public Sprite addBlocSprite;
    public Sprite removeBlocSprite;
    public Sprite addBorderSprite;

    public GameObject toolButton;

    public GameObject addPanel;

    public GameObject label;

    public int currentId = 0;

    [Header("Gui")]
    public bool open = true;
    public GameObject laserGUI;   // 0
    public GameObject blockGUI;   // 1
    public GameObject lightGUI;   // 2
    public GameObject balloonGUI; // 3
    public GameObject prefabButton;

    int currentCategory = 0;

    [Header("Buttons sprites")]
    public Sprite tabOn;
    public Sprite tabOff;
    public GameObject laserButton;
    public GameObject blockButton;
    public GameObject lightButton;
    public GameObject balloonButton;
    public GameObject description;

    public static SandboxManager instance;

    void Awake(){
        instance = this;
    }

    void Start(){
        ToggleGui();
        UpdateTab();
        //on ajoute les boutons
        for(int i = 0; i < BlocManager.instance.blocs.Count; i++){
            GameObject go;
            switch(BlocManager.instance.blocs[i].category){
                case BlocManager.Category.Lasers:
                    go = Instantiate(prefabButton, laserGUI.transform);
                    break;
                case BlocManager.Category.Blocks:
                    go = Instantiate(prefabButton, blockGUI.transform);
                    break;
                case BlocManager.Category.Lights:
                    go = Instantiate(prefabButton, lightGUI.transform);
                    break;
                case BlocManager.Category.Balloons:
                    go = Instantiate(prefabButton, balloonGUI.transform);
                    break;
                default:
                    go = Instantiate(prefabButton, laserGUI.transform);
                    break;
            }
            go.GetComponent<ButtonSandbox>().UpdateData(i);
        }
    }

    public void UpdateSandboxMod(){
        Debug.Log("UpdateSandboxMod");
        if(sandboxMode){
            tools.SetActive(true);
            description.SetActive(false);
        }else{
            tools.SetActive(false);
            description.SetActive(true);
        }
    }

    
    void UpdateSprite(){
        Debug.Log("UpdateSprite");
        if(toolId == 0){
            toolButton.GetComponent<Image>().sprite = addBlocSprite;
            addPanel.SetActive(true);
        }else if(toolId == 1){
            toolButton.GetComponent<Image>().sprite = removeBlocSprite;
            addPanel.SetActive(false);
            GuiManager.instance.Close();
        }else if(toolId == 2){
            toolButton.GetComponent<Image>().sprite = addBorderSprite;
            addPanel.SetActive(false);
        }
    }

    public void UpdateTool(){
        toolId = (toolId+1)%3;
        UpdateSprite();
    }

    public void SetId(int id){
        currentCategory = id;
        UpdateTab();
    }

    public void UpdateTab(){
        Debug.Log("UpdateTab");
        laserGUI.SetActive(false);
        blockGUI.SetActive(false);
        lightGUI.SetActive(false);
        balloonGUI.SetActive(false);
        laserButton.GetComponent<Image>().sprite = tabOff;
        blockButton.GetComponent<Image>().sprite = tabOff;
        lightButton.GetComponent<Image>().sprite = tabOff;
        balloonButton.GetComponent<Image>().sprite = tabOff;
        if(currentCategory == 0){
            laserGUI.SetActive(true);
            laserButton.GetComponent<Image>().sprite = tabOn;
        }else if(currentCategory == 1){
            blockGUI.SetActive(true);
            blockButton.GetComponent<Image>().sprite = tabOn;
        }else if(currentCategory == 2){
            lightGUI.SetActive(true);
            lightButton.GetComponent<Image>().sprite = tabOn;
        }else if(currentCategory == 3){
            balloonGUI.SetActive(true);
            balloonButton.GetComponent<Image>().sprite = tabOn;
        }
    }

    public void Select(int id){
        currentId = id;
        label.GetComponent<TextMeshProUGUI>().text = BlocManager.instance.blocs[id].name;
    }

    public void ToggleGui(){
        open = !open;
        if(open){
            GuiManager.instance.Open("sandbox");
        }else{
            GuiManager.instance.Close();
        }
    }

    public void PlaceBlock(){
        //on place le block
        GetComponent<GridManager>().AddBlocToNearest(GetComponent<BlocManager>().FindBlocDataWithId(currentId).name);
        GetComponent<LaserManager>().GenerateLasers();
    }
}
