using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;

    public int life;
    public int money;


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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney(blockData blockData, GameObject collision)
    {

    }

}
