using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//i want to use PrefabUtility
using UnityEditor;

public class BlocManager : MonoBehaviour{
    public List<BlocData> blocs;

    public static BlocManager instance;

    public enum Category{
        Lasers,
        Blocks,
        Lights,
        Balloons
    }

    void Awake(){
        if(instance == null){
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public GameObject FindBloc(string name){
        foreach(BlocData bloc in blocs){
            if(bloc.name == name){
                return bloc.prefab;
            }
        }
        return null;
    }

    public BlocData FindBlocData(string name){
        foreach(BlocData bloc in blocs){
            if(bloc.name == name){
                return bloc;
            }
        }
        return null;
    }

    public int FindBlocId(string name){
        for(int i = 0; i < blocs.Count; i++){
            if(blocs[i].name == name){
                return i;
            }
        }
        return -1;
    }

    public BlocData FindBlocDataWithId(int id){
        //return nth's bloc
        return blocs[id];
    }

    public BlocData FindBlocDataWithGameObject(GameObject go){
        //Debug.Log("FindBlocDataWithGameObject");
        foreach(BlocData bloc in blocs){
            if(bloc.prefab == go){
                return bloc;
            }
        }
        return null;
    }

    public int FindBlocIdWithGameObject(GameObject go){
        //Debug.Log("FindBlocIdWithGameObject");
        for(int i = 0; i < blocs.Count; i++){
            if(blocs[i].prefab == go){
                return i;
            }
        }
        return -1;
    }

    public int GetLength(){
        return blocs.Count;
    }
}
