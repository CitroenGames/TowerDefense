using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float m_maxHealth = 100f;

    public float m_Health;

    private void Start()
    {
        m_Health = m_maxHealth;
    }

    public void TakeDamage(float damage)
    {
        m_Health -= damage;

        if (m_Health <= 0)
        {
            EnemySpawner.OnEnemyKilled.Invoke();
            Destroy(gameObject);
        }
    }
}
