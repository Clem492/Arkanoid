using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int life = 500;
    public int money =0;
 
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

        textMoney.text = money.ToString();
        textLife.text = life.ToString() +"/500";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(life);
    }

    

    public void LooseLife(GameObject collision)
    {
        blockData.BlockType blockType = BlockManager.instance.GetBlockType(collision);
        if (blockType == null)
        {
            Debug.Log("je n'ai pas de blocType");
        }
        life -= blockType.Dommage;
        textLife.text = life.ToString() + "/500";
    }

    public void AddMoney(GameObject collision)
    {
        blockData.BlockType blockType = BlockManager.instance.GetBlockType(collision);
        money += blockType.MoneyValue;
        textMoney.text = money.ToString();
    }


}
