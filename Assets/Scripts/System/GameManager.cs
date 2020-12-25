using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool isGameContinueing;

    // Start is called before the first frame update
    void Start()
    {
        //とりあえず即座にゲームが始まるようにする。
        GameStart();
    }

    private void GameStart(){

    }


    public void GemeOver(){

    }


    public bool GetIsGameContinueing(){
        return isGameContinueing;
    }
}
