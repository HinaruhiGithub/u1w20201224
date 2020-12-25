using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    }

    public enum CharaMode
    {
        running,
        swimming,
        stunned
    }

    abstract public class CharacterBase : MonoBehaviour
    {
        private enum Direction{
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3
        }

        private CharaMode playerMode;
        private Road road;
        private float speed;
        private Direction direction;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Road GetRoad(){
            return road;
        }

        public void Run(){

        }
    }

}
