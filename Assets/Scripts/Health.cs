using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float m_maxHealth = 100f;
    [SerializeField] private Slider slider;
    public float m_Health;

    private bool isDead = false;

    private void Start()
    {
        m_Health = m_maxHealth;
        slider.maxValue = m_maxHealth;
        slider.value = m_Health;
    }

    public void TakeDamage(float damage)
    {
        m_Health -= damage;
        slider.value = m_Health;

        if (m_Health <= 0 && !isDead)
        {
            EnemySpawner.OnEnemyKilled.Invoke();
            isDead = true;
            Destroy(gameObject);
        }
    }
}
