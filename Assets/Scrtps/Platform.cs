
using UnityEngine;

/**
 * MOVING PLATFORM
 * 
 * Add to the platform GameObject that: 
 *  1.) Moves!
 *  2.) Has a Collider that is not marked as a Trigger
 *
 *  This script will not work if:
 *  - there's no collider as a component of the gameObject
 *  - this gameObject doesn't change it's position.
 *  - the player's Tag is changed/edited so the string doesn't match
 */
public class Platform : MonoBehaviour
{

    void Start()
    {
        m_prevPosition = this.transform.position;
        m_currentPosition = Vector3.zero;
        m_velocity = Vector3.zero;
        m_playerRigidbody = null;
        m_isTouchingPlatform = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            m_playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (null != m_playerRigidbody)
            {
                m_isTouchingPlatform = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            m_isTouchingPlatform = false;
            m_playerRigidbody = null;
        }
    }

    void FixedUpdate()
    {
        m_currentPosition = this.transform.position;
        m_velocity = (m_currentPosition - m_prevPosition) / Time.fixedDeltaTime;
        m_prevPosition = m_currentPosition;

        if (m_isTouchingPlatform)
        {
            m_playerRigidbody.AddForce(new Vector3(m_velocity.x, 0, m_velocity.z), ForceMode.VelocityChange);
        }
    }


    private Vector3 m_prevPosition;
    private Vector3 m_currentPosition;
    private Vector3 m_velocity;
    private Rigidbody m_playerRigidbody;
    private bool m_isTouchingPlatform = false;
}
