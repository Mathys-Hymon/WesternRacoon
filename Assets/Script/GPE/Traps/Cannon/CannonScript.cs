using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [Header("SHOOT INFOS")]
    [SerializeField] private float shootFrequency;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool startShooting;

    [Header("BULLET REFERENCE")]
    [SerializeField] public GameObject bulletPrefab;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSRC;

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
            Invoke("BulletAnticipation", 3f / shootFrequency);
        }
        
    }
    private void BulletAnticipation()
    {
        sr.color = Color.red;
        Invoke("ShootBullet", 2f / shootFrequency);
    }
    private void ShootBullet()
    {
        audioSRC.pitch = 1.0f;
        audioSRC.Play();
        sr.color = Color.white;
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (transform.right * 2), transform.rotation);
        bullet.GetComponent<CannonBulletScript>().SetBulletSpeed(bulletSpeed);
        Invoke("BulletAnticipation", 8f / shootFrequency);
    }
}
