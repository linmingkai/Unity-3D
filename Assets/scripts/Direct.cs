using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direct : System.Object
{
    public static int gameStatus;
    public static Direct instance;
    public ISceneController currentScene{get;set;}
    public static Direct getInstance(){
        if (instance== null){
            instance=new Direct();
        }
        return instance;
    }

}

public interface ISceneController{
    void loadResources();
}

public interface UserAction{
    void pndOnClick(GameObject gameObject);
    void moveBoot();
    void restart();
}