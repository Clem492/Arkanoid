using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int life = 500;
    public int increaseLife = 40;
    private int maxLife = 500;
    public int money = 0;

    public int Level = 1;

    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private TextMeshProUGUI textMoneyAmelioration;
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

    [SerializeField] private GameObject paddle;
    [SerializeField] private float scaleFactorPaddel;
    [SerializeField] private GameObject barrierPrefab;
    [SerializeField] private float scaleFactorBarrier;
    [SerializeField] private float scaleFactorBall;
    [SerializeField] private int lifeFactorRegen;
    [SerializeField] private int lifeFactorBarrier;
    [SerializeField] private int costFactorAmelioration;
    public bool finishAmelioration = false;
    private void Awake()
    {
        panelAmelioration.gameObject.SetActive(false);
        barrierPrefab.transform.localScale = Vector3.one;
        paddle.transform.localScale = new Vector3(1.5f, 0.3f, 1);
        balle.transform.localScale = Vector3.one / 2;
    }

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
        textLevel.text = "Level : " + Level.ToString();

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
        balle.transform.position = new Vector3(0, 0, 0);
        //détruire les blocs déja existant mais désactiver
        BriqueToDestroy = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject go in BriqueToDestroy)
        {
            Destroy(go);
        }
        //detruire les balle de trop
        BalleToDestroy = GameObject.FindGameObjectsWithTag("BalleClone");
        foreach (GameObject go in BalleToDestroy)
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
        //faire l'amélioration
        panelAmelioration.gameObject.SetActive(true);
        PutAmeliorationInPanel();
        ShowMoney();
        yield return new WaitUntil(() => finishAmelioration);
        finishAmelioration = false;
        panelAmelioration.gameObject.SetActive(false);
        //faire spawn les nouvelle brique 
        BlockManager.instance.SpawnBlock();
        yield return new WaitForSeconds(5);
        isPaused = false;
    }


    public void ShowMoney()
    {
        textMoney.text = money.ToString();
        textMoneyAmelioration.text = money.ToString();
    }







    private Vector3 IncreaseSizePaddle()
    {
        Vector3 newScale = new Vector3(scaleFactorPaddel, 0, 0);
        paddle.transform.localScale += newScale;
        return paddle.transform.localScale;
    }

    private Vector3 IncreaseSizeBarrier()
    {
        Vector3 newScale = new Vector3(scaleFactorBarrier, 0, 0);
        barrierPrefab.transform.localScale += newScale;
        return barrierPrefab.transform.localScale;
    }

    private Vector3 IncreaseSizeBall()
    {
        Vector3 newScale = new Vector3(scaleFactorBall, scaleFactorBall, scaleFactorBall);
        balle.transform.localScale += newScale;
        return balle.transform.localScale;
    }

    private int IncreaseYourLife()
    {
        increaseLife += lifeFactorRegen;
        return increaseLife;
    }

    private int IncreaseLifeBarrier()
    {
        BarriereManager.instance.life += lifeFactorBarrier;
        return BarriereManager.instance.life;
    }

    public void SkipAmelioraton()
    {
        StartCoroutine(CoroutineSkipAmelioration());
    }

    private IEnumerator CoroutineSkipAmelioration()
    {
        finishAmelioration = true;
        yield return new WaitForSeconds(0.5f);
        finishAmelioration = false;

    }

    public void DoAmelioration(int panelIndex)
    {
        string ameliorationName =
        panelAmeliorationType[panelIndex]
        .GetComponentsInChildren<TextMeshProUGUI>()[0].text;

        switch (ameliorationName)
        {
            case "Paddle so long":
                if (money >= ameliorationDatas[0].listAmeliration[3].cost)
                {
                    IncreaseSizePaddle();
                    money -= ameliorationDatas[0].listAmeliration[3].cost;
                    ameliorationDatas[0].listAmeliration[3].cost += costFactorAmelioration;
                    finishAmelioration = true;
                }

                break;

            case "Barrier life":
                if (money >= ameliorationDatas[0].listAmeliration[0].cost)
                {
                    IncreaseLifeBarrier();
                    money -= ameliorationDatas[0].listAmeliration[0].cost;
                    ameliorationDatas[0].listAmeliration[0].cost += costFactorAmelioration;
                    finishAmelioration = true;
                }

                break;

            case "Regen your life":
                if (money >= ameliorationDatas[0].listAmeliration[1].cost)
                {
                    IncreaseYourLife();
                    money -= ameliorationDatas[0].listAmeliration[1].cost;
                    ameliorationDatas[0].listAmeliration[1].cost += costFactorAmelioration;
                    finishAmelioration = true;
                }

                break;

            case "incredible ball":

                if (money >= ameliorationDatas[0].listAmeliration[4].cost)
                {
                    IncreaseSizeBall();
                    money -= ameliorationDatas[0].listAmeliration[4].cost;
                    ameliorationDatas[0].listAmeliration[4].cost += costFactorAmelioration;
                    finishAmelioration = true;
                }
                break;

            case "Barrier so long":
                if (money >= ameliorationDatas[0].listAmeliration[2].cost)
                {
                    IncreaseSizeBarrier();
                    money -= ameliorationDatas[0].listAmeliration[2].cost;
                    ameliorationDatas[0].listAmeliration[2].cost += costFactorAmelioration;
                    finishAmelioration = true;
                }
                break;
        }
    }


    private void PutAmeliorationInPanel()
    {

        for (int i = 0; i < panelAmeliorationType.Length; i++)
        {
            int random = Random.Range(0, ameliorationDatas[0].listAmeliration.Count);
            panelAmeliorationType[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = ameliorationDatas[0].listAmeliration[random].name;
            panelAmeliorationType[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = ameliorationDatas[0].listAmeliration[random].description;
            panelAmeliorationType[i].GetComponentsInChildren<TextMeshProUGUI>()[2].text = "Purchase cost : " + ameliorationDatas[0].listAmeliration[random].cost.ToString();
            panelAmeliorationType[i].GetComponentsInChildren<Image>()[1].sprite = ameliorationDatas[0].listAmeliration[random].sprite;
        }
    }



    //TODO : implémenter le game over
    //TODO : Faire le menu
}
