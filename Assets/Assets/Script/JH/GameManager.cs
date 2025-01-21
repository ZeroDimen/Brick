using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Player_Data;
public enum State
{
    Play,
    Shoot,
    Standby,
    Menu,
    End
}
public class GameManager : MonoBehaviour
{
    private GameData gameData;
    public State _state;
    public static GameManager manager;
    public GameObject player;
    public int UsedDeck;
    float play_time;
    private int map;
    public string[] cardName = new string[8];
    public GameObject[] stage;
    public GameObject clearPanel;
    public GameObject failPanel;
    public GameObject[] grid;
    public int _grid;
    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(gameObject);
        
        gameData = SaveSystem.LoadPlayerData("save_1101"); // 파일 읽기 
        map = gameData.playerData.Map; // 저장된 맵 번호
        for (int i = 0; i < 8; i++)
            cardName[i] = gameData.cardDataList.Cards[i].CardName; // 이름
        stage[map].SetActive(true);
        Brick.Bricks.Clear();
    }
    private void Update()
    {
        if (_state == State.Shoot)
        {
            play_time += Time.deltaTime;
            if (play_time > 5)
                Time.timeScale = 2;
        }
        else if(_state != State.End)
        {
            Time.timeScale = 1;
            play_time = 0;
        }
    }

    public void Open_Grid(int height)
    {
        _grid = height;
        MiniCam._grid = height;
            
        switch (height)
        {
            case 10:
                grid[0].SetActive(true);
                MiniCam.max = 0.9f;
                break;
            case 15:
                grid[1].SetActive(true);
                MiniCam.max = 5.8f;
                break;
            case 20:
                grid[2].SetActive(true);
                MiniCam.max = 11f;
                break;
        }
    }
    
    public void Change_State(State state)
    {
        if (_state == state)
            return;

        if (_state == State.Shoot && state == State.Standby)
            player = null;
        if (_state == State.Standby && state == State.Play)
        {
            UI_Manager.manager.DrawCard(UsedDeck);
            if (UI_Manager.manager.Is_Gauge_Zero())
            {
                Time.timeScale = 0;
                Game_Fail();
            }
        }
        if (state == State.Menu)
            Time.timeScale = 0;
        else
            Time.timeScale = 1; 
            

        if (state == State.End)
        {
            Time.timeScale = 0;
            Game_Clear();
        }

        _state = state;
    }
    public void Wait(float time)
    {
        Invoke("Shoot", time);
    }
    void Shoot()
    {
        Change_State(State.Play);
    }
    public void Ball_Reset()
    {
        if (_state != State.Shoot)
            return;
        foreach (var ball in Ball.balls)
        {
            if (ball != null)
                Destroy(ball.gameObject);
        }
        Change_State(State.Standby);
        Ninja_Ball.die_count = 0;
        Ball.balls.Clear();
        Ninja_Ball.Ninja.Clear();
        MiniCam.posReset = true;
        player = null;
    }
    public void Fire_Ball_Collision_Flag()
    {
        StartCoroutine(Fire_Ball_Collision_Flag_());
    }
    IEnumerator Fire_Ball_Collision_Flag_()
    {
        yield return new WaitForSeconds(1);
        HS_ProjectileMover.flag = false;
    }
    
    public void Game_Clear()
    {
        clearPanel.SetActive(true);
        if (map == gameData.playerData.MaxMap) // 맵 첫 클리어시
        {
            gameData.playerData.MaxMap++;
            gameData.playerData.Money += 5000;
            SaveSystem.SavePlayerData(gameData, "save_1101"); // 파일저장
        }
        else if (map > gameData.playerData.MaxMap)
        {
            Debug.Log("Game_Clear script error");
        }
    }
    public void Game_Next_Map()
    {
        gameData.playerData.Map++;
        SaveSystem.SavePlayerData(gameData, "save_1101"); // 파일저장
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Game_ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Game_Exit()
    {
        SceneManager.LoadScene("Main_Scene");
    }

    public void Game_Fail()
    {
        failPanel.SetActive(true);
    }
}