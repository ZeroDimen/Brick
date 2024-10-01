using System.IO;
using UnityEngine;
using static Player_Data;

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/saves/"; //세이브 파일 저장경로 
        
    public static void SavePlayerData(GameData SaveData, string SaveFileName) //세이브 파일을 저장 하는 함수 
    {
        if (!Directory.Exists(SavePath)) // 세이브 파일 저장 경로에 폴더 파일이 없으면 생성
        {
            Directory.CreateDirectory(SavePath);
        }

        string SaveJosn = JsonUtility.ToJson(SaveData); // Json 포맷으로 변환(직렬화)

        string SaveFilePath = SavePath + SaveFileName + ".json";
        File.WriteAllText(SaveFilePath, SaveJosn); // 파일 생성 및 저장 
        Debug.Log("Save : " + SaveFilePath);
    }

    public static GameData LoadPlayerData(string SaveFileName) //세이브 파일을 불러오는 함수 
    {
        string SaveFilePath = SavePath + SaveFileName + ".json";
        if (!File.Exists(SaveFilePath)) // 세이브 파일이 없을 경우 호출
        {
            Debug.Log("No Save File");
            return null;
        }

        string SaveFile = File.ReadAllText(SaveFilePath); // 파일을 불러오는 함수 
        GameData SaveData = JsonUtility.FromJson<GameData>(SaveFile); // 불러온 파일의 포맷을 변경하여 SaveData에 삽입 (역직렬화)
        return SaveData;
    }
}