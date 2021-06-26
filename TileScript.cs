using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    float yPos;
    Generator _Generator;

    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
        _Generator = GameObject.Find("TilesGenerator").GetComponent<Generator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < yPos -10f)
        {
            _Generator.GenerateTiles();
            Destroy(this.gameObject);
        }
    }
}
