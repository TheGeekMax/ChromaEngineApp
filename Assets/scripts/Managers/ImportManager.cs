using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImportManager : MonoBehaviour
{  
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;
    public TextMeshProUGUI stars;
    public TextMeshProUGUI starCount;

    public string levelCode;
    

    //[HideInInspector]
    public bool entered = false;

    void Awake(){
        if(GameObject.FindWithTag("Keep") != null){
            GameObject keep = GameObject.FindWithTag("Keep");
            levelCode = keep.GetComponent<Keep>().LevelCode;
            name.text = keep.GetComponent<Keep>().Name;
            description.text = keep.GetComponent<Keep>().Description;
            stars.text = "you got " + keep.GetComponent<Keep>().Stars + " coin"+(keep.GetComponent<Keep>().Stars > 1 ? "s" : "");
            starCount.text = keep.GetComponent<Keep>().starCount.ToString();
            GetComponent<SandboxManager>().sandboxMode = keep.GetComponent<Keep>().SandBoxMode;
        }
        GetComponent<SandboxManager>().UpdateSandboxMod();
        if(levelCode != ""){
            entered = true;
            //test if object with tag "Keep" exists
            
        }
    }

    void Update(){
        if(!SandboxManager.instance.sandboxMode){
            return;
        }
        //pas de code mis au debut, chaque tick, on creer le code du niveau et on le met dans levelCode
        Encode();
    }

    public void Decode(){
        string code = levelCode;
        //on importe le code du niveau
        /*
            string sous forme
            saaaaaaa...bcbcbc...-ddddd...

            s = s/m/l la taille du plateau
            a = les bordures
            b = l'id des blocs
            c = leurs positions (3 premiers bits = x, 3 suivants = y)
            d = les blocs deplaceables
        */
        // int state = 0;
        GridManager gridManager = GetComponent<GridManager>();
        BlocManager blocManager = GetComponent<BlocManager>();
        BorderManager borderManager = GetComponent<BorderManager>();

        int borderCaracSize = 0;
        switch(code[0]){
            case 's':
                gridManager.sizeData = SizeData.Small_4x4;
                borderCaracSize = 4;
                break;
            case 'm':
                gridManager.sizeData = SizeData.Medium_6x6;
                borderCaracSize = 9;
                break;
            case 'l':
                gridManager.sizeData = SizeData.Large_8x8;
                borderCaracSize = 16;
                break;
        }
        gridManager.Init();

        //on cree les bordures
        Alphabet alphabet = new Alphabet(Alphabet.default16,4);
        Alphabet alphabet2 = new Alphabet(Alphabet.default64,6);
        int borderId = 0;
        int curx = 0;
        int cury = 0;
        for(int i = 0; i < borderCaracSize; i++){
            int[] id = alphabet.tobinary(code[i+1]);
            //Debug.Log("id = " + id[0] + id[1] + id[2] + id[3]);
            for(int j = 0; j < 4; j++){
                if((id[j]) != 0){
                    borderManager.AddBlocToBorder(curx,cury);
                }
                curx++;
                if(curx >= gridManager.gridWidth){
                    curx = 0;
                    cury++;
                }
                borderId++;
            }
        }  
        //on cree les blocs
        int state = 0;
        for(int i = borderCaracSize+1; i < code.Length; i++){
            switch(state){
                case 0:
                    if(code[i] == '-'){
                        state = 1;
                    }else{
                        int[] blocId = alphabet2.tobinary(code[i]);
                        string temp = "";
                        for(int j = 0; j < blocId.Length; j++){
                            temp = temp + blocId[j];
                        }
                        BlocData current = blocManager.FindBlocDataWithId(alphabet2.binaryto10(blocId));
                        if(current == null){
                            //Debug.Log("Bloc non trouve");
                        }
                        Vector2Int blocPos = alphabet2.binaryToCoors(alphabet2.tobinary(code[i+1]));
                        //Debug.Log("Bloc "+current.name+" en "+blocPos);
                        gridManager.AddBloc(current.name, blocPos.x, blocPos.y,(int)Random.Range(0, 4));
                        i++;
                    }
                    break;
                case 1:
                    int[] bd = alphabet2.tobinary(code[i]);
                    BlocData cr = blocManager.FindBlocDataWithId(alphabet2.binaryto10(bd));
                    if(cr == null){
                        //Debug.Log("Bloc non trouve");
                    }
                        gridManager.AddBlocToFreeSpace(cr.name);
                    break;
            }
        }
        
    }

    public void Encode(){
        //on exporte le code du niveau
        string code = "";
        GridManager gridManager = GetComponent<GridManager>();
        BlocManager blocManager = GetComponent<BlocManager>();
        BorderManager borderManager = GetComponent<BorderManager>();

        //etape 1 : on met la taille du plateau
        switch(gridManager.sizeData){
            case SizeData.Small_4x4:
                code += "s";
                break;
            case SizeData.Medium_6x6:
                code += "m";
                break;
            case SizeData.Large_8x8:
                code += "l";
                break;
        }

        //etape 2 : on met les bordures
        Alphabet alphabet = new Alphabet(Alphabet.default16,4);
        Alphabet alphabet2 = new Alphabet(Alphabet.default64,6);
        int borderId = 0;
        int curId = 0;
        for(int i = 0; i < gridManager.gridWidth; i++){
            for(int j = 0; j < gridManager.gridWidth; j++){
                if(curId == 4){
                    code += alphabet.frombinary(alphabet.tenToBinary(borderId));
                    borderId = 0;
                    curId = 0;
                }
                if(borderManager.IsBlocInBorder(new Vector2(j,i))){
                    borderId += (int)Mathf.Pow(2,curId);
                }
                curId++;
            }
        }
        code += alphabet.frombinary(alphabet.tenToBinary(borderId));

        //etape 3 : on met les blocs fixes(ceux pour qui borderManager.IsBlocInBorder est faux)
        for(int i = 0; i < gridManager.gridWidth; i++){
            for(int j = 0; j < gridManager.gridWidth; j++){
                if(!borderManager.IsBlocInBorder(new Vector2(i,j))){
                    BlocData current = gridManager.GetBlocDataAt(i,j);
                    if(current == null){
                        //Debug.Log("Bloc non trouve");
                    }else{
                        code += alphabet2.frombinary(alphabet2.tenToBinary(blocManager.FindBlocIdWithGameObject(current.prefab)));
                        code += alphabet2.frombinary(alphabet2.coorsToBinary(new Vector2Int(i,j)));
                    }
                }
            }
        }

        //etape 4 : on met les blocs deplaceables
        code += "-";
        for(int i = 0; i < gridManager.gridWidth; i++){
            for(int j = 0; j < gridManager.gridWidth; j++){
                if(borderManager.IsBlocInBorder(new Vector2(i,j))){
                    BlocData current = gridManager.GetBlocDataAt(i,j);
                    if(current == null){
                        //Debug.Log("Bloc non trouve");
                    }else{
                        code += alphabet2.frombinary(alphabet2.tenToBinary(blocManager.FindBlocIdWithGameObject(current.prefab)));
                    }
                }
            }
        }

        levelCode = code;
    }
}
