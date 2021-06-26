using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject TilePrefab;
    public float xDiff = 1.1f;
    public float yDiffSmall = 0.4f;
    public float yDiffBig = 1.35f;

    private float xPos = -2.5f;
    private float yPos = -4.5f;

    private string smallTile = "smallTile";
    private string bigTile = "bigTile";
    // Start is called before the first frame update
    void Start()
    {
       for(int i=0; i<7; ++i)
        {
            GenerateTiles();
        } 
    }

    // Update is called once per frame
    public void GenerateTiles()
    {
        int random = Random.Range(0, 5);
        if (random <= 2)
        {
            GenerateSmallTile();
        }
        else
            GenerateBigTile();
    }
    void GenerateSmallTile()
    {
        xPos += xDiff;
        yPos += yDiffSmall;
        TilePrefab.tag = smallTile;
        Instantiate(TilePrefab, new Vector3(xPos, yPos, 0), TilePrefab.transform.rotation);
    }
    void GenerateBigTile() 
    {
        xPos += xDiff;
        yPos += yDiffBig;
        TilePrefab.tag = bigTile;
        Instantiate(TilePrefab, new Vector3(xPos, yPos, 0), TilePrefab.transform.rotation);

    }
}
