using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float halfWidth = 0.5f;

    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private int weaponIndex = 0;

    [SerializeField]
    private float shootInterval = 0.1f;

    public float lastShotTime = 0f;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float minX = -5f + halfWidth;
        float maxX = 5f - halfWidth;

        float toX = Mathf.Clamp(mousePos.x, minX, maxX);

        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if (GameManager.instance.isGameOver == false)
        {
            Shoot();
        }

        
    }

    void Shoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Coin")
        {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }
    

    public void Upgrade()
    {
        weaponIndex += 1;

        if (weaponIndex >= weapons.Length)
        {
            weaponIndex = weapons.Length - 1;
        }
    }
}