using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// 前回の分岐点での動き方
    /// </summary>
    List<int> previousTurnSection;
    List<GameObject> enemyObjects;



    // Start is called before the first frame update
    void Start()
    {
        previousTurnSection = new List<int>();
        enemyObjects = new List<GameObject>();

        GetEnemyReference();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 全敵の参照先を得る。
    /// </summary>
    void GetEnemyReference(){
        foreach(Transform child in this.transform){
            enemyObjects.Add(child.gameObject);
        }
    }

    /// <summary>
    /// 分岐点のデータを初期化する
    /// </summary>
    /// <param name="sectionSum"></param>
    public void InitSectionData(int sectionSum){
        for (int i = 0; i < sectionSum; i++){
            previousTurnSection.Add(-1);
        }
    }

    /// <summary>
    /// 分岐点情報の更新
    /// </summary>
    /// <param name="sectionNum">分岐点番号</param>
    /// <returns>結局どっちに曲がるのか</returns>
    public int UpdateSection(int sectionNum){
        int ans = -1;

        return ans;
    }
}
