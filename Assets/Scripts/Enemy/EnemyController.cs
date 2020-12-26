using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Chasing.CharacterBase
{
    private bool isRunning;
    private GameObject playerObject;
    private GameObject enemyMaster;
    [SerializeField] private Object sectionPrefab;
    private GameObject sectionMasterObject;
    [SerializeField] private float stunMax = 2.0f;
    private float stunCount;


    public void Init(GameObject _playerObject, GameObject _gameMaster, GameObject _enemyMaster, GameObject _sectionMasterObject){
        isRunning = true;
        playerObject = _playerObject;
        gameMasterObject = _gameMaster;
        enemyMaster = _enemyMaster;
        sectionMasterObject = _sectionMasterObject;
        stunCount = stunMax;
    }

    public void Stunning(){
        Debug.Log(stunCount);
        stunCount-= Time.deltaTime;
        if(stunCount <= 0){
            stunCount =  stunMax;
            playerMode = Chasing.CharaMode.running;
        }
    }

    public void Move()
    {
        Debug.Log(road.fromNum.ToString() + "," + road.toNum.ToString());
        
        switch (playerMode)
        {
            case Chasing.CharaMode.running:
                Run();
                break;
            case Chasing.CharaMode.swimming:
                Run(swimSpeedMagnification);
                break;
            case Chasing.CharaMode.stunned:
                Stunning();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// マンホールにぶつかったら…
    /// </summary>
    private void Ouch(GameObject manhallObject){
        manhallObject.GetComponent<ManhallController>().SetIsClosed();

        //とりあえず止まる。
        this.playerMode = Chasing.CharaMode.stunned;
    }

    /// <summary>
    /// 捕まえた!
    /// </summary>
    private void CatchPlayer(){
        gameMasterObject.GetComponent<GameManager>().GameOver();
    }

    /// <summary>
    /// 分岐点でどっちに動く？
    /// </summary>
    private void JudgeDirection(Transform sectionTransform){
        Debug.Log("In");
        this.transform.position = sectionTransform.position;
        //主人公が見える位置にいるならそっちに行く
        if(playerObject.GetComponent<PlayerController>().GetRoad().CanLook(this.road.toNum) && (playerMode == Chasing.CharaMode.running)){

            int pd = playerObject.GetComponent<PlayerController>().GetDirection();

            //主人公の方向から敵の方向を割り出す。
            if(this.road.toNum == playerObject.GetComponent<PlayerController>().GetRoad().fromNum){
                //敵が向かったセクションが主人公が去った後のセクションなら同じ方向へ
                this.direction = (Chasing.CharacterBase.Direction)pd;
            } else {
                //そうじゃないなら反対方向へ
                this.direction = (Chasing.CharacterBase.Direction)((pd / 2) * 2 + (1 - pd % 2));
            }
            
            //計算したうえでダメならとりあえず…
            if(sectionTransform.gameObject.GetComponent<SectionController>().GetNeighborNum((int)this.direction) < 0){
                this.direction = (Chasing.CharacterBase.Direction)enemyMaster.GetComponent<EnemyManager>().UpdatePreviousSection(this.road.toNum, (int)this.direction);
                Debug.Log("Answer:" + ((int)this.direction).ToString() + "," + this.direction.ToString());
                this.road.UpdateRoad(sectionMasterObject.GetComponent<SectionManager>().GetSection(this.road.toNum).GetComponent<SectionController>().GetNeighborNum((int)this.direction));

            } else {
                this.road.UpdateRoad(playerObject.GetComponent<PlayerController>().GetRoad().toNum);
            }
        } else {
            this.direction = (Chasing.CharacterBase.Direction)enemyMaster.GetComponent<EnemyManager>().UpdatePreviousSection(this.road.toNum, (int)this.direction);
            Debug.Log("Answer:" + ((int)this.direction).ToString() + "," + this.direction.ToString());
            this.road.UpdateRoad(sectionMasterObject.GetComponent<SectionManager>().GetSection(this.road.toNum).GetComponent<SectionController>().GetNeighborNum((int)this.direction));
        }
    }


    public void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            if(this.playerMode == Chasing.CharaMode.running) CatchPlayer();
        } else if (collider.tag == "Section"){
            JudgeDirection(collider.transform);
        } else if (collider.tag == "Manhall"){
            if(collider.gameObject.GetComponent<ManhallController>().GetIsOpened()) Ouch(collider.gameObject);
        }
    }
}
