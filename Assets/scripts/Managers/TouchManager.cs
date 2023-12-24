using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    int gridWidth;

    Vector2Int beginTouchPosition;
    Vector2 beginTouchPositionFloat;
    int type; //0 = hold, 1 = move
    public bool isMoving;
    GameObject choosenBloc;
    bool dontMove;

    bool deleted = false;

    void Awake(){
        Input.multiTouchEnabled = false;
    }

    void Start(){
        gridWidth = GetComponent<GridManager>().gridWidth;
    }

    // programme de detection de toucches
    void Update(){
        if(GuiManager.instance.open || GetComponent<WinManager>().finished){
            return;
        }
        SandboxManager sandboxManager = GetComponent<SandboxManager>();
        if(sandboxManager.sandboxMode && sandboxManager.toolId == 1){
            if(Input.touchCount > 0){
                //données annexes
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                float x = Mathf.Ceil(touchPos.x)+(gridWidth/2)-1;
                float y = (gridWidth/2)-Mathf.Ceil(touchPos.y);
                Vector2 intTouchPos = new Vector2(Mathf.Clamp(x,0,gridWidth-1),Mathf.Clamp(y,0,gridWidth-1));

                if(x < 0 || x >= gridWidth || y < 0 || y >= gridWidth){
                    return;
                }

                if(touch.phase == TouchPhase.Ended){
                    isMoving = false;
                    GetComponent<GridManager>().RemoveBloc((int)intTouchPos.x,(int)intTouchPos.y);
                }
            }
            return;
        }else if(sandboxManager.sandboxMode && sandboxManager.toolId == 2){
            if(Input.touchCount > 0){
                //données annexes
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                float x = Mathf.Ceil(touchPos.x)+(gridWidth/2)-1;
                float y = (gridWidth/2)-Mathf.Ceil(touchPos.y);
                Vector2 intTouchPos = new Vector2(Mathf.Clamp(x,0,gridWidth-1),Mathf.Clamp(y,0,gridWidth-1));

                if(x < 0 || x >= gridWidth || y < 0 || y >= gridWidth){
                    return;
                }

                if(touch.phase == TouchPhase.Ended){
                    GetComponent<BorderManager>().ToggleBorder((int)intTouchPos.x,(int)intTouchPos.y);
                }
            }
            return;
        }



        if(Input.touchCount > 0){
            //données annexes
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            float x = Mathf.Ceil(touchPos.x)+(gridWidth/2)-1;
            float y = (gridWidth/2)-Mathf.Ceil(touchPos.y);
            Vector2 intTouchPos = new Vector2(Mathf.Clamp(x,0,gridWidth-1),Mathf.Clamp(y,0,gridWidth-1));

            //test si click OOB
            if(touch.phase == TouchPhase.Began){
                isMoving = true;
                if(x < 0 || x >= gridWidth || y < 0 || y >= gridWidth){
                    deleted = true;
                    return;
                }
                //debut
                AudioManager.instance.Play(1);
                beginTouchPosition = new Vector2Int((int)intTouchPos.x,(int)intTouchPos.y);
                beginTouchPositionFloat = touchPos;
                type = 0;
                choosenBloc = GetComponent<GridManager>().GetBlocObject((int)intTouchPos.x,(int)intTouchPos.y);
                dontMove = true;
                if(GetComponent<BorderManager>().IsBlocInBorder(intTouchPos) || GetComponent<SandboxManager>().sandboxMode){
                    dontMove = false;
                }
            }else if(touch.phase == TouchPhase.Moved){
                if(x < 0 || x >= gridWidth || y < 0 || y >= gridWidth){
                    return;
                }
                //milieu
                if((Vector2.Distance(beginTouchPositionFloat,touchPos) > 0.3f || type == 1) && !dontMove){
                    //on detecte un mouvement
                    if(type == 0 && choosenBloc != null){
                        type = 1;
                        GetComponent<GridManager>().RemoveBlocId((int)beginTouchPosition.x,(int)beginTouchPosition.y);
                        choosenBloc.transform.position = new Vector3(touchPos.x-.5f, touchPos.y+.5f, 0);
                    }else if (type == 1){
                        choosenBloc.transform.position = new Vector3(touchPos.x-.5f, touchPos.y+.5f, 0);
                    }
                }
            }else if(touch.phase == TouchPhase.Ended ){
                //fin
                isMoving = false;
                if(deleted){
                    deleted = false;
                    return;
                }

                
                //cas du release
                int clampedX  = (int)Mathf.Clamp(intTouchPos.x,0,gridWidth-1);
                int clampedY  = (int)Mathf.Clamp(intTouchPos.y,0,gridWidth-1);
                if(type == 0){
                    if(GetComponent<GridManager>().GetBlocObject(clampedX, clampedY) != null){
                        GetComponent<GridManager>().RotateBloc(clampedX, clampedY);
                    }else if(GetComponent<SandboxManager>().sandboxMode){
                        GetComponent<GridManager>().AddBloc(GetComponent<BlocManager>().FindBlocDataWithId(GetComponent<SandboxManager>().currentId).name,clampedX, clampedY);
                        GetComponent<LaserManager>().GenerateLasers();
                    }
                }else if(!dontMove){
                    AudioManager.instance.Play(2);
                    if(GetComponent<GridManager>().GetBlocObject(clampedX,clampedY) == null && (GetComponent<BorderManager>().IsBlocInBorder(new Vector2(clampedX,clampedY)) || GetComponent<SandboxManager>().sandboxMode)){
                        GetComponent<GridManager>().SetBlocId(clampedX, clampedY,choosenBloc);
                        choosenBloc.transform.position = new Vector3(Mathf.Ceil(Mathf.Clamp(touchPos.x,-gridWidth/2+1,gridWidth/2))-1, Mathf.Ceil(Mathf.Clamp(touchPos.y,-gridWidth/2+1,gridWidth/2)), 0);
                    }else{
                        GetComponent<GridManager>().SetBlocId((int)beginTouchPosition.x, (int)beginTouchPosition.y,choosenBloc);
                        choosenBloc.transform.position = new Vector3(beginTouchPosition.x-(gridWidth/2), (gridWidth/2)-beginTouchPosition.y, 0);
                    }
                }
            }
        }
    }
}
