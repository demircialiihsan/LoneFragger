using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public AudioSource shootClip;

    public Transform worldPointCursor;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float fireRate = .5f;
    public float charge = 1f;

    public int bulletCapacity = 10;
    public float overheatDuration = 3;
    public GameObject overheatImage;

    private float overheat = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Shoot());
        }
        else if (Input.GetMouseButtonUp(0) && overheat <= 0)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator Shoot()
    {
        while (overheat > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(charge);

        int firedBulletAmount = 0;

        while (Input.GetMouseButton(0))
        {
            FireBullet();
            firedBulletAmount++;

            if (firedBulletAmount >= bulletCapacity)
            {
                StartCoroutine(Overheat());
                break;
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    IEnumerator Overheat()
    {
        overheatImage.SetActive(true);

        overheat = overheatDuration;

        while (overheat > 0)
        {
            overheat -= Time.deltaTime;
            yield return null;
        }

        overheatImage.SetActive(false);
    }

    void FireBullet()
    {
        shootClip.Play();

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().AdjustDirection(shootingPoint.forward);

        Destroy(bullet, 5f);
    }
}
