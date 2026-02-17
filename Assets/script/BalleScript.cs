using UnityEngine;

public class BalleScript : MonoBehaviour
{
    [SerializeField] int ballSpeed;

    public Vector3 direction;

    private bool firstShoot = true;

    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        FirstMove();
        Move();
    }

    private void FirstMove()
    {
        if (firstShoot)
        {
            transform.Translate(Vector3.down * Time.deltaTime * ballSpeed);
        }
    }

    private void Move()
    {
        transform.Translate(direction.x, direction.y, direction.z);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            firstShoot = false;
            Vector2 normal = (new Vector2(transform.position.x, transform.position.y) - collision.ClosestPoint(transform.position)).normalized;
            direction = Vector2.Reflect(direction, normal);
        }

    }   

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, direction);
    }

}
