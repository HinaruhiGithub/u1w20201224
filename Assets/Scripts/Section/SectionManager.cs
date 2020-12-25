using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    private int sectionNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetSectionNum();
    }

    public void GetSectionNum(){
        sectionNum = this.transform.childCount;
    }
}
