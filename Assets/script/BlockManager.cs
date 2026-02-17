using UnityEngine;

public class BlockManager : MonoBehaviour
{

    public int rows = 5;
    public int cols = 11;
    public float spacing = 1.5f;
    private GameObject[,] enemies;
    public Vector2 startPosition = new Vector2(-6.5f, 7.5f);
    public GameObject blocPrefab;
    private int blockRemaining;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnBlock()
    {

        for (int row = 0; row < rows; row++)
        {

            for (int col = 0; col < cols; col++)
            {
                GameObject block = Instantiate(blocPrefab);

                if (block != null)
                {
                    float xPos = startPosition.x + (col * spacing);
                    float yPos = startPosition.y - (row * spacing * 0.6f);


                    block.transform.position = new Vector3(xPos, yPos, 0);
                    blockRemaining++;



                }
            }
        }
    }
}
