using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isRunning;


    // Start is called before the first frame update
    void Start()
    {
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// マンホールにぶつかった
    /// </summary>
    private void Ouch(){

    }

    /// <summary>
    /// 捕まえた!
    /// </summary>
    private void CatchPlayer(){

    }

    /// <summary>
    /// 分岐点でどっちに動く？
    /// </summary>
    private void JudgeDirection(){

    }


    public void OnTriggerEnter2D(Collider2D collider){

    }
}
