using UnityEngine;

public class BalleScript : MonoBehaviour
{
    [SerializeField] int ballSpeed;



    RaycastHit hit;

    public Vector3 currentDirection;


    private void Start()
    {
        currentDirection = Vector3.down;
    }

    void Update()
    {

        Move();
    }


    private void Move()
    {

        transform.Translate(currentDirection * Time.deltaTime * ballSpeed);


    }



    Vector2 Reflect(Vector2 direction, Vector2 normale)
    {
        return direction - 2 * Vector2.Dot(normale, direction) * normale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            float x = transform.position.x - collision.transform.position.x;
            float length = collision.bounds.size.x;

            float directionX = x / length;

            currentDirection = new Vector2(directionX, 1f).normalized;
        }
        else if (collision.CompareTag("Block"))
        {
            Vector2 normal = (new Vector2(transform.position.x, transform.position.y) - collision.ClosestPoint(transform.position)).normalized;
            currentDirection = Vector2.Reflect(currentDirection, normal);
            //TODO : implémenter l'augmentation de l'argent
            collision.gameObject.SetActive(false);
        }
        else
        {
            Vector2 normal = (new Vector2(transform.position.x, transform.position.y) - collision.ClosestPoint(transform.position)).normalized;
            currentDirection = Vector2.Reflect(currentDirection, normal);
        }

    }



}
