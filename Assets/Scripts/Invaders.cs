using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] gridElementPrefab;  // The prefab for the grid elements
    public int rows = 5;                  // Number of rows in the grid
    public int columns = 5;               // Number of columns in the grid
    public float cellSize = 1.0f;         // Size of each cell in the grid

    private Vector3 moveDirection = Vector2.right;
    [SerializeField] private float speedMovement;

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
                Invader invader = Instantiate(gridElementPrefab[row], transform);
                invader.transform.localPosition = position;
            }
        }
    }

    private void Update()
    {
        //Move the Invaders
        transform.position += speedMovement * Time.deltaTime * moveDirection;

        //Get the right and left edges of the camera view
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // Debug.Log(rightEdge);

        float offsetX = 1.0f;

        foreach (Transform invader in this.transform)
        {   
            //IF the current current Invader is not active in the Hierarchy continue with the next one...
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            //Check if the current invader is moving to the right or left AND if has reached the edge of the screen
            if (moveDirection == Vector3.right && invader.position.x >= (rightEdge.x - offsetX))
            {
                AdvanceRow();
            }
            else if (moveDirection == Vector3.left && invader.position.x <= (leftEdge.x + offsetX))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        // Reverse the direction of the movement and move the Invaders one row down
        moveDirection.x *= -1f;

        Vector3 position = transform.position;
        position.y -= 1f;

        transform.position = position;
    }

}
