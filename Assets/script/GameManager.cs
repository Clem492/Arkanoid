using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int life = 500;
    private int maxLife = 500;
    public int money = 0;

    public int Level = 1;

    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private TextMeshProUGUI textLife;
    [SerializeField] private TextMeshProUGUI textLevel;

    [SerializeField] private GameObject balle;
    private GameObject[] BriqueToDestroy;
    private GameObject[] BalleToDestroy;

    public bool newLevel = false;
    public bool isPaused = false;

    [SerializeField] SubDivisionPower subDivisionPower;

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
        textLevel.text ="Level : " + Level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        NextLevel();
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

    private void NextLevel()
    {
        if (BlockManager.instance.blockRemaining <= 0 && !newLevel)
        {
            newLevel = true;
           /* money = 0;*/
            IncreaseLevel();
            StartCoroutine(NewLevel());
            return;
        }
    }

    public void IncreaseLevel()
    {
        Level++;
        textLevel.text = "Level : " + Level.ToString();
    }

    public IEnumerator NewLevel()
    {
        isPaused = true;
        balle.transform.position = new Vector3(0,0,0);
        //détruire les blocs déja existant mais désactiver
        BriqueToDestroy = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject go in BriqueToDestroy)
        {
            Destroy(go);
        }
        //detruire les balle de trop
        BalleToDestroy = GameObject.FindGameObjectsWithTag("BalleClone");
        foreach(GameObject go in BalleToDestroy)
        {
            subDivisionPower.allBalle.Clear();
            subDivisionPower.allBalle.Add(GameObject.FindWithTag("Balle"));
            Destroy(go);
        }
        //faire spawn les nouvelle brique 
        BlockManager.instance.SpawnBlock();
        yield return new WaitForSeconds(5);
        isPaused = false;   
    }


    public void ShowMoney()
    {
        textMoney.text = money.ToString();
    }



    //TODO : implémenter le game over
    //TODO ! corriger bug de la balle en dehors de la map
}
