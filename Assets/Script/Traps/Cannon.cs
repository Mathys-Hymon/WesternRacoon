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
    [SerializeField] private Vector2 bulletFlyDirection = Vector2.right;

    void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    
    void Update()
    {
        //if (bulletInstance != null && bulletInstance.transform.position.x >= MaxPos)
        //{
        //    Destroy(bulletInstance);
        //}
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f / shootFrequency);

            PrepareBullet();
        }
    }
    private GameObject bulletInstance;

    private void PrepareBullet()
    {
        bulletInstance = Instantiate(bulletPrefab, transform.position + transform.up, transform.rotation);
        Rigidbody2D bulletRb = bulletPrefab.GetComponent<Rigidbody2D>();

        //Vector2 bulletDirection = bulletFlyDirection.normalized;
        //bulletRb.velocity = bulletDirection * shootSpeed;
    }
}
