using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10f;
    public int damage = 50;

    private Vector3 direction;

    public void AdjustDirection(Vector3 dir)
    {
        direction = dir;
        direction.y = 0;
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
