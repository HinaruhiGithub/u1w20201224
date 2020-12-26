using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;



public class TimeManager : MonoBehaviour
{
    public ReactiveProperty<float> passedTime;
    [SerializeField] private GameObject timeTextObject;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyManager enemyManager;


    // Start is called before the first frame update
    void Start()
    {
        passedTime = new ReactiveProperty<float>(0.0f);

        passedTime.Subscribe(_ =>{
            UpdateTimeText(passedTime.Value);
            playerController.Move();
            enemyManager.MoveAllChildren();
        }).AddTo(this.gameObject);
    }

    
    public void StartTimer(){
        passedTime.Value = 0.0f;
    }

    public void UpdateTime(float delta){
        passedTime.Value += delta;
    }

    private void UpdateTimeText(float value){
        timeTextObject.GetComponent<Text>().text = value.ToString();
    }
}
