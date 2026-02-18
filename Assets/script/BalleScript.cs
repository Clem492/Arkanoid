using UnityEngine;
using static UnityEngine.UI.Image;

public class BalleScript : MonoBehaviour
{
    [SerializeField] int ballSpeed;

    private bool firstShoot = true;

    RaycastHit hit;

   public Vector2 currentDirection;

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
        Move();
    }

    private void Move()
    {
        if (!firstShoot)
        {
            transform.Translate(Vector3.right * Time.deltaTime * ballSpeed);
        }
        
    }

   private void calculateTrajectoir()
    {
        Vector3 origin = transform.position;
        Vector3 dir = transform.right;

        Ray ray = new Ray(origin, dir);
/*        Gizmos.DrawLine(origin, origin + dir);*/
        //tirer un raycast faire le produit scalaire entre les obstacles
        for (int i = 0; i < 200; i++)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                /*Gizmos.DrawLine(origin, hit.point);
                Gizmos.DrawSphere(hit.point, 0.1f);*/
                Vector3 normale = hit.normal;

                Vector3 reflet = Reflect(ray.direction, normale);
                /*Gizmos.DrawLine(hit.point, (Vector2)hit.point + reflet * 4);*/
                origin = hit.point;
                dir = reflet;
                ray = new Ray(origin, dir);
                transform.rotation = Quaternion.Euler(dir);
            }
            else
            {
                break;
            }
        }
    }

    Vector2 Reflect(Vector2 direction, Vector2 normale)
    {
        return direction - 2 * Vector2.Dot(normale, direction) * normale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            firstShoot = false;
            
        }
        calculateTrajectoir();
    }   



}
