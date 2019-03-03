using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10;
    public float moveSpeed = 10f;
    public int health = 100;
    public int point = 10;

    private Transform player;
    private bool isCollidingWithPlayer = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isCollidingWithPlayer)
        {
            Move();
        }
        transform.LookAt(player.position);
    }

    void Move()
    {
        Vector3 distance = (player.position - transform.position);

        distance.Normalize();
        distance.y = 0;

        transform.Translate(distance * Time.deltaTime * moveSpeed, Space.World);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        EnemySpawner.enemies.Remove(gameObject);
        GameManager.instance.UpdateScore(point);

        GameObject pt = (Instantiate(GameManager.instance.plusTenPrefab, transform.position, Quaternion.identity));
        Destroy(pt, 2f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            isCollidingWithPlayer = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;
        }
    }
}
