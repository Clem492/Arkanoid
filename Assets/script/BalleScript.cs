using System.Collections.Generic;
using UnityEngine;

public class BalleScript : MonoBehaviour
{
    [SerializeField] int ballSpeed;



    RaycastHit hit;

    public Vector3 currentDirection;

    private GameObject blockSave;
    
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
            blockSave = BlockManager.instance.GetBlockTouched(collision.gameObject);
            Vector2 normal = (new Vector2(transform.position.x, transform.position.y) - collision.ClosestPoint(transform.position)).normalized;
            currentDirection = Vector2.Reflect(currentDirection, normal);
            GameManager.instance.AddMoney(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("lifeBlock"))
        {
            if (blockSave != null)
            {
                GameManager.instance.LooseLife(blockSave);
            }
            Vector2 normal = (new Vector2(transform.position.x, transform.position.y) - collision.ClosestPoint(transform.position)).normalized;
            currentDirection = Vector2.Reflect(currentDirection, normal);
        }
        else
        {
            Vector2 normal = (new Vector2(transform.position.x, transform.position.y) - collision.ClosestPoint(transform.position)).normalized;
            currentDirection = Vector2.Reflect(currentDirection, normal);
        }

    }



}
