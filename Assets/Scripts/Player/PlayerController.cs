using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Chasing.CharacterBase
{

    [SerializeField] private Object sectionPrefab;
    [SerializeField] private Object manhallPrefab;
    [SerializeField] private float keyStrength = 0.6f;

    private float horizontal;
    private float vertical;
    private bool zKey;
    private float zKeyPusshed = 0.1f;



    void Update(){
        if(zKeyPusshed > 0) zKeyPusshed-=Time.deltaTime;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        zKey = Input.GetButtonDown("OpenManhall");

        if(zKey) zKeyPusshed = 0.1f;

    }

    public void Move(){

        switch(playerMode){
            case Chasing.CharaMode.running:
                Run();
                break;
            case Chasing.CharaMode.swimming:
                Run(swimSpeedMagnification);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 潜る
    /// </summary>
    private void Dive(GameObject manhallObject){
        playerMode = Chasing.CharaMode.swimming;
        manhallObject.GetComponent<ManhallController>().SetIsClosed();
    }


    /// <summary>
    /// 泳ぎから戻ってくる
    /// </summary>
    private void ReturnToField(GameObject manhallObject){
        playerMode = Chasing.CharaMode.running;
        manhallObject.GetComponent<ManhallController>().SetIsClosed();
    }

    /// <summary>
    /// 分岐点で曲がる
    /// </summary>
    /// <param name="sectionObject">分岐の場所</param>
    /// <param name="direction">方向</param>
    private void TurnDirection(GameObject sectionObject, Direction _direction){
        this.direction = _direction;
        this.transform.position = sectionObject.transform.position;
        this.road.UpdateRoad(sectionObject.GetComponent<SectionController>().GetNeighborNum((int)_direction));
        //セクションを二回曲がれないようにする。
        sectionObject.GetComponent<SectionController>().SetCanTurn(false);
    }

    /// <summary>
    /// 方向キー入力について
    /// </summary>
    private void InputDirectionKey(GameObject sectionObject){
        //分岐済みなら分岐しない
        if (!sectionObject.GetComponent<SectionController>().GetCanTurn()) return;

        //↑
        if (keyStrength < vertical)
        {
            if(sectionObject.GetComponent<SectionController>().GetNeighborNum(0) >= 0){
                TurnDirection(sectionObject, Direction.Up);
            }
        }
        //↓
        if (vertical < -keyStrength){
            if (sectionObject.GetComponent<SectionController>().GetNeighborNum(1) >= 0)
            {
                TurnDirection(sectionObject, Direction.Down);
            }
        }
        //←
        if (horizontal < -keyStrength)
        {
            if (sectionObject.GetComponent<SectionController>().GetNeighborNum(2) >= 0)
            {
                TurnDirection(sectionObject, Direction.Left);
            }
        }
        //→
        if (keyStrength < horizontal)
        {
            if (sectionObject.GetComponent<SectionController>().GetNeighborNum(3) >= 0)
            {
                TurnDirection(sectionObject, Direction.Right);
            }
        }

    }

    public void Randomturn(GameObject sectionObject){
        //そもそも壁じゃないなら出ない
        if (sectionObject.GetComponent<SectionController>().GetNeighborNum((int)this.direction) >= 0) return;

        int d = -1;
        //反対方向
        int reverseDirection = ((int)direction / 2) * 2 + (1 - (int)direction % 2);

        List<int> canMove = new List<int>();

        var sectionController = sectionObject.GetComponent<SectionController>();
        for (int i = 0; i < 4; i++)
        {
            if (sectionController.GetNeighborNum(i) >= 0) canMove.Add(i);
        }

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
                break;
            case 3:
            case 4:
                canMove.Remove(reverseDirection);
                d = canMove[(int)Mathf.Floor(UnityEngine.Random.value * (float)canMove.Count)];
                break;
            default:
                Debug.LogError("移動可能方向が" + canMove.Count.ToString() + "と、illigalな値が出ました");
                break;
        }
        if (canMove.Count == 1)
        {
            d = canMove[0];
        }

            Debug.Assert(d >= 0, "敵の移動方向の判断が無理でした。セクションの設定がおかしい可能性があります。");


        TurnDirection(sectionObject, (Direction)d);

    }

    /// <summary>
    /// マンホールを開けるか開けないか
    /// </summary>
    /// <param name="manhallObject"></param>
    private void InputManhallOpenKey(GameObject manhallObject){
        if(manhallObject.GetComponent<ManhallController>().GetIsOpened()) return;

        if(zKeyPusshed > 0){
            OpenManhall(manhallObject);
        }
    }

    /// <summary>
    /// マンホール開ける
    /// </summary>
    /// <param name="manhallObject"></param>
    private void OpenManhall(GameObject manhallObject){
        Debug.Log("どうする？");

        manhallObject.GetComponent<ManhallController>().SetIsOpened();
    }

    /// <summary>
    /// マンホールに入れるか出れるかどうかの確認
    /// </summary>
    /// <param name="manhallObject"></param>
    private void CheckManhall(GameObject manhallObject){

        if(this.playerMode == Chasing.CharaMode.running){
            if (manhallObject.GetComponent<ManhallController>().GetCanDive())
            {
                Dive(manhallObject);
            }
        }

        if (this.playerMode == Chasing.CharaMode.swimming)
        {
            if (manhallObject.GetComponent<ManhallController>().GetIsOpened())
            {
                ReturnToField(manhallObject);
            }
        }

    }

    /// <summary>
    /// 壁とぶつかったか？ぶつかったなら止まる
    /// </summary>
    /// <param name="sectionObject">いたセクションオブジェクト</param>
    private void CollisionToWall(GameObject sectionObject){
        //そもそも壁じゃないなら出ない
        if(sectionObject.GetComponent<SectionController>().GetNeighborNum((int)this.direction) >= 0) return;

        this.direction = Direction.Stop;
        this.transform.position = sectionObject.transform.position;
    }

    public void OnTriggerStay2D(Collider2D collider){
        if(collider.tag == "Section"){
            //セクションと重なってるなら、方向キー入力可能にする。
            InputDirectionKey(collider.gameObject);
        } else if (collider.tag == "Manhall"){
            //マンホールと重なってるなら、マンホールを開けるか開けないか？
            InputManhallOpenKey(collider.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Manhall"){
            //マンホール入る？
            CheckManhall(collider.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        //壁なのにセクションから出ようとしたなら
        if (collider.tag == "Section")
        {
            //CollisionToWall(collider.gameObject);
            Randomturn(collider.gameObject);
        }
    }

}
