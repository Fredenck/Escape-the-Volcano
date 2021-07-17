using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * GUN
 * Fires a bullet in the forward direction of this transform
 *
 *
 */

public class Gun : MonoBehaviour
{
    public float FireRatePerMinute = 60f;

    [Header("Spawnable Object")]
    public GameObject BulletPrefab = null;
    public float LaunchSpeed = 20f;


    void Start()
    {
        m_WaitTimer = new WaitForSeconds(FireRatePerMinute / 60.0f);
        m_readyToFile = true;
    }

    public void Fire()
    {
        if (m_readyToFile)
        {
            m_readyToFile = false;
            StartCoroutine(WaitForNextFire());
            Debug.Log("FIRE!");

            if (null != BulletPrefab)
            {
                GameObject l_bullet = GameObject.Instantiate(BulletPrefab, this.transform.position, this.transform.rotation);

                Rigidbody l_bulletRigidbody = l_bullet.GetComponent<Rigidbody>();
                l_bulletRigidbody.AddForce((this.transform.forward * LaunchSpeed), ForceMode.VelocityChange);
            }
        }
    }

    IEnumerator WaitForNextFire()
    {
        yield return m_WaitTimer;
        m_readyToFile = true;
    }

    private WaitForSeconds m_WaitTimer;
    private bool m_readyToFile = false;
}


