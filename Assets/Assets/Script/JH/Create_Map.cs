using UnityEngine;

public class Create_Map : MonoBehaviour
{
    enum Block
    {
        None,
        remove,
        nomal,
        diamond,
        heal,
        Indestructible
    }
    Block block = Block.None;
    GameObject obj;
    public GameObject[] Icon;
    public GameObject[] ball_buttons;
    public GameObject[] block_buttons;
    public GameObject nomal_block;
    public GameObject diamond_block;
    public GameObject heal_block;
    public GameObject Indestructible_block;
    public static bool isTurn;
    Camera miniCam;
    void Start()
    {
        miniCam = GetComponent<Camera>();
    }

    void Update()
    {
        if (isTurn && !Ball.isShoot)
        {
            Create_Block();
            Icon_Visible();
        }
        else
            Icon_Invisible();
    }
    public void My_Turn()
    {
        Scroll.isTurn = false;
        Ball.isTurn = false;
        isTurn = true;
    }

    void Create_Block()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = miniCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 viewportPos = miniCam.ScreenToViewportPoint(mousePos);
            if (viewportPos.x > 1 || viewportPos.x < 0 || viewportPos.y > 1 || viewportPos.y < 0)
                return;

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (block == Block.remove)
                    Destroy(hit.collider.gameObject);
                return;
            }

            float x = Mathf.Round(mousePos.x);
            float y = Mathf.Round(mousePos.y);
            mousePos.z = 0;

            if (Mathf.Abs(x) <= 2)
                mousePos.x = x;
            else
                return;

            if (-3 <= y && y <= 26)
                mousePos.y = y;
            else
                return;

            switch (block)
            {
                case Block.None:
                    return;
                case Block.remove:
                    return;
                case Block.nomal:
                    obj = nomal_block;
                    break;
                case Block.diamond:
                    obj = diamond_block;
                    break;
                case Block.heal:
                    obj = heal_block;
                    break;
                case Block.Indestructible:
                    obj = Indestructible_block;
                    break;
            }
            Instantiate(obj, mousePos, Quaternion.identity);
        }
    }
    void Icon_Visible()
    {
        Icon_Invisible();
        switch (block)
        {
            case Block.None:
                Icon[0].SetActive(true);
                break;
            case Block.remove:
                Icon[1].SetActive(true);
                break;
            case Block.nomal:
                Icon[2].SetActive(true);
                break;
            case Block.diamond:
                Icon[3].SetActive(true);
                break;
            case Block.heal:
                Icon[4].SetActive(true);
                break;
            case Block.Indestructible:
                Icon[5].SetActive(true);
                break;
        }
    }
    void Icon_Invisible()
    {
        foreach (var icon in Icon)
            if (icon.activeSelf == true)
                icon.SetActive(false);
    }
    public void Switch_Button()
    {
        if (block_buttons[0].activeSelf == true)
        {
            foreach (var button in ball_buttons)
                if (button != null)
                    button.SetActive(true);
            foreach (var button in block_buttons)
                if (button != null)
                    button.SetActive(false);
        }
        else
        {
            foreach (var button in block_buttons)
                if (button != null)
                    button.SetActive(true);
            foreach (var button in ball_buttons)
                if (button != null)
                    button.SetActive(false);
        }
    }
    public void Remove_Button()
    {
        block = Block.remove;
    }
    public void Nomal_Block_Button()
    {
        block = Block.nomal;
    }
    public void Diamond_Block_Button()
    {
        block = Block.diamond;
    }
    public void Heal_Block_Button()
    {
        block = Block.heal;
    }
    public void Indestructible_Block_Button()
    {
        block = Block.Indestructible;
    }
}
