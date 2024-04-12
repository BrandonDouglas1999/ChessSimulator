using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
    public Transform blackTile;
    public Transform whiteTile;
    // Start is called before the first frame update
    void Start()
    {
        makeBoard();
    }

    private void makeBoard()
    {
        for(int i = 0; i < 8; i++) //loops y axis
        {
            float z = i + .5f;
            if (i % 2 == 0) //if even row, print x axis
            {
                Instantiate(blackTile, new Vector3(.5F, 0, z), Quaternion.identity);
                Instantiate(whiteTile, new Vector3(1.5F, 0, z), Quaternion.identity);
                Instantiate(blackTile, new Vector3(2.5F, 0, z), Quaternion.identity);
                Instantiate(whiteTile, new Vector3(3.5F, 0, z), Quaternion.identity);
                Instantiate(blackTile, new Vector3(4.5F, 0, z), Quaternion.identity);
                Instantiate(whiteTile, new Vector3(5.5F, 0, z), Quaternion.identity);
                Instantiate(blackTile, new Vector3(6.5F, 0, z), Quaternion.identity);
                Instantiate(whiteTile, new Vector3(7.5F, 0, z), Quaternion.identity);
            }
            else //if odd row, print inverted x axis
            {
                Instantiate(whiteTile, new Vector3(0.5F, 0, z), Quaternion.identity);
                Instantiate(blackTile, new Vector3(1.5F, 0, z), Quaternion.identity);
                Instantiate(whiteTile, new Vector3(2.5F, 0, z), Quaternion.identity);
                Instantiate(blackTile, new Vector3(3.5F, 0, z), Quaternion.identity);
                Instantiate(whiteTile, new Vector3(4.5F, 0, z), Quaternion.identity);
                Instantiate(blackTile, new Vector3(5.5F, 0, z), Quaternion.identity);
                Instantiate(whiteTile, new Vector3(6.5F, 0, z), Quaternion.identity);
                Instantiate(blackTile, new Vector3(7.5F, 0, z), Quaternion.identity);
            }
        }
    }
}
