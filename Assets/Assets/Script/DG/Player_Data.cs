using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : MonoBehaviour
{
    [Serializable]
    public class PlayerData // 사용자의 데이터
    {
        public PlayerData(string _Name, int _Money, int _Map, int _MaxMap)
        {
            Name = _Name; // 사용자의 이름
            Money = _Money; // 사용자의 재화
            Map = _Map;
            MaxMap = _MaxMap;
            
        }

        public string Name;
        public int Money;
        public int Map;
        public int MaxMap;
    }

    [Serializable]
    public class CardData // 공의 데이터
    {
        public string CardName; // 카드의 이름
        public int CardCost; // 카드의 비용
        public int CardLevel; // 카드의 레벨
        public float CardDamage; // 카드의 공격력
        public int CardCount ; // 카드의 갯수
        // public bool CardIsHave; // 카드의 소지 유무

    }

    [Serializable]
    public class CardDataList // 여러 공의 데이터를 관리하기 위한 리스트
    {
        public List<CardData> Cards = new List<CardData>();
    }
    

    [Serializable]
    public class GameData // 게임에 사용되는 데이터
    {
        public PlayerData playerData;
        public CardDataList cardDataList;
    }
}