using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.5f;
    public float bulletSpeed = 20f;     
    public Renderer playerRenderer;

    private float fireDelay = 0.5f;

    public Material[] colors;
    public BulletPool bulletPool;

    private int currentColorIndex = 0;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        if(colors != null)
        {
            playerRenderer.material = colors[currentColorIndex];
        }
    }


    private void Update()
    {
        fireDelay += Time.deltaTime;

        if (fireDelay >= fireRate)
        {
            ShootBullet();
            fireDelay = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            playerRenderer.material = colors[currentColorIndex];
        }
    }
        
    void ShootBullet()
    {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.SetActive(true);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        Renderer bulletRenderer = bullet.GetComponent<Renderer>();

        if( bulletRb != null )
        {
            Material bulletColor = colors[currentColorIndex];
            bulletRb.velocity = bulletSpawnPoint.right * bulletSpeed;
            bulletRenderer.material = bulletColor;
        }

        StartCoroutine(DeactivateBullet(bullet, 2f));
    }

    public Material GetCurrentColor()
    {
        return colors[currentColorIndex];
    }

    private System.Collections.IEnumerator DeactivateBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        bullet.SetActive(false);
    }
}
