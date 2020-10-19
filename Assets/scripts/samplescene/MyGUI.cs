using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGUI : MonoBehaviour
{
    UserAction action;
    public int w,h;
    public Texture2D title,restart,over,win,esc;
    // Start is called before the first frame update
    void Start()
    {
        action=Direct.getInstance().currentScene as UserAction;
        w=Screen.width;
        h=Screen.height;

        restart=Resources.Load<Texture2D>("img/restart");
        over=Resources.Load<Texture2D>("img/over");
        win=Resources.Load<Texture2D>("img/win");
        
    }

    // Update is called once per frame
    void Update()
    {
        w=Screen.width;
        h=Screen.height;

        
    }

    void OnGUI(){
        

        
       
        if ( GUI.Button(new Rect(w*0.45f,h-w*0.1f,w*0.1f,w*0.05f),restart) ){
            action.restart();
        }
        
        if (Direct.gameStatus==-1){
            GUI.Label(new Rect(0.5f*w-100,0.5f*h-100,200,200),over);
        }
        if (Direct.gameStatus==1){
            GUI.Label(new Rect(0.5f*w-100,0.5f*h-100,200,200),win);
        }
    }
}
