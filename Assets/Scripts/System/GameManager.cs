using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System;


public class GameManager : MonoBehaviour
{

    private bool isGameContinueing;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject enemyMasterObject;



    // Start is called before the first frame update
    void Start()
    {
        //とりあえず即座にゲームが始まるようにする。
        StartCoroutine(ReadyGo(1.0f, () => { Debug.Log("Delay call"); }));
    }

    void Update(){
        if(isGameContinueing){
            timeManager.UpdateTime(Time.deltaTime);
        }
    }

    private IEnumerator ReadyGo(float timeWait, Action action){
        yield return new WaitForSeconds(timeWait);
        GameStart();
    }

    private void GameStart(){
        isGameContinueing = true;
        playerObject.GetComponent<PlayerController>().StartGame();
        enemyMasterObject.GetComponent<EnemyManager>().GameStart();
    }


    public void GameOver(){
        isGameContinueing = false;
    }


    public bool GetIsGameContinueing(){
        return isGameContinueing;
    }
}
