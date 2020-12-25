using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ManhallController : MonoBehaviour
{
    private ReactiveProperty<int> openedNum;


    // Start is called before the first frame update
    void Start()
    {
        openedNum = new ReactiveProperty<int>(0);
    }

    //
    public void IncOpenedNum(){
        openedNum.Value++;
    }

    public void DecOpenedNum(){
        openedNum.Value--;
    }

}
