using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 0;
    [SerializeField] private float shootFrequency = 0;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private float MaxPos = 0;
    [SerializeField] private float bulletFlyDirection;

    void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    
    void Update()
    {
        if (bulletInstance != null && bulletInstance.transform.position.x >= MaxPos)
        {
            Destroy(bulletInstance);
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / shootFrequency);

            PrepareBullet();
        }
    }
    private GameObject bulletInstance;

    private void PrepareBullet()
    {
        bulletPrefab = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bulletPrefab.GetComponent<Rigidbody2D>();

        Vector2 bulletDirection = new Vector2(Mathf.Cos(bulletFlyDirection * Mathf.Deg2Rad), Mathf.Sin(bulletFlyDirection * Mathf.Deg2Rad));
        bulletRb.velocity = bulletDirection * shootSpeed;
    }
}
