using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour, IKillable
{
    public GameObject GO => this.gameObject;
    [SerializeField] float health;
    public float Health => health;
    [SerializeField] float armor;
    public float Armor => armor;
    public event Action OnGotKilled;
    public event Action OnSpawnBonus;
    public event Action<float> OnGotHit;
    public event Action<bool> OnShieldStateChanged;

    public void SetArmor(float value) { armor = value; }
    public void SetHealth(float value) { health = value; }

    private void Start()
    {
        FindObjectOfType<SpawnBonus>().OnAddTankHealth(this);
    }

    private void Update()
    {
        
    }

    public void Damage(float value)
    {
        OnGotHit?.Invoke(value);
        if (IsShielded())
        {
            value = DamageShield(value);
        }
        if (!IsShielded())
        {
            OnShieldStateChanged?.Invoke(false);
        }
        health -= value;
        if(health <= 0)
        {
            KillThis();
        }
    }

    float DamageShield(float value)
    {
        float valueDifference = value - armor;
        armor -= value;
        if (armor < 0)
        {
            armor = 0;
        }
        return valueDifference < 0 ? 0 : valueDifference;
    }

    bool IsShielded()
    {
        return armor > 0;
    }

    public void KillThis()
    {
        OnGotKilled?.Invoke();
        if (gameObject.GetComponent<EnemyMovement>() != null)
        {
            OnSpawnBonus?.Invoke();
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();
        if(projectile != null)
        {
            Damage(projectile.Power);
        }
    }

}
