using UnityEngine;

public class BalleCloneScript : MonoBehaviour
{
    [SerializeField] int ballSpeed;

    public SpriteRenderer ballColor;


    public Vector3 currentDirection;

    private GameObject blockSave;
    [SerializeField] float timeToDeath;
    private SubDivisionPower subDivisionPower;
    private CameraShake cameraShake;

    private void Awake()
    {
        subDivisionPower = GameObject.FindWithTag("GameManager").GetComponent<SubDivisionPower>();
        cameraShake = GameObject.FindWithTag("MainCamera").GetComponent <CameraShake>();
        blockSave = null;
        ballColor = GetComponent<SpriteRenderer>();
        currentDirection = Vector3.down;
    }

    private void Start()
    {


    }

    void Update()
    {

        Move();
        VerifcationPosition();
        Debug.Log(blockSave);
        timeToDeath -= Time.deltaTime;
        if (timeToDeath < 0)
        {
            subDivisionPower.allBalle .Remove(gameObject);
            Destroy(gameObject);
        }
    }


    private void Move()
    {

        transform.Translate(currentDirection * Time.deltaTime * ballSpeed);


    }




    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            if (blockSave == null)
            {
                ballColor.color = Color.magenta;
                ballSpeed = 3;
            }

            float x = transform.position.x - collision.transform.position.x;
            float length = collision.bounds.size.x;

            float directionX = x / length;

            currentDirection = new Vector2(directionX, 1f).normalized;
        }
        else if (collision.CompareTag("Block"))
        {
            BlockManager.instance.blockRemaining--;
            //récupérer le bloc toucher et le sauvegarder pour savoir quelle bloc a été toucher
            blockSave = collision.gameObject;
            if (blockSave == null)
            {
                Debug.Log("je n'ai pas trouver de bloc !");
            }

            //fait le rebond de la balle
            Vector2 normal = (new Vector2(transform.position.x, transform.position.y) - collision.ClosestPoint(transform.position)).normalized;
            currentDirection = Vector2.Reflect(currentDirection, normal);

            //change la vitesse et on ajoute la monnaie
            GameManager.instance.IncreaseSpeed(collision.gameObject, ref ballSpeed);
            GameManager.instance.AddMoney(collision.gameObject);

            //on change la couleur en fonction de l'état de la balle
            ballColor.color = collision.GetComponent<SpriteRenderer>().color;


            //on détruit le bloc
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("lifeBlock"))
        {
            Debug.Log("LifeBlock");

            if (blockSave != null)
            {
                Debug.Log("entre en collision");
                GameManager.instance.LooseLifeSave(blockSave);
                GameManager.instance.cameraShake = true;
                cameraShake.timer += 0.2f;
                blockSave = null;
                ballSpeed = 3;
                ballColor.color = Color.magenta;
            }
            else
            {
                GameManager.instance.LooseLife();
                GameManager.instance.cameraShake = true;
                cameraShake.timer += 0.2f;
            }
                Vector2 normal = (new Vector2(transform.position.x, transform.position.y) - collision.ClosestPoint(transform.position)).normalized;
            currentDirection = Vector2.Reflect(currentDirection, normal);
        }
        else if (collision.gameObject.CompareTag("barriere"))
        {
            blockSave = null;
            ballColor.color = Color.magenta;
            ballSpeed = 3;
            float x = transform.position.x - collision.transform.position.x;
            float length = collision.bounds.size.x;

            float directionX = x / length;

            currentDirection = new Vector2(directionX, 1f).normalized;
        }
        else
        {
            if (blockSave == null)
            {
                ballColor.color = Color.magenta;
                ballSpeed = 3;
            }

            Vector2 normal = (new Vector2(transform.position.x, transform.position.y) - collision.ClosestPoint(transform.position)).normalized;
            currentDirection = Vector2.Reflect(currentDirection, normal);
        }

    }



    private void VerifcationPosition()
    {
        if ((transform.position.x > 9.5 || transform.position.x < -9.5) ||  (transform.position.y > 5.5 || transform.position.y < -4.5f))
        {
            subDivisionPower.allBalle.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
