using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    private int sectionNum = 0;
    private List<GameObject> sectionObject;


    // Start is called before the first frame update
    void Start()
    {
        GetSectionNum();

        sectionObject = new List<GameObject>();
        InitSection();
    }

    private void InitSection(){

        for (int i = 0; i < this.transform.childCount; i++){
            sectionObject.Add(null);
        }

        foreach (Transform child in this.transform)
        {
            int num = child.gameObject.GetComponent<SectionController>().GetNumber();
            if ((num < 0) || (sectionObject.Count <= num))
            {
                Debug.LogError("セクションのnumber"+ num.ToString() +"が良くないです");
            }
            else
            {
                Debug.Assert(sectionObject[num] == null, "セクションのnumber" + num.ToString() + "が重複してます。");
                sectionObject[num] = child.gameObject;
            }
        }

    }

    public void GetSectionNum(){
        sectionNum = this.transform.childCount;
    }

    public GameObject GetSection(int num){
        return sectionObject[num];
    }
}
