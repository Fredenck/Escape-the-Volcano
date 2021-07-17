using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int DamageAmount = 1;
    public float ApplyDamageTimesPerSecond = 1f;


    void OnTriggerEnter(Collider p_hitCollider)
    {
        string l_tag = p_hitCollider.gameObject.tag;

        if (l_tag == GameConstants.PLAYER_TAG)
        {
            m_health = p_hitCollider.gameObject.GetComponent<HealthComponent>();
            m_DamageTimer = ApplyDamageTimesPerSecond;
        }
    }

    void OnTriggerExit(Collider p_hitCollider)
    {
        string l_tag = p_hitCollider.gameObject.tag;

        if (l_tag == GameConstants.PLAYER_TAG)
        {
            m_health = null;
        }
    }


    void Update()
    {
        m_DamageTimer += Time.deltaTime;

        if (m_DamageTimer > ApplyDamageTimesPerSecond)
        {
            m_DamageTimer = 0f;
            if (null != m_health)
            {
                m_health.TakeDamage(DamageAmount);
            }
        }
    }

    private float m_DamageTimer = 0f;
    private HealthComponent m_health = null;
}
