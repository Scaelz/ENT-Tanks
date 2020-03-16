﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankShoot : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] float shootPower;
    [SerializeField] float recoilPower;
    [SerializeField] float reloadSpeed;
    float reloadTimer;
    bool canShoot = true;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!canShoot)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadSpeed)
            {
                canShoot = !canShoot;
                reloadTimer = 0;
            }
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            GameObject go = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            TankProjectile projectile = go.GetComponent<TankProjectile>();
            projectile.SetSpeed(shootPower);
            Recoil();
            canShoot = !canShoot;
        }
    }

    void Recoil()
    {
        rb.AddForce(-transform.forward * recoilPower);
    }
}
