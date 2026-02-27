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
    private int spawningBasic = 50;
    private int spawningElementaire = 20;
    private int spawningRare = 10;
    private int spawningLegendaire = 3;
    private int spawningMythique = 0;



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
        WhatLevel();
    }


    public void SpawnBlock()
    {
        tabBlock = new GameObject[rows, cols];
        for (int row = 0; row < rows; row++)
        {

            for (int col = 0; col < cols; col++)
            {
               int spawningProb  = Random.Range(0, 100);
                if (spawningProb >= spawningBasic)
                {
                    //instancer le basique
                    tabBlock[row, col] = Instantiate(blocPrefab[0]);
                }
                else if (spawningProb >= spawningElementaire)
                {
                    //instantier l'élémentaire
                    tabBlock[row, col] = Instantiate(blocPrefab[1]);
                }
                else if (spawningProb >= spawningRare)
                {
                    //instantierle Rare
                    tabBlock[row, col] = Instantiate(blocPrefab[2]);
                }
                else if (spawningProb >= spawningLegendaire)
                {
                    //instantier le Legendaire
                    tabBlock[row, col] = Instantiate(blocPrefab[3]);
                }
                else if (spawningProb >= spawningMythique)
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



    private void WhatLevel()
    {
        if (GameManager.instance.Level > 1 && GameManager.instance.Level <3)
        {
            spawningBasic = 70;
            spawningElementaire = 30;
            spawningRare = 15;
            spawningLegendaire = 5;
            spawningMythique = 0;
        }
        if (GameManager.instance.Level >= 3 && GameManager.instance.Level <5)
        {
            spawningBasic = 90;
            spawningElementaire = 50;
            spawningRare = 25;
            spawningLegendaire = 10;
            spawningMythique = 0;
        }
        if (GameManager.instance.Level >= 5 && GameManager.instance.Level <7)
        {
            spawningBasic =100;
            spawningElementaire = 70;
            spawningRare = 30;
            spawningLegendaire = 10;
            spawningMythique = 0;
        }
        if (GameManager.instance.Level >= 7 && GameManager.instance.Level <9)
        {
            spawningBasic = 100;
            spawningElementaire = 90;
            spawningRare = 50;
            spawningLegendaire = 20;
            spawningMythique = 0;
        }
        if (GameManager.instance.Level >= 9 && GameManager.instance.Level <11)
        {
            spawningBasic = 100;
            spawningElementaire = 100;
            spawningRare = 85;
            spawningLegendaire = 40;
            spawningMythique = 0;
        }
        if (GameManager.instance.Level >= 11 && GameManager.instance.Level <13)
        {
            spawningBasic = 100;
            spawningElementaire = 100;
            spawningRare = 100;
            spawningLegendaire = 70;
            spawningMythique = 0;
        }
        if (GameManager.instance.Level >= 13 && GameManager.instance.Level < 15)
        {
            spawningBasic = 100;
            spawningElementaire = 100;
            spawningRare = 100;
            spawningLegendaire = 100;
            spawningMythique = 0;
        }
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
