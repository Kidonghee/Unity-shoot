using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    private float minY = -7f;

    void Start()
    {
        Jump();
    }

    void Jump()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();

        float randomJumpForce = Random.Range(4f, 8f);
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f, 2f);

        rigidbody2D.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.IncreaseCoin();
            Destroy(gameObject);
        }
    }
}