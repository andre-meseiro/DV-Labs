using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletVelocity;
    [HideInInspector] public float damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject currentBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.shooting = this;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(bulletSpawnPoint.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
