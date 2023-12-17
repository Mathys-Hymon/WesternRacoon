using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [Header("SHOOT INFOS")]
    [SerializeField] private float shootFrequency;
    [SerializeField] private float bulletSpeed;

    [Header("BULLET REFERENCE")]
    [SerializeField] public GameObject bulletPrefab;


    void Start()
    {
        ShootBullet();
    }
    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (transform.right * 2), transform.rotation);
        bullet.GetComponent<CannonBulletScript>().SetBulletSpeed(bulletSpeed);
        Invoke("ShootBullet", 10f / shootFrequency);
    }
}
