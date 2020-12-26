using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhallController : MonoBehaviour
{
    private bool isOpened;
    private bool canDive;
    [SerializeField] private Object PlayerPrefab;



    void Start()
    {
        isOpened = false;
        canDive = false;
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player"){
            if(isOpened && !canDive){
                canDive = true;
            }
        }
    }

    public void SetIsOpened()
    {
        isOpened = true;
    }

    public void SetIsClosed()
    {
        isOpened = false;
        canDive = false;
    }

    public bool GetCanDive(){
        return canDive;
    }

    public bool GetIsOpened(){
        return isOpened;
    }
}
