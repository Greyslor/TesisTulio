using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public bool isTargetFish = false;
    public float speed = 2f;

    [Tooltip("límites horizontales del movimiento")]
    public float leftLimit = -8f;
    public float rightLimit = 8f;

    private int direction = -1; // -1 = izquierda, 1 = derecha
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Movimiento horizontal constante
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        //Rebote
        if (transform.position.x < leftLimit)
        {
            direction = 1;
            FlipSprite();
        }
        else if (transform.position.x > rightLimit)
        {
            direction = -1;
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = (direction == 1); //voltea hacia el movimiento
    }
}
