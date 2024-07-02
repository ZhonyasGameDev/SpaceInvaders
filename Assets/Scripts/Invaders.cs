using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    [Header("Grid")]
    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1.0f;         // Size of each cell in the grid

    [Header("Invaders")]
    public Invader[] gridElementPrefab = new Invader[5];  // The prefab for the grid elements
    public AnimationCurve speedMovement;

    private Vector3 moveDirection = Vector2.right;
    private Vector3 initialPosition;
    public float speedMultiplier = 2f;

    [Header("Missiles")]
    public Projectile missilePrefab;
    public float missileSpawnRate = 1f;

    // public int amountKilled { get; private set; }

    /*  // Read-only property that calculates the total number of invaders
     public int TotalInvaders => rows * columns;
     public float PercentKilled => (float)amountKilled / TotalInvaders;
     public int amountAlive => TotalInvaders - amountKilled; */

    private void Awake()
    {
        initialPosition = transform.position;

        CreateGrid();
    }

    void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileSpawnRate, missileSpawnRate);
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

                // invader.killed += InvaderKilled;

                invader.transform.localPosition = position;
            }
        }
    }

    private void Update()
    {
        //Calculate the percentage of invaders killed
        int totalCount = rows * columns;
        int amountAlive = GetAliveCount();
        int amountKilled = totalCount - amountAlive;
        float percentKilled = amountKilled / (float)totalCount;

        // Evaluate the speed of the invaders based on how many have been killed
        float speed = speedMovement.Evaluate(percentKilled);
        // transform.position += speed * Time.deltaTime * moveDirection;
        transform.position += speed * speedMultiplier * Time.deltaTime * moveDirection;

        // Transform the viewport to world coordinates so we can check when the invaders reach the edge of the screen
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        float offsetX = 1.0f;

        // The invaders will advance to the next row after reaching the edge of the screen
        foreach (Transform invader in this.transform)
        {
            // Skip any invaders that have been killed
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            //Check if the current invader is moving to the right or left AND if has reached the edge of the screen
            if (moveDirection == Vector3.right && invader.position.x >= (rightEdge.x - offsetX))
            {
                AdvanceRow();
                break;
            }
            else if (moveDirection == Vector3.left && invader.position.x <= (leftEdge.x + offsetX))
            {
                AdvanceRow();
                break;
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
        int amountAlive = GetAliveCount();

        // No missiles should spawn when no invaders are alive
        if (amountAlive == 0)
        {
            return;
        }

        foreach (Transform invader in transform)
        {
            // Any invaders that are killed cannot shoot missiles
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            //The probability of instantiating a missil - fewer live Invaders more probability
            if (Random.value < (1.0f / amountAlive))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                //Prevents only one missile from being instantiated at a time
                break;
            }
        }
    }

    //Called in GameManager
    public void ResetInvaders()
    {
        moveDirection = Vector3.right;
        transform.position = initialPosition;

        foreach (Transform invader in transform)
        {
            invader.gameObject.SetActive(true);
        }
    }

    public int GetAliveCount()
    {
        int count = 0;

        foreach (Transform invader in transform)
        {
            if (invader.gameObject.activeSelf)
            {
                count++;
            }
        }

        return count;
    }

    /*  private void InvaderKilled()
     {
         amountKilled++;
     } */

}
