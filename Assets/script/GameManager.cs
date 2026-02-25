using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int life = 500;
    private int maxLife = 500;
    public int money = 0;

    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private TextMeshProUGUI textLife;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ShowMoney();
        textLife.text = life.ToString() + "/500";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseSpeed(GameObject collision, ref int balleSpeed)
    {
        blockData.BlockType blockType = BlockManager.instance.GetBlockType(collision);
        if (blockType != null)
        {
            balleSpeed = blockType.rebondSpeed;
        }
        else
        {
            Debug.Log("aucun type de bloc trouver");
        }
    }

    public void LooseLifeSave(GameObject collision)
    {
        blockData.BlockType blockType = BlockManager.instance.GetBlockType(collision);
        if (blockType != null)
        {
            life -= blockType.Dommage;
            textLife.text = life.ToString() + "/500";
        }
        else
        {
            Debug.Log("aucun type de bloc trouver");
        }

    }

    public void LooseLife()
    {
        life -= 20;
        textLife.text = life.ToString() + "/500";
    }

    public void AddLife()
    {
        life += 40;
        if (life > maxLife)
        {
            life = maxLife;
        }
        textLife.text = life.ToString() + "/500";
    }

    public void AddMoney(GameObject collision)
    {
        blockData.BlockType blockType = BlockManager.instance.GetBlockType(collision);
        if (blockType != null)
        {
            money += blockType.MoneyValue;
            ShowMoney();
        }
        else
        {
            Debug.Log("aucun type de bloc trouver");
        }

    }


    public void ShowMoney()
    {
        textMoney.text = money.ToString();
    }

    //TODO : implémenter le game over
    //TODO ! corriger bug de la balle en dehors de la map
}
