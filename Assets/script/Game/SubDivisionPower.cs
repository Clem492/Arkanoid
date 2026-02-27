using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDivisionPower : MonoBehaviour
{
    [SerializeField] private GameObject ballePrefab;
    public List<GameObject> allBalle;
    private int allballeSize;
    [SerializeField] private int MoneyRequiredToSubDivision;
    [SerializeField] private ParticleSystem heal;



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

    private IEnumerator Heal()
    {
        heal.Play();
        yield return new WaitForSeconds(2);
        heal.Stop();
    }

    private void SubDivision()
    {
        if (GameManager.instance.money >= MoneyRequiredToSubDivision)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                allballeSize = allBalle.Count;
                GameManager.instance.money -= MoneyRequiredToSubDivision;
                GameManager.instance.ShowMoney();
                GameManager.instance.AddLife();
                StartCoroutine(Heal());
                for (int i = 0; i < allballeSize; i++)
                {
                    GameObject newBalle = Instantiate(ballePrefab, allBalle[i].transform.position, Quaternion.Euler(0, 0, 0));
                    allBalle.Add(newBalle);

                }
            }
        }
        

    }
}
