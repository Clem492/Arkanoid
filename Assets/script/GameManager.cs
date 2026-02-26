using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int life = 500;
    public int increaseLife = 40;
    private int maxLife = 500;
    public int money = 0;

    public int Level = 1;

    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private TextMeshProUGUI textLife;
    [SerializeField] private TextMeshProUGUI textLevel;

    [SerializeField] private GameObject balle;
    private GameObject[] BriqueToDestroy;
    private GameObject[] BalleToDestroy;
    private GameObject[] BarriereToDestroy;

    public bool newLevel = false;
    public bool isPaused = false;

    [SerializeField] SubDivisionPower subDivisionPower;
    //récupération du panel et des donné pour les afficher
    [SerializeField] List<AmeliorationData> ameliorationDatas;
    [SerializeField] RectTransform[] panelAmeliorationType;
    [SerializeField] RectTransform panelAmelioration;

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
        PutAmeliorationInPanel();
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
        life += increaseLife;
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
            money = 0;
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
        BarriereToDestroy = GameObject.FindGameObjectsWithTag("barriere");
        foreach (GameObject go in BarriereToDestroy)
        {
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
    //TODO : les amélioration 
        //la vie des barrière
        //la vie du regen
        //la taille des barrièe
        //la taille du padle
        //la taille de la balle
    private void PutAmeliorationInPanel()
    {
        for (int i = 0; i < panelAmeliorationType.Length; i++)
        {
            panelAmeliorationType[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = new string("test");
        }
    }
}
