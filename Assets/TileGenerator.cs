using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileGenerator : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    private List<GameObject> _tiles;

    void Start()
    {
        for (int i = 0; i <= 10; ++i)
        {
            for (int j = 0; j <= 10; ++j)
            {

                int index = Random.Range(0, 6);
                int indexRota = Random.Range(0, 3);

                GameObject.Instantiate(_tiles[index], new Vector3(40 * i, 0.01f, 40 * j), Quaternion.Euler(new Vector3(0f, indexRota * 90, 0f)));

            }


        }
    }


      

      
    }

    
