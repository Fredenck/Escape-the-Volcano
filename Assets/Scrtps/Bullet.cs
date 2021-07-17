using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool HitsPlayers = true;
    public bool HitsEnemies = true;
    public int DamageAmount = 1;
    public float TimeToLive = 5f;


    private float m_deathTimer = 0f;
    void Update()
    {
        m_deathTimer += Time.deltaTime;
        if (TimeToLive < m_deathTimer)
        {
            Destroy(this.gameObject, 0f);
        }
    }

    void OnTriggerEnter(Collider p_hitCollider)
    {
        string l_tag = p_hitCollider.gameObject.tag;
        HealthComponent l_health = null;

        Debug.Log("HIT");

        if (l_tag == GameConstants.PLAYER_TAG && HitsPlayers)
        {
            l_health = p_hitCollider.gameObject.GetComponent<HealthComponent>();
            if (null != l_health)
            {
                l_health.TakeDamage(DamageAmount);
            }
        }
        else if (l_tag == GameConstants.ENEMY_TAG && HitsEnemies)
        {
            l_health = p_hitCollider.gameObject.GetComponent<HealthComponent>();
            if (null != l_health)
            {
                l_health.TakeDamage(DamageAmount);
            }
        }

        Destroy(this.gameObject, 0f);
    }
}
