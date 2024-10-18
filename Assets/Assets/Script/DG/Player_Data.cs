using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : MonoBehaviour
{
    [Serializable]
    public class PlayerData // 사용자의 데이터
    {
        public PlayerData(string _Name, uint _Money, int _Stamina)
        {
            Name = _Name; // 사용자의 이름
            Money = _Money; // 사용자의 재화
            Stamina = _Stamina;
        }

        public string Name;
        public uint Money;
        public int Stamina;
    }

    [Serializable]
    public class BallData // 공의 데이터
    {
        public BallData(string _ballName, int _ballLevel, int _ballCost, float _ballDamage, float _ballCriticalChance,
            float _ballCriticalDamage)
        {
            BallName = _ballName;
            BallLevel = _ballLevel;
            BallCost = _ballCost;
            BallDamage = _ballDamage;
            BallCriticalChance = _ballCriticalChance;
            BallCriticalDamage = _ballCriticalDamage;
        }
        public string BallName; // 공의 이름
        public int BallLevel; // 공의 레벨
        public int BallCost; // 공의 소비 마나
        public float BallDamage; // 공의 공격력
        public float BallCriticalChance; // 공의 치명타 확률
        public float BallCriticalDamage; // 공의 치명타 데미지
    }

    [Serializable]
    public class BallDataList // 여러 공의 데이터를 관리하기 위한 리스트
    {
        public List<BallData> Balls = new List<BallData>();
    }

    [Serializable]
    public class TimeData
    {
        public TimeData( string _usertime, int _seconds)
        {
            User_Time = _usertime;
            Seconds = _seconds;
        }
        public string User_Time;
        public int Seconds;
        
    }

    [Serializable]
    public class GameData // 게임에 사용되는 데이터
    {
        public PlayerData playerData;
        public BallDataList ballDataList;
        public TimeData timeData;
    }
}