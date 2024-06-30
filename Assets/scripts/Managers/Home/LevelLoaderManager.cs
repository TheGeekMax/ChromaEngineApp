using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//pour charger un json
using System.IO;

public class LevelLoaderManager : MonoBehaviour{
    //instance
    public static LevelLoaderManager instance;

    //ficier json a mettre dans l'editor
    public TextAsset levelData;


    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void ResetPlayer(){
        SaveSystem.ResetPlayer();
        //on kill le Keep object
        Destroy(Keep.instance.gameObject);
        //on reload la scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
   
    }

    public void LoadCustomPlayScene(){
        //on charge le niveau avec le code
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void LoadData(){
        //etape 1 : charger les données (dans le dossier parent le Levels.json)
        string jsonString = levelData.text;
        
        //etape 2 : charger les données
        LevelParser levelParser = JsonUtility.FromJson<LevelParser>(jsonString);

        //etape 2.5 : on edit les niveaux completé via le keep
        foreach(string code in Keep.instance.finished_codes){
            foreach(LevelInfos level in levelParser.levels){
                if(level.levelCode == code){
                    level.completed = true;
                }
            }
        }

        //etape 3 : charger les boutons des villes
        //3.1 : ajouter les villes
        foreach(CityInfo city in levelParser.cities){
            LevelbuttonManager.instance.AddCity(city.name,city.starcount);
        }
        //3.2 : ajouter les niveaux
        foreach(LevelInfos level in levelParser.levels){
            if(level.sandbox){
                LevelbuttonManager.instance.AddLevelSandBox(level.name,levelParser.cities[level.city].name,level.levelCode,level.stars);
            }
            else{
                if(level.completed){
                    LevelbuttonManager.instance.AddLevelCompleted(level.name,levelParser.cities[level.city].name,level.levelCode,level.stars,level.description);
                }
                else{
                    LevelbuttonManager.instance.AddLevelUnlocked(level.name,levelParser.cities[level.city].name,level.levelCode,level.stars,level.description);
                }
            }
        }

        //etape 4 : afficher les villes
        LevelbuttonManager.instance.ShowCities();
    }

}
 