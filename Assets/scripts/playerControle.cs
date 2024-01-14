using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControle : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public Sprite walkLeftSprite;
    public Sprite walkRightSprite;
    public Sprite walkUpSprite;
    public Sprite walkDownSprite;
    [HideInInspector]
    public Vector2 movement;
    [HideInInspector]
    public float lastHorizontalVector;
    float lastVerticalVector;

    void Update()
    {
        // Get input for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set the appropriate sprite based on the movement direction
        UpdateSprite();
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateSprite()
    {
        if (movement.x != 0)
        {
            lastHorizontalVector = movement.x;
            if (movement.x > 0)
            {
                spriteRenderer.sprite = walkRightSprite;
                
            }
            else if (movement.x < 0)
            {
                spriteRenderer.sprite = walkLeftSprite;
                
            }
        }
        // Update sprite based on movement direction
        if (movement.y > 0)
        {
            spriteRenderer.sprite = walkUpSprite;
        }
        else if (movement.y < 0)
        {
            spriteRenderer.sprite = walkDownSprite;
        }
    }
}
