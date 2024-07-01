using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] gridElementPrefab;  // The prefab for the grid elements
    public Proyectile missilePrefab;
    public int rows = 5;                  // Number of rows in the grid
    public int columns = 5;               // Number of columns in the grid
    public float cellSize = 1.0f;         // Size of each cell in the grid
    private Vector3 moveDirection = Vector2.right;
    public AnimationCurve speedMovement;
    public float speedMultiplier = 2f;
    //How often missiles spawn
    public float missileAttackRate = 1f;
    public int amountKilled { get; private set; }

    // Read-only property that calculates the total number of invaders
    public int TotalInvaders => rows * columns;
    public float PercentKilled => (float)amountKilled / TotalInvaders;
    public int amountAlive => TotalInvaders - amountKilled;

    void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileAttackRate, missileAttackRate);

        CreateGrid();
    }

    void CreateGrid()
    {
        // Calculate the total width and height of the grid
        float gridWidth = columns * cellSize;
        float gridHeight = rows * cellSize;

        // Calculate the starting position of the grid to center it
        Vector3 startPosition = new Vector3(-gridWidth / 2 + cellSize / 2, -gridHeight / 2 + cellSize / 2, 0f);
        // Debug.Log(startPosition);

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // Calculate the position for each grid element
                Vector3 position = startPosition + new Vector3(column * cellSize, row * cellSize, 0);

                // Instantiate the grid element at the calculated position
                Invader invader = Instantiate(gridElementPrefab[row], transform);

                invader.killed += InvaderKilled;

                invader.transform.localPosition = position;
            }
        }
    }

    private void Update()
    {
        //Move the Invaders
        transform.position += speedMovement.Evaluate(PercentKilled) * Time.deltaTime * moveDirection * speedMultiplier;

        //Get the right and left edges of the camera view
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

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
        //Reverse the direction of the movement and move the Invaders one row down
        moveDirection.x *= -1f;

        Vector3 position = transform.position;
        position.y -= 1f;

        transform.position = position;
    }

    private void MissileAttack()
    {
        //The probability of instantiating a missil - fewer live Invaders more probability
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Random.value < (1.0f / amountAlive))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                //Prevents only one missile from being instantiated at a time
                break;
            }
        }
    }

    private void InvaderKilled()
    {
        amountKilled++;
    }

}
