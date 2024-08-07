using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MysteryShip : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float cycleTime = 30f;
    public int Score { get; } = 300;

    private Vector2 leftDestination;
    private Vector2 rightDestination;
    private int direction = -1;
    private bool spawned;
    // private bool gameOver;

    public Action<bool> OnSpawn;

    private void Start()
    {
        // Transform the viewport to world coordinates so we can set the mystery
        // ship's destination points
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // Offset each destination by 1 unit so the ship is fully out of sight
        leftDestination = new Vector2(leftEdge.x - 1f, transform.position.y);
        rightDestination = new Vector2(rightEdge.x + 1f, transform.position.y);
        
        Despawn();
    }

    private void Update()
    {
        if (!spawned)
        {
            return;
        }

        if (direction == 1)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }

    }

    private void MoveRight()
    {
        transform.position += speed * Time.deltaTime * Vector3.right;

        if (transform.position.x >= rightDestination.x)
        {
            Despawn();
        }
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= leftDestination.x)
        {
            Despawn();
        }
    }

    private void Spawn()
    {
        direction *= -1;

        if (direction == 1)
        {
            transform.position = leftDestination;
        }
        else
        {
            transform.position = rightDestination;
        }

        spawned = true;

        //Invoke the spawn sound
        OnSpawn?.Invoke(spawned);

    }

    public void Despawn()
    {
        spawned = false;
        //Invoke the spawn sound
        OnSpawn?.Invoke(spawned);

        if (direction == 1)
        {
            transform.position = rightDestination;
        }
        else
        {
            transform.position = leftDestination;
        }

        // As long the Player has not lost the game invokes the mystery ship
        if (!GameManager.Instance.GetGameOverValue())
        {
            Invoke(nameof(Spawn), cycleTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            Despawn();
            GameManager.Instance.OnMysteryShipKilled(this);
        }
    }
}
