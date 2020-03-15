using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShoot : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] float shootPower;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject go = Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
        TankProjectile projectile = go.GetComponent<TankProjectile>();
        projectile.SetSpeed(shootPower);
    }
}
