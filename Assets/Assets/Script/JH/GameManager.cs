using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Player_Data;
public enum State
{
    Play,
    Shoot,
    Standby,
    Menu
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
    
    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(gameObject);
        
        gameData = SaveSystem.LoadPlayerData("save_1101"); // 파일 읽기 
        map = gameData.playerData.Map; // 저장된 맵 번호
        for (int i = 0; i < 8; i++)
            cardName[i] = gameData.cardDataList.Cards[i].CardName; // 이름
        stage[map].SetActive(true);
    }
    private void Update()
    {
        if (_state == State.Shoot)
        {
            play_time += Time.deltaTime;
            if (play_time > 5)
                Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
            play_time = 0;
        }
    }

    public void Game_ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Change_State(State state)
    {
        if (_state == state)
            return;

        if (_state == State.Shoot && state == State.Standby)
            player = null;
        if (_state == State.Standby && state == State.Play)
            UI_Manager.manager.DrawCard(UsedDeck);

        if (state == State.Menu)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

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
    public void Ball_Reset(State state = State.Play)
    {
        foreach (var ball in Ball.balls)
        {
            if (ball != null)
                Destroy(ball.gameObject);
        }
        Change_State(State.Play);
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
}