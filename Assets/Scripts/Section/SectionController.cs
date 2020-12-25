using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour
{
    [SerializeField] private int number = -1;

    [SerializeField] private List<int> neighborNum = new List<int>(){-1, -1, -1, -1};


    // Start is called before the first frame update
    void Start()
    {
        GetNumber();
    }

    private int GetNumber(){
        return number;
    }

    private int GetNeighborNum(int direction){
        return neighborNum[direction];
    }
}
