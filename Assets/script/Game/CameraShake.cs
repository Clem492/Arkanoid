using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount = 0.02f;
    private Vector3 initalPos;
    public float timer = 0.2f;


    private void Awake()
    {
        initalPos = transform.position;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            transform.position = initalPos;
            GameManager.instance.cameraShake = false;
            timer = 0f;
        }
        else
        {
            transform.position = initalPos + Random.insideUnitSphere * shakeAmount;

        }
       
        

    }
}
