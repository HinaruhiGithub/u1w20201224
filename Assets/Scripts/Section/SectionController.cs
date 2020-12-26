using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SectionController : MonoBehaviour
{
    [SerializeField] private int number = -1;
    [SerializeField] private List<int> neighborNum = new List<int>(){-1, -1, -1, -1};
    [SerializeField] private Object PlayerPrefab;


    //一回触れたら方向転換できるのは一回まで
    private bool canTurn;


    // Start is called before the first frame update
    void Start()
    {
        GetNumber();
        canTurn = true;
    }

    public int GetNumber(){
        return number;
    }

    public int GetNeighborNum(int direction){
        return neighborNum[direction];
    }

    public void SetCanTurn(bool _canTurn){
        this.canTurn = _canTurn;
    }

    public bool GetCanTurn(){
        return canTurn;
    }

    public void OnTriggerExit2D(Collider2D collider){
        //抜けたのがオブジェクトなら、canTurnを復活する
        if(collider.tag == "Player"){
            SetCanTurn(true);
        }
    }
}
