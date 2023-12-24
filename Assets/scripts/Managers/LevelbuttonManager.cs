using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelbuttonManager : MonoBehaviour{
    // Start is called before the first frame update
    public Sprite unlockedSprite;
    public Sprite completedSprite;
    public Sprite sandBoxSprite;

    public GameObject levelButtonPrefab;
    public GameObject cityButtonPrefab;
    public GameObject parentButton;

    [Header("buttons")]
    public GameObject backButton;

    public static LevelbuttonManager instance;

    public enum LevelButtonState{
        Unlocked,
        Completed,
        SandBox
    }

    Dictionary<string, CityData> Cities;
    Dictionary<string, LevelData> levelDataDict;


    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        levelDataDict = new Dictionary<string, LevelData>();
        Cities = new Dictionary<string, CityData>();
    }

    void Start(){
        LevelLoaderManager.instance.LoadData();
        if(Keep.instance != null){
            if(Keep.instance.currentCity != ""){
                CityOnClick(Keep.instance.currentCity);
            }
        }
    }

    // Onclick functions
    public void CityOnClick(string cityName){
        Keep.instance.currentCity = cityName;
        Debug.Log("CityOnClick: " + cityName);
        ShowLevels(cityName);
    }

    public void Back(){
        Keep.instance.currentCity = "";
        ShowCities();
    }

    //fonctions d'affichages boutons 
    void ResetButtons(){
        foreach(Transform child in parentButton.transform){
            Destroy(child.gameObject);
        }
    }

    public void ShowCities(){
        ResetButtons();
        backButton.SetActive(false);
        foreach(KeyValuePair<string, CityData> city in Cities){
            GameObject cityButton = Instantiate(cityButtonPrefab, parentButton.transform);
            cityButton.GetComponent<CityButton>().SetName(city.Value.name);
            cityButton.GetComponent<CityButton>().SetStars(city.Value.starReq);
        }
    }

    void ShowLevels(string cityName){
        ResetButtons();
        backButton.SetActive(true);
        foreach(KeyValuePair<string, LevelData> levelData in levelDataDict){
            //Debug.Log(levelData.Key + " " + levelData.Value.cityName);
            if(levelData.Value.cityName == cityName){
                GameObject levelButton = Instantiate(levelButtonPrefab, parentButton.transform);
                levelButton.GetComponent<ButtonData>().UpdateData(levelData.Value.levelName, levelData.Value.levelCode, levelData.Value.stars,levelData.Value.state, levelData.Value.description);
            }
        }
        parentButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-1500);
    }

    //fonction d'ajout backend

    public void AddCity(string cityName,int starReq){
        if(!Cities.ContainsKey(cityName)){
            Cities.Add(cityName,new CityData(cityName,starReq));
        }
    }

    public void AddLevelUnlocked(string levelName, string city, string levelCode, int stars = 0, string description = ""){
        if(!levelDataDict.ContainsKey(levelName)){
            levelDataDict.Add(levelName, LevelData.DefaultLevel(levelName, city,levelCode, stars, description));
        }
    }

    public void AddLevelCompleted(string levelName, string city,string levelCode, int stars = 0, string description = ""){
        if(!levelDataDict.ContainsKey(levelName)){
            levelDataDict.Add(levelName, LevelData.DefaultCompleted(levelName, city, levelCode,stars, description));
        }
    }

    public void AddLevelSandBox(string levelName, string city, string levelCode, int stars = 0){
        if(!levelDataDict.ContainsKey(levelName)){
            levelDataDict.Add(levelName, LevelData.DefaultSandBox(levelName, city, levelCode,stars));
        }
    }
}
