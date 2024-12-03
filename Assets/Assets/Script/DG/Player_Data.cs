using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : MonoBehaviour
{
    [Serializable]
    public class PlayerData // 사용자의 데이터
    {
        public PlayerData(string _Name, uint _Money)
        {
            Name = _Name; // 사용자의 이름
            Money = _Money; // 사용자의 재화
        }

        public string Name;
        public uint Money;
    }

    [Serializable]
    public class CardData // 공의 데이터
    {
        public ushort CardNumber; // 카드의 고유 번호
        public string CardName; // 카드의 이름
        public int CardLevel; // 카드의 레벨
        public float CardDamage; // 카드의 공격력
        public bool CardIsHave;

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