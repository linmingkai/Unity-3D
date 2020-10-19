using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{

    UserAction action;

    void Start(){
        action=Direct.getInstance().currentScene as UserAction;
    }

    void OnMouseDown(){
        if (Direct.gameStatus==-1 || Direct.gameStatus==1) return;

       if (gameObject.name=="priest" || gameObject.name=="devil"){//点了牧师或魔鬼
           action.pndOnClick(gameObject);
        
       }

       if (gameObject.name=="boot"){
           action.moveBoot();
       }
    }




}
