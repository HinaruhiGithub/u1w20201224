using System;
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
    [SerializeField] private GameObject sectionMasterObject;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject gameMasterObject;
    [SerializeField] private GameObject timeManagerObject;





    // Start is called before the first frame update
    void Start()
    {
        previousTurnSection = new List<int>();
        enemyObjects = new List<GameObject>();

        GetEnemyReference();
        InitSectionData(sectionMasterObject.transform.childCount);
    }

    public void GameStart(){
        foreach(GameObject child in enemyObjects){
            child.GetComponent<EnemyController>().Init(playerObject, gameMasterObject, this.gameObject, this.sectionMasterObject);
            child.GetComponent<EnemyController>().StartGame();
        }
    }

    public void MoveAllChildren()
    {
        if(enemyObjects == null) return;
        foreach (GameObject child in enemyObjects)
        {
            child.GetComponent<EnemyController>().Move();
        }
    }

    /// <summary>
    /// 全敵の参照先を得る。
    /// </summary>
    private void GetEnemyReference(){
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
    /// <returns>結局どの方向に曲がるのか</returns>
    public int UpdatePreviousSection(int sectionNum, int direction){
        int d = -1;
        //反対方向
        int reverseDirection = (direction / 2) * 2 + (1 - direction % 2);

        List<int> canMove = new List<int>();

        Debug.Log(sectionNum);

        var sectionController = sectionMasterObject.GetComponent<SectionManager>().GetSection(sectionNum).GetComponent<SectionController>();
        for (int i = 0; i < 4; i++){
            if(sectionController.GetNeighborNum(i) >= 0) canMove.Add(i);
        }

        String Dd = "";
        for (int i = 0; i < canMove.Count; i++){
            Dd += canMove[i] + ",";
        }
        Debug.Log(Dd);

        switch (canMove.Count)
            {
                case 0:
                    Debug.LogError("四面楚歌と判断されました。");
                    break;

                case 1:
                    d = canMove[0];
                    break;
                case 2:
                    canMove.Remove(reverseDirection);
                    d = canMove[0];
                    foreach (var a in canMove)
                    {
                        Debug.Log(a.ToString() + ", Size:" + canMove.Count);
                    }
                    Debug.Log("Answer:" + d.ToString());
                    break;
                case 3:
                case 4:
                    canMove.Remove(reverseDirection);
                    if (canMove.Contains(previousTurnSection[sectionNum]))
                    {
                        Debug.Log("Delete");
                        canMove.Remove(previousTurnSection[sectionNum]);
                    }
                    d = canMove[(int)Math.Floor(UnityEngine.Random.value * (float)canMove.Count)];

                    foreach (var a in canMove)
                    {
                        Debug.Log(a.ToString() + ", Size:" + canMove.Count);
                    }
                    Debug.Log("Answer:" + d.ToString());
                    break;
                default:
                    Debug.LogError("移動可能方向が" + canMove.Count.ToString() + "と、illigalな値が出ました");
                    break;
            }

        Debug.Assert(d >= 0, "敵の移動方向の判断が無理でした。セクションの設定がおかしい可能性があります。");

        return d;
    }
}
