using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;


namespace Chasing{

    public class Road
    {
        public int fromNum = -1;
        public int toNum = -1;

        public Road(int _fromNum, int _toNum)
        {
            this.fromNum = _fromNum;
            this.toNum = _toNum;
        }

        public void UpdateRoad(int _toNum)
        {
            fromNum = toNum;
            toNum = _toNum;
        }

        public bool CanLook(int sectionNum){
            if((sectionNum == this.fromNum) || (sectionNum == this.toNum)){
                return true;
            } else {
                return false;
            }
        }

    }

    public enum CharaMode
    {
        running,
        swimming,
        stunned,
        ready
    }

    abstract public class CharacterBase : MonoBehaviour
    {
        protected enum Direction{
            Stop = -1,
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3
        }

        protected CharaMode playerMode;
        [SerializeField] private int initFromNum;
        [SerializeField] private int initToNum;
        protected Road road;
        protected float speed;
        [SerializeField] private float initSpeed = 2.0f;
        [SerializeField] protected Direction direction;
        [SerializeField] protected float swimSpeedMagnification = 2.0f;
        protected GameObject gameMasterObject;


        // Start is called before the first frame update
        void Start()
        {
            playerMode = CharaMode.ready;
            speed = initSpeed;
            road = new Road(initFromNum, initToNum);

        }


        public void StartGame(){
            playerMode = CharaMode.running;
        }

        public Road GetRoad(){
            return road;
        }

        public void Run(float magnification = 1.0f){
            float delta = speed * magnification * Time.deltaTime;

            switch(direction){
                case Direction.Stop:
                    break;
                case Direction.Up:
                    this.transform.Translate(0, delta, 0);
                    break;
                case Direction.Down:
                    this.transform.Translate(0, -delta, 0);
                    break;
                case Direction.Left:
                    this.transform.Translate(-delta, 0, 0);
                    break;
                case Direction.Right:
                    this.transform.Translate(delta, 0, 0);
                    break;
                default:
                    Debug.LogError("directionの値が" + direction.ToString() + "を示したのでダメです。");
                    break;
            }
        }

        public CharaMode GetPlayerMode(){
            return playerMode;
        }

        public int GetDirection(){
            return (int)this.direction;
        }

    }

}
