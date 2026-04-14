using UnityEngine;

public class Background : MonoBehaviour
{
    public float moveSpeed = 3f;

    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < -10)
        {
            transform.position += new Vector3(0, 20f, 0);
        }
    }
}