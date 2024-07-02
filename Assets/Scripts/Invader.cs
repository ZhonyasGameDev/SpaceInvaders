using System;
using UnityEngine;

public class Invader : MonoBehaviour
{
    // public Action killed;
    public Sprite[] animationSprites;
    public int score = 10;
    public float animationTime;
    private int animationFrame;
    private SpriteRenderer spriteRenderer;
    private const string PROYECTILE_LAYER_NAME = "Laser";

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        animationFrame++;

        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = this.animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //When a projectile contacts an invader 
        if (other.gameObject.layer == LayerMask.NameToLayer(PROYECTILE_LAYER_NAME))
        {
            // killed?.Invoke();
            // gameObject.SetActive(false);
            GameManager.Instance.OnInvaderKilled(this);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            GameManager.Instance.OnBoundaryReached();
        }
    }

}
