using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class TimeManager : MonoBehaviour
{
    private ReactiveProperty<float> passedTime;


    // Start is called before the first frame update
    void Start()
    {
        passedTime = new ReactiveProperty<float>(0.0f);
    }

    
    public void StartTimer(){
        passedTime.Value = 0.0f;
    }

    /// <summary>
    /// これ何でいるのだろう…
    /// </summary>
    public void SetTimer(){

    }

    private void UpdateTimer(){

    }
}
