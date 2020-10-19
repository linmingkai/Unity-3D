using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftCoast{
    public GameObject obj;
    public Vector3 position;
    public GameObject[] ptr;

    public leftCoast(){
        ptr=new GameObject[7];
        for(int i=1;i<=6;++i)
            ptr[i]=null;
        position=new Vector3(-15,0,0);
        obj=Object.Instantiate(Resources.Load<GameObject>("profeb/coast"),position,Quaternion.identity);
        obj.name="leftCoast";
    }

}

public class rightCoast{
    public GameObject obj;
    public Vector3 position;
    public GameObject[] ptr;
    

    public rightCoast(){
        ptr=new GameObject[7];
        for(int i=1;i<=6;++i)
            ptr[i]=null;
        position=new Vector3(15,0,0);
        obj=Object.Instantiate(Resources.Load<GameObject>("profeb/coast"),position,Quaternion.identity);
        obj.name="rightCoast";
    }
}

public class water{
    public GameObject obj;
    public Vector3 position;

    public water(){
        position=new Vector3(0,-0.5f,0);
        obj=Object.Instantiate(Resources.Load<GameObject>("profeb/water"),position,Quaternion.identity);
        obj.name="water";
        obj.AddComponent(typeof(OnClick));
    }
}

public class  pnd{
    public GameObject obj;
    public int pos;

    public Vector3 target;

    public int moveStatus;

    public pnd(int kind,Vector3 position){
        pos=1;
        moveStatus=0;

        if (kind==1){
                obj=Object.Instantiate(Resources.Load<GameObject>("profeb/priest"),position,Quaternion.identity);
                obj.name="priest";
                obj.AddComponent(typeof(OnClick));

        }
        
        if (kind==2){
            obj=Object.Instantiate(Resources.Load<GameObject>("profeb/devil"),position,Quaternion.identity);
            obj.name="devil";
            obj.AddComponent(typeof(OnClick));

        }
    }
}


public class boot{
    public GameObject obj;
    public  Vector3 bootl,bootr;
    public GameObject l,r;
    public static Vector3 lv,rv;

    public int side;

    public int moveStatus;

    public boot(){
        bootl=new Vector3(-3.5f,2,0);
        bootr=new Vector3(3.5f,2,0);

        obj=Object.Instantiate<GameObject>(Resources.Load<GameObject>("profeb/boot"),bootl,Quaternion.identity);
        obj.name="boot";
        side=1;

        lv=new Vector3(-0.75f,0.9f,0);
        rv=new Vector3(0.75f,0.9f,0);
        l=null;r=null;

        moveStatus=0;

        obj.AddComponent(typeof(OnClick));       
      

       
    }

    
}

