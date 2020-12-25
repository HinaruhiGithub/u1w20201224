using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhallManager : MonoBehaviour
{
    private bool isOpened;
    private bool canDive;
    void Start()
    {
        isOpened = false;
        canDive = false;
    }

    public void OnTriggerExit2D(Collider2D collider){
        
    }

    private void SetIsOpened(){
        isOpened = true;
    }

    private void SetIsClosed(){
        isOpened = false;
        canDive = false;
    }
}
