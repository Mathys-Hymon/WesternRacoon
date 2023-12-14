using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyScript : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == 6)
            {
                PlayerMovement.Instance.Die();
            }
            if (collision.gameObject.layer != 9)
            {
                Destroy(gameObject);
            }
        }
    }
}
