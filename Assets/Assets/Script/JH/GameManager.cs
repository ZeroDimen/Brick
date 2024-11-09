using UnityEngine;
public enum State
{
    Play,
    Menu
}
public class GameManager : MonoBehaviour
{
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
    public void Change_State(State state)
    {
        _state = state;
    }
}