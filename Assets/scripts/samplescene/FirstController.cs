using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour,ISceneController,UserAction
{
    public  Vector3[] leftpos;
    public  Vector3[] rightpos;
    public  int[] leftseat;
    public  int[] rightseat;

    public  leftCoast lc;
    public  rightCoast rc;
    public water wat;
    public  pnd[] p;
    public  boot b;


    
    // Start is called before the first frame update
    void Awake(){
        Direct direct=Direct.getInstance();
        direct.currentScene=this;
    }

    void Start()
    {
        leftpos=new Vector3[7]{new Vector3(0,0,0),new Vector3(-6,3,0),new Vector3(-8,3,0),new Vector3(-10,3,0),
        new Vector3(-12,3,0),new Vector3(-14,3,0),new Vector3(-16,3,0)};

        rightpos=new Vector3[7]{new Vector3(0,0,0),new Vector3(6,3,0),new Vector3(8,3,0),new Vector3(10,3,0),
        new Vector3(12,3,0),new Vector3(14,3,0),new Vector3(16,3,0)};

        leftseat=new int[7];
        rightseat=new int[7];

        for(int i=1;i<=6;++i){
            leftseat[i]=0;
            rightseat[i]=0;
        }
        leftseat[1]=1;leftseat[2]=1;leftseat[3]=1;
        leftseat[4]=2;leftseat[5]=2;leftseat[6]=2;


        Direct.gameStatus=0;

        loadResources();
    }

    // Update is called once per frame

    void Update()
    {

        if (b.moveStatus!=0){
            for(int i=1;i<=6;++i){
                if (p[i].moveStatus==1||p[i].moveStatus==2){
                    p[i].obj.transform.position=Vector3.MoveTowards(p[i].obj.transform.position,p[i].target,0.06f);
                }

                if (p[i].obj.transform.position==p[i].target){
                    p[i].moveStatus=0;
                }
            }

            if (b.moveStatus==1){

                b.obj.transform.position=Vector3.MoveTowards(b.obj.transform.position,b.bootr,0.06f);
                if (b.obj.transform.position==b.bootr){
                    b.moveStatus=0;
                    b.side=2;
                    Direct.gameStatus=check();
                }

            }else if (b.moveStatus==2){
                
                b.obj.transform.position=Vector3.MoveTowards(b.obj.transform.position,b.bootl,0.06f);
                if (b.obj.transform.position==b.bootl){
                    b.moveStatus=0;
                    b.side=1;
                    Direct.gameStatus=check();
                }

            }

        }


    }

    public void loadResources(){
        lc=new leftCoast();
        rc=new rightCoast();
        wat=new water();
        
        p=new pnd[7];
        for(int i=1;i<=6;++i){
            p[i]=new pnd(leftseat[i],leftpos[i]);
            lc.ptr[i]=p[i].obj;
        }
        


        b=new boot();
        gameObject.AddComponent(typeof(MyGUI));
    }

    public void moveBoot(){
        if (b.moveStatus!=0) return;
        if (b.l==null && b.r==null) return;

        for(int i=1;i<=6;++i){
            if (p[i].obj==b.l || p[i].obj==b.r){
                p[i].moveStatus=b.side;
                if (b.side==1) p[i].target=p[i].obj.transform.position+new Vector3(7,0,0);
                else p[i].target=p[i].obj.transform.position+new Vector3(-7,0,0);
            }
        }
        b.moveStatus=b.side;
    }

    public void pndOnClick(GameObject gameObject){
            if (b.moveStatus!=0) return;

            if (b.l!=gameObject && b.r!=gameObject){// 不在船上
                int objside=0;
                int index=0;
                for(int i=1;i<=6;++i){
                    if (lc.ptr[i]==gameObject){
                        objside=1;
                        index=i;
                        break;
                    }

                    if (rc.ptr[i]==gameObject){
                        objside=2;
                        index=i;
                        break;
                    }
                }

                if (objside!=b.side){ //人与船不在同一海岸，则不上
                    return;
                }

                if (b.l==null){//上船，船满则不上
                    b.l=gameObject;

                    if (objside==1)
                        lc.ptr[index]=null;
                    else
                        rc.ptr[index]=null;

                    gameObject.transform.position=b.obj.transform.position+boot.lv;
                    
                }else{
                    if (b.r==null){
                        b.r=gameObject;

                        if (objside==1)
                            lc.ptr[index]=null;
                         else
                            rc.ptr[index]=null;

                        gameObject.transform.position=b.obj.transform.position+boot.rv;
                    }
                }

            }else{  //在船上
                if (b.side==1){ // 在左海岸
                    for(int i=1;i<=6;++i){
                        if (lc.ptr[i]==null){
                            gameObject.transform.position=leftpos[i];
                            lc.ptr[i]=gameObject;

                            if (b.l==gameObject)
                                b.l=null;
                            else 
                                b.r=null;

                            break;
                        }
                    }
                }else{// 在右海岸
                    for(int i=1;i<=6;++i){
                        if (rc.ptr[i]==null){
                            gameObject.transform.position=rightpos[i];
                            rc.ptr[i]=gameObject;

                            if (b.l==gameObject)
                                b.l=null;
                            else 
                                b.r=null;

                            break;
                        }
                    }
                }
            }

        Direct.gameStatus=check();
    }


    int check(){
        int p,d;
        p=0;d=0;
        for(int i=1;i<=6;++i){
            if (lc.ptr[i]==null) continue;
            if (lc.ptr[i].name=="priest")
                p++;
            if (lc.ptr[i].name=="devil")
                d++;
        }
        if (b.side==1){
            if (b.l!=null){
                if (b.l.name=="priest") p++;
                if (b.l.name=="devil") d++;
            }
            if (b.r!=null){
                if (b.r.name=="priest") p++;
                if (b.r.name=="devil") d++;
            }
        }
        if (d>p && p!=0){
            return -1;
        }

        p=0;d=0;
        for(int i=1;i<=6;++i){
            if (rc.ptr[i]==null) continue;
            if (rc.ptr[i].name=="priest")
                p++;
            if (rc.ptr[i].name=="devil")
                d++;
        }
        if (b.side==2){
            if (b.l!=null){
                if (b.l.name=="priest") p++;
                if (b.l.name=="devil") d++;
            }
            if (b.r!=null){
                if (b.r.name=="priest") p++;
                if (b.r.name=="devil") d++;
            }
        }
        if (d>p && p!=0){
            return -1;
        }

        int number=0;
        for(int i=1;i<=6;++i){
            if (rc.ptr[i]!=null)
                number++;
        }
        if (number==6)
            return 1;
        return 0;

    }

    public void restart(){
        for(int i=1;i<=6;++i){
            p[i].obj.transform.position=leftpos[i];
            lc.ptr[i]=p[i].obj;
            rc.ptr[i]=null;
            p[i].moveStatus=0;
        }
        b.l=null;
        b.r=null;
        b.obj.transform.position=b.bootl;
        b.side=1;
        b.moveStatus=0;
        Direct.gameStatus=0;

    }
}
