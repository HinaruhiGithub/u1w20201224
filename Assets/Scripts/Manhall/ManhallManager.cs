using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ManhallManager : MonoBehaviour
{
    private ReactiveProperty<int> openedNum;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject gameMasterObject;


    // Start is called before the first frame update
    void Start()
    {
        openedNum = new ReactiveProperty<int>(0);
    }

    private void JudgeGameOver(){
        //空いてるマンホールが一つ以上ならゲームオーバ－にならない
        if(openedNum.Value > 0) return;
        
        //もしマンホールが全部しまって、プレーヤーが地下ならゲームオーバー
        if(playerObject.GetComponent<PlayerController>().GetPlayerMode() == Chasing.CharaMode.swimming){
            gameMasterObject.GetComponent<GameManager>().GameOver();
        }
    }

    public void IncOpenedNum()
    {
        openedNum.Value++;
    }

    public void DecOpenedNum()
    {
        openedNum.Value--;
        JudgeGameOver();
    }
}
