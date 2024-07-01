using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public Proyectile laserPrefab;
    private bool laserActive;

    private void Update()
    {
        //Handle movement
        Vector3 horizontalMovement;
        float InputX = Input.GetAxis("Horizontal");
        horizontalMovement = new Vector3(InputX, 0, 0);

        transform.position += moveSpeed * Time.deltaTime * horizontalMovement;

        //Shooting...
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        if (!laserActive)
        {
            Proyectile proyectile = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            proyectile.destroyed += LaserDestroyed;
            
            laserActive = true;
        }


    }

    private void LaserDestroyed()
    {
        laserActive = false;
    }


}
