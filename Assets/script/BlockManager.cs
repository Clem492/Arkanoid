using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance;
    public int rows = 5;
    public int cols = 11;
    public float spacing = 1.5f;

    public Vector2 startPosition = new Vector2(-6.5f, 7.5f);
    public GameObject[] blocPrefab;
    public int blockRemaining;

    private GameObject[,] tabBlock;

    public List<blockData> blockDatas;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        SpawnBlock();
        int spawningProb = Random.Range(0, 4);

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SpawnBlock()
    {
        tabBlock = new GameObject[rows, cols];
        for (int row = 0; row < rows; row++)
        {

            for (int col = 0; col < cols; col++)
            {
                int spawningProb = Random.Range(0, 100);
                if (spawningProb >= 50)
                {
                    //instancer le basique
                    tabBlock[row, col] = Instantiate(blocPrefab[0]);
                }
                else if (spawningProb >= 20)
                {
                    //instantier l'élémentaire
                    tabBlock[row, col] = Instantiate(blocPrefab[1]);
                }
                else if (spawningProb >= 10)
                {
                    //instantierle Rare
                    tabBlock[row, col] = Instantiate(blocPrefab[2]);
                }
                else if (spawningProb >= 3)
                {
                    //instantier le Legendaire
                    tabBlock[row, col] = Instantiate(blocPrefab[3]);
                }
                else if (spawningProb >= 0)
                {
                    //instantier le Mythique
                    tabBlock[row, col] = Instantiate(blocPrefab[4]);
                }


                float xPos = startPosition.x + (col * spacing);
                float yPos = startPosition.y - (row * spacing * 0.6f);


                tabBlock[row, col].transform.position = new Vector3(xPos, yPos, 0);
                
            }
        }
        blockRemaining = tabBlock.Length;
    }




    public blockData.BlockType GetBlockType(GameObject block)
    {

        if (block.name.Contains("Basic"))
        {
            return blockDatas[0].blockTypes[0];
        }
        else if (block.name.Contains("Elementaire"))
        {
            return blockDatas[0].blockTypes[1];
        }
        else if (block.name.Contains("Rare"))
        {
            return blockDatas[0].blockTypes[2];
        }
        else if (block.name.Contains("Legendaire"))
        {
            return blockDatas[0].blockTypes[3];
        }
        else if (block.name.Contains("Mythique"))
        {
            return blockDatas[0].blockTypes[4];
        }
        else
        {
            Debug.LogError("Aucun Bloc trouver");
            return null;
        }
    }

}
