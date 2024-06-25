using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update() {
        
        Vector3 horizontalMovement;
        float InputX = Input.GetAxis("Horizontal");
        horizontalMovement = new Vector3(InputX, 0, 0);

        transform.position += moveSpeed * Time.deltaTime * horizontalMovement;
    }
}
