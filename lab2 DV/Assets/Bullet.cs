using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    [HideInInspector] public ShootingManager shooting;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponentInParent<EnemyHealth1>())
            {
                EnemyHealth1 enemyHealth1 = collision.gameObject.GetComponentInParent<EnemyHealth1>();
                enemyHealth1.TakeDamage(shooting.damage);
            }

            if (collision.gameObject.GetComponentInParent<EnemyHealth2>())
            {
                EnemyHealth2 enemyHealth2 = collision.gameObject.GetComponentInParent<EnemyHealth2>();
                enemyHealth2.TakeDamage(shooting.damage);
            }

            if (collision.gameObject.GetComponentInParent<EnemyHealth3>())
            {
                EnemyHealth3 enemyHealth3 = collision.gameObject.GetComponentInParent<EnemyHealth3>();
                enemyHealth3.TakeDamage(shooting.damage);
            }
        }

        Destroy(this.gameObject);
    }
}
