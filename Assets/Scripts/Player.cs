using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public Projectile laserPrefab;
    private Projectile laser;

    // private bool laserActive;

    private void Update()
    {
        Vector3 position = transform.position;

        // Clamp the position of the character so they do not go out of bounds
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);

        // Set the new position
        transform.position = position;

        //Handle movement
        Vector3 horizontalMovement;
        float InputX = Input.GetAxis("Horizontal");
        horizontalMovement = new Vector3(InputX, 0, 0);

        transform.position += moveSpeed * Time.deltaTime * horizontalMovement;

        // Only one laser can be active at a given time so first check that
        // there is not already an active laser
        if (laser == null && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") ||
            other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }



}
