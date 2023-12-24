using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    GameObject[,] grid;
    [HideInInspector]
    public int[,] ids;
    int suspended = 0;

    public SizeData sizeData;
    //[HideInInspector]
    public int gridWidth;

    public GameObject Parent;

    public static GridManager instance;

    public GameObject baloonParticle;

    void Awake(){
        instance = this;
    }

    public void Init(){
        switch (sizeData)
        {
            case SizeData.Small_4x4:
                gridWidth = 4;
                break;
            case SizeData.Medium_6x6:
                gridWidth = 6;
                break;
            case SizeData.Large_8x8:
                gridWidth = 8;
                break;
        }

        grid = new GameObject[gridWidth, gridWidth];
        ids = new int[gridWidth, gridWidth];
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                grid[i, j] = null;
                ids[i, j] = -1;
            }
        }

        GetComponent<CameraManager>().Init();
        GetComponent<LaserManager>().Init();
        GetComponent<BorderManager>().Init();
    }
    
    void Start(){
        
        if(GetComponent<ImportManager>().entered){
            GetComponent<ImportManager>().Decode();
            GetComponent<LaserManager>().GenerateLasers();
            return;
        }
        Init();

        //on ajoute le border
        for(int i = 0; i < gridWidth; i++){
            for(int j = 0; j < gridWidth; j++){
                GetComponent<BorderManager>().AddBlocToBorder(i,j);
            }
        }

        GetComponent<ImportManager>().Encode();
        
        GetComponent<LaserManager>().GenerateLasers();
    }

    public void AddBloc(string blocstr, int x, int y,int orientation = 0){
        //instanciate bloc
        GameObject bloc = GetComponent<BlocManager>().FindBloc(blocstr);
        if(bloc == null){
            return;
        }

        GameObject newBloc = Instantiate(bloc, new Vector3((x-gridWidth/2), (gridWidth/2)-y, 0), Quaternion.identity);
        newBloc.transform.parent = Parent.transform;
        newBloc.GetComponent<Bloc>().orientation = orientation;
        newBloc.GetComponent<Bloc>().UpdateSprite();
        //add to grid
        grid[x, y] = newBloc;
        ids[x, y] = GetComponent<BlocManager>().FindBlocId(blocstr);
    }

    public void AddBlocToFreeSpace(string blocstr){
        for (int i = 0; i < 100; i++){
            int x = Random.Range(0, gridWidth);
            int y = Random.Range(0, gridWidth);
            if(grid[x,y] == null && GetComponent<BorderManager>().IsBlocInBorder(new Vector2(x,y))){
                AddBloc(blocstr, x, y, (int)Random.Range(0, 4));
                return;
            }
        }
    }

    public void AddBlocToNearest(string blocstr){
        for(int i = 0; i < gridWidth; i++){
            for(int j = 0; j < gridWidth; j++){
                if(grid[j,i] == null){
                    AddBloc(blocstr, j, i, 0);
                    return;
                }
            }
        }
    }

    public List<GeneratorData> GetGeneratorList (){
        if (grid == null){
            return null;
        }

        List<GeneratorData> generatorList = new List<GeneratorData>();
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                if (grid[i, j] != null){
                    Bloc bloc = grid[i, j].GetComponent<Bloc>();
                    if (bloc is Generator){
                        Generator gen = grid[i, j].GetComponent<Generator>();
                        generatorList.Add(new GeneratorData(new Vector2(i, j), gen.orientation, gen.color));
                    }
                }
            }
        }
        return generatorList;
    }

    public bool IsEmpty(int x, int y){
        return grid[x, y] == null;
    }

    public Bloc GetBloc(int x, int y){
        if (grid[x, y] == null){
            return null;
        }
        return grid[x, y].GetComponent<Bloc>();
    }

    public GameObject GetBlocObject(int x, int y){
        return grid[x, y];
    }

    public void RotateBloc(int x, int y){
        if (grid[x, y] == null){
            return;
        }
        Bloc bloc = grid[x, y].GetComponent<Bloc>();
        bloc.RotateClockwise();
        GetComponent<LaserManager>().GenerateLasers();
    }

    public void RemoveBlocId(int x, int y){
        if (grid[x, y] == null){
            return;
        }
        grid[x, y] = null;
        suspended = ids[x, y];
        ids[x, y] = -1;
        GetComponent<LaserManager>().GenerateLasers();
    }

    public void RemoveBloc(int x, int y){
        if (grid[x, y] == null){
            return;
        }
        Destroy(grid[x, y]);
        grid[x, y] = null;
        ids[x, y] = -1;
        GetComponent<LaserManager>().GenerateLasers();
    }

    public void RemoveBloc(GameObject obj){
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                if (grid[i, j] == obj){
                    RemoveBloc(i, j);
                    return;
                }
            }
        }
    }

    public void RemoveblocWithExplosion(GameObject obj, Vector3 color){
        for (int i = 0; i < gridWidth; i++){
            for (int j = 0; j < gridWidth; j++){
                if (grid[i, j] == obj){
                    RemoveBloc(i, j);
                    AudioManager.instance.Play(0);
                    //on instancie l'explosion au centre du bloc
                    Vector3 pos = new Vector3(i - gridWidth / 2 + .5f, gridWidth / 2 - j-.5f, 0);
                    GameObject newObj = Instantiate(baloonParticle, pos, Quaternion.identity);
                    //on change la couleur du particle system
                    newObj.GetComponent<ParticleSystem>().startColor = new Color(color.x, color.y, color.z,1);
                    //puis on run l'animation
                    newObj.GetComponent<ParticleSystem>().Play();
                    return;
                }
            }
        }

    }

    public void SetBlocId(int x, int y, GameObject bloc){
        grid[x, y] = bloc;
        ids[x, y] = suspended;
        suspended = -1;
        GetComponent<LaserManager>().GenerateLasers();
    }

    public BlocData GetBlocDataAt(int x, int y){
        if (grid[x, y] == null){
            return null;
        }
        return GetComponent<BlocManager>().FindBlocDataWithId(ids[x, y]);
    }

    public bool IsWin(){
        for (int i = 0; i < gridWidth; i++){
            for (int j = 0; j < gridWidth; j++){
                if (grid[i, j] != null){
                    //on test si le bloc a le script WinObject
                    if (grid[i, j].GetComponent<WinObject>() != null){
                        if(!grid[i, j].GetComponent<WinObject>().isWin){
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
