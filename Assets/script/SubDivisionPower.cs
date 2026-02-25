using System.Collections.Generic;
using UnityEngine;

public class SubDivisionPower : MonoBehaviour
{
    [SerializeField] private GameObject ballePrefab;
    public List<GameObject> allBalle;
    private int allballeSize;
    [SerializeField] private int MoneyRequiredToSubDivision;




    private void Awake()
    {
        allBalle = new List<GameObject>();
        allBalle.Add(GameObject.FindWithTag("Balle"));

    }

    void Start()
    {

        Debug.Log(allBalle.Count);
    }




    private void Update()
    {
        SubDivision();

    }

    private void SubDivision()
    {
        if (GameManager.instance.money >= MoneyRequiredToSubDivision)
        {
           
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            allballeSize = allBalle.Count;
            GameManager.instance.money -= MoneyRequiredToSubDivision;
            GameManager.instance.ShowMoney();
            GameManager.instance.AddLife();
            for (int i = 0; i < allballeSize; i++)
            {
                GameObject newBalle = Instantiate(ballePrefab, allBalle[i].transform.position, Quaternion.Euler(0, 0, 0));
                allBalle.Add(newBalle);

            }
        }

    }
}
