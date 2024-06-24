using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Invader gridElementPrefab;  // The prefab for the grid elements
    public int rows = 5;                  // Number of rows in the grid
    public int columns = 5;               // Number of columns in the grid
    public float cellSize = 1.0f;         // Size of each cell in the grid

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        // Calculate the total width and height of the grid
        float gridWidth = columns * cellSize;
        float gridHeight = rows * cellSize;

        // Calculate the starting position of the grid to center it
        Vector3 startPosition = new Vector3(-gridWidth / 2 + cellSize / 2, -gridHeight / 2 + cellSize / 2, 0);
        // Debug.Log(startPosition);

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // Calculate the position for each grid element
                Vector3 position = startPosition + new Vector3(column * cellSize, row * cellSize, 0);
                
                // Instantiate the grid element at the calculated position
                // Instantiate(gridElementPrefab, position, Quaternion.identity);
                Invader invader = Instantiate(gridElementPrefab, transform);
                invader.transform.localPosition = position;
            }
        }
    }


}
