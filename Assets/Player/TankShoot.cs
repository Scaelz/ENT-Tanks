using System.Collections;
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
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        reloadTimer += Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            if (reloadTimer >= reloadSpeed)
            {
                Shoot();
                reloadTimer = 0;
            }
        }
    }

    void Shoot()
    {
        GameObject go = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        Debug.Log(muzzle.rotation);
        TankProjectile projectile = go.GetComponent<TankProjectile>();
        projectile.SetSpeed(shootPower);
        Recoil();
    }

    void Recoil()
    {
        rb.AddForce(-transform.forward * recoilPower);
    }
}
