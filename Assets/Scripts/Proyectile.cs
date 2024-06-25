using System;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [SerializeField] private float proyectileSpeed;
    [SerializeField] private Vector3 direction;

    public Action destroyed;

    private void Update()
    {
        transform.position += proyectileSpeed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        destroyed?.Invoke();
        Destroy(gameObject);
    }
}
