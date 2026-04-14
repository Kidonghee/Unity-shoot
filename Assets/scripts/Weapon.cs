using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;
    public float damage = 1;
    
    void Start()
    {
        Destroy(gameObject, 1F);
    }
    void Update()
    {   

        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
    
}
