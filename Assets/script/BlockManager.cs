using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    public int rows = 5;
    public int cols = 11;
    public float spacing = 1.5f;

    public Vector2 startPosition = new Vector2(-6.5f, 7.5f);
    public GameObject blocPrefab;
    private int blockRemaining;

    private GameObject[,] tabBlock;

    Dictionary<string, blockData.BlockType> blockType;
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
        tabBlock = new GameObject[rows, cols];
        for (int row = 0; row < rows; row++)
        {

            for (int col = 0; col < cols; col++)
            {
                tabBlock[row, col] = Instantiate(blocPrefab);

                float xPos = startPosition.x + (col * spacing);
                float yPos = startPosition.y - (row * spacing * 0.6f);


                tabBlock[row, col].transform.position = new Vector3(xPos, yPos, 0);
                blockRemaining++;
            }
        }
    }

    private void ReActiveBlock()
    {
        for (int row = 0; row < rows; row++)
        {

            for (int col = 0; col < cols; col++)
            {
                tabBlock[row, col].SetActive(true);
            }
        }


    }


}
