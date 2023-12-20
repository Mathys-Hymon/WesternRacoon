using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [Header("SHOOT INFOS")]
    [SerializeField] private float shootFrequency;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool startShooting;
    [SerializeField] private float bulletSpawnOffset = 1f;

    [Header("BULLET REFERENCE")]
    [SerializeField] public GameObject bulletPrefab;


    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if(startShooting)
        {
            ShootBullet();
        }
        else
        {
            Invoke("BulletAnticipation", 8f / shootFrequency);
        }
        
    }
    private void BulletAnticipation()
    {
        sr.color = Color.red;
        Invoke("ShootBullet", 2f / shootFrequency);
    }
    private void ShootBullet()
    {
        sr.color = Color.clear;
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (transform.right * bulletSpawnOffset), transform.rotation);
        bullet.GetComponent<CannonBulletScript>().SetBulletSpeed(bulletSpeed);
        Invoke("ShootBullet", 10f / shootFrequency);
    }
}
