using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


/**
 * HEALTH COMPONENT
 * Gives an GameObject health
 *
 */
public class HealthComponent : MonoBehaviour
{
    [Header("Game Rules")]
    public int CurrentHealth = 50;
    public int MaximumHealth = 50;
    public bool DestroyOnDeath = false;

    [Header("UI Elements")]

    public Text UI_HealthText;
    public Slider UI_HealthBar;

    [Header("Game Events")]
    public UnityEvent OnDeath = new UnityEvent();
    public UnityEvent OnTakeDamage = new UnityEvent();


    void Start()
    {
        CurrentHealth = MaximumHealth;
        UpdateUI();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p_amount">Amount of damage, always positive</param>
    public void TakeDamage(int p_amount)
    {
        if (p_amount > 0)
        {
            CurrentHealth -= p_amount;
            CurrentHealth = (CurrentHealth < 0) ? 0 : CurrentHealth;

            Debug.LogFormat("DAMAGE TAKEN: {0} REMAINING: {1}/{2}", p_amount, CurrentHealth, MaximumHealth);

            if (null != OnTakeDamage)
                OnTakeDamage.Invoke();
        }
        UpdateUI();
        CheckForDeath();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="p_amount">Amount of healing, always positive</param>
    public void ReceiveHealing(int p_amount)
    {
        if (p_amount > 0)
        {
            CurrentHealth += p_amount;
            CurrentHealth = (CurrentHealth > MaximumHealth) ? MaximumHealth : CurrentHealth;
        }
        UpdateUI();
        CheckForDeath();
    }


    public void Kill()
    {
        CurrentHealth = 0;

        UpdateUI();
        CheckForDeath();
    }


    private void CheckForDeath()
    {
        if (CurrentHealth <= 0)
        {
            if (null != OnDeath)
            {
                OnDeath.Invoke();
            }

            if (DestroyOnDeath)
            {
                Destroy(this.gameObject, 0f);
            }
        }
    }

    private void UpdateUI()
    {
        if (null != UI_HealthText)
        {
            UI_HealthText.text = string.Format("{0}", CurrentHealth);
            //UI_HealthText.text = string.Format("{0} / {1}", CurrentHealth, MaximumHealth);
        }
        if (null != UI_HealthBar)
        {
            UI_HealthBar.value = ((float)CurrentHealth) / ((float)MaximumHealth);
        }
    }

}
