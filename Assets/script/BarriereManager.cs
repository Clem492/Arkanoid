using UnityEngine;

public class BarriereManager : MonoBehaviour
{
    public static BarriereManager instance;

    [SerializeField] private GameObject barrierePrefab;
    [SerializeField] private Camera cam;


    //vie du bloc
    public int life;
    public int maxLife = 10;

    //scale du bloc
    public Vector2 scale;
    public Vector2 maxeScale;

    //argent requis
    public int moneyRequiredToSpawn;


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

    void Update()
    {
        if (GameManager.instance.money >= moneyRequiredToSpawn)
        {
            SpawnBarriere();

        }

    }


    public void IncreaseScaleX()
    {
        //augmenter la taille max
        if (scale != maxeScale)
        {
            scale.x += 0.2f;
        }
    }

    public void IncreaseLife()
    {
        //augmenter la vie max
        if (life != maxLife)
        {
            life++;
        }
    }

    private void SpawnBarriere()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction);
            if (Physics.Raycast(ray, out hit) && ray.origin.y < 0)
            {
                if (hit.transform.CompareTag("floor"))
                {
                    Instantiate(barrierePrefab, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);
                    GameManager.instance.money -= moneyRequiredToSpawn;
                    GameManager.instance.ShowMoney();
                }

            }
        }
    }
}
