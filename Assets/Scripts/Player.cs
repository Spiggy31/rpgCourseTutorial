using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed; // how fast we move

    private Vector2 facingDirection; // direction we're facing

    [Header("Sprites")]
    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    // components
    private Rigidbody2D rig;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // get the component
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // get the horizontal and vertical inputs
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 velocity = new Vector2(x, y);

        if (velocity.magnitude != 0)
        {
            facingDirection = velocity;
        }

        UpdateSpriteDirection();

        rig.velocity = velocity * moveSpeed;
    }

    private void UpdateSpriteDirection()
    {
        if (facingDirection == Vector2.up)
            spriteRenderer.sprite = upSprite;
        else if (facingDirection == Vector2.down)
            spriteRenderer.sprite = downSprite;
        else if (facingDirection == Vector2.left)
            spriteRenderer.sprite = leftSprite;
        else if (facingDirection == Vector2.right)
            spriteRenderer.sprite = rightSprite;
    }
}
