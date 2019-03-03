using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public Sprite[] healthIndicators;
    public Image healthIndicator;

    public AudioSource deathClip;
    public AudioSource gruntClip;

    private bool safe = false;
    public float safetyDuration = 2f;

    public void UpdateHealth(int amount)
    {
        if (amount < 0)
        {
            gruntClip.Play();
        }

        health += amount;

        health = Mathf.Min(health, 100);

        if (health <= 0)
        {
            Die();
            healthIndicator.sprite = healthIndicators[3];
            return;
        }

        healthIndicator.sprite = healthIndicators[(health / 25) - 1];
    }

    void Die()
    {
        deathClip.Play();

        GetComponent<Animator>().SetTrigger("Die");

        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;

        StartCoroutine(GameManager.instance.EndGame());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealthPack"))
        {
            UpdateHealth(25);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!safe)
        {
            safe = true;
            UpdateHealth(-damage);

            StartCoroutine(Safety());
        }
    }

    IEnumerator Safety()
    {
        yield return new WaitForSeconds(safetyDuration);

        safe = false;
    }
}
