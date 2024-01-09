using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyHealth1 : MonoBehaviour
{
    [SerializeField] private float health = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if(health > 0)
        {
            health -= damage;

            if (health <= 0)
            {
                EnemyDeath();
            }

            UnityEngine.Debug.Log("Hit");
        }
    }

    void EnemyDeath()
    {
        UnityEngine.Debug.Log("Death");
        Destroy(this.gameObject);
    }
}
