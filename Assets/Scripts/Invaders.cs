using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] invaderPrefab = new Invader[5];

    public int rows = 5;
    public int columns = 11;

    private void Awake()
    {
        // prefabs = new Invader[prefabs.Length];


        for (int row = 0; row < this.rows; row++)
        {
            for (int column = 0; column < this.columns; column++)
            {
                Instantiate(invaderPrefab[row], transform);
            }
        }
    }
}
