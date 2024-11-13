using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum State
{
    Play,
    Menu
}
public class GameManager : MonoBehaviour
{
    public GameObject a;
    public GameObject a_1;
    public GameObject a_2;
    public GameObject b;
    public GameObject b_1;
    public GameObject c;
    public GameObject c_1;
    public GameObject d;
    public GameObject d_1;

    /////////
    public State _state;
    public static GameManager manager;
    public GameObject player;
    public int count;
    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(gameObject);
    }
    void Start()
    {

    }

    void Update()
    {

    }

    void Game_State()
    {

    }
    public void Game_ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Change_State(State state)
    {
        _state = state;
        if (_state == State.Menu)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void Wait(float time)
    {
        Invoke("IsShoot", time);
    }
    void IsShoot()
    {
        Ball.isShoot = false;
    }
    public void Ball_Reset()
    {
        foreach (var ball in Ball.balls)
        {
            if (ball != null)
                Destroy(ball.gameObject);
        }
        Ball.balls.Clear();
        Ninja_Ball.count = 0;
        MiniCam.posReset = true;
        player = null;
    }
    public void Deck_Reset()
    {
        if (GameObject.Find("Nomal Ball_UI") == null)
        {
            GameObject obj = Instantiate(a_1, a.transform.position, Quaternion.identity);
            obj.transform.SetParent(a.transform);
            obj.name = "Nomal Ball_UI";
        }
        if (GameObject.Find("Nomal Ball_UI2") == null)
        {
            GameObject obj = Instantiate(a_2, a.transform.position + Vector3.up * 100, Quaternion.identity);
            obj.transform.SetParent(a.transform);
            obj.name = "Nomal Ball_UI2";
        }
        if (GameObject.Find("Nomal Ball2_UI") == null)
        {
            GameObject obj = Instantiate(b_1, b.transform.position, Quaternion.identity);
            obj.transform.SetParent(b.transform);
            obj.name = "Nomal Ball2_UI";
        }
        if (GameObject.Find("Nomal Ball3_UI") == null)
        {
            GameObject obj = Instantiate(c_1, c.transform.position, Quaternion.identity);
            obj.transform.SetParent(c.transform);
            obj.name = "Nomal Ball3_UI";
        }
        if (GameObject.Find("Fire_Spell") == null)
        {
            GameObject obj = Instantiate(d_1, d.transform.position, Quaternion.identity);
            obj.transform.SetParent(d.transform);
            obj.name = "Fire_Spell";
        }
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