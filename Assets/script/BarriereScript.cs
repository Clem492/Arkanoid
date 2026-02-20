using TMPro;
using UnityEngine;

public class BarriereScript : MonoBehaviour
{
    private TextMeshProUGUI LifeText;
    private int barriereLife;

    private void Awake()
    {
        LifeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        barriereLife = BarriereManager.instance.life;
        ShowLife();
    }


    void Update()
    {
        DestroyBarriere();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Balle"))
        {
            barriereLife--;
            ShowLife();

        }
    }

    private void ShowLife()
    {
        LifeText.text = barriereLife.ToString() + "/" + BarriereManager.instance.life;
    }

    private void DestroyBarriere()
    {
        if (barriereLife <= 0)
        {
            Destroy(gameObject);
        }
    }

   
}
