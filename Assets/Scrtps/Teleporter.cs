
using UnityEngine;

/**
 * TELEPORTER
 * Teleport the player from this location to the destination
 * 
 * Requires:
 *  1.) A Destination GameObject (empty, with no components)
 *  2.) A Collider (preferably a box collider) with IsTrigger checked (set to true) on this gameObject (and ONLY This object).
 *
 * Troubleshooting:
 *  Q. Character gets stuck at destination / Character launches upward
 *  A. Make sure the destination is above the ground (and your character isn't clipping into a collider).
 */

public class Teleporter : MonoBehaviour
{
    [Tooltip("The Destination of the teleporter")]
    public GameObject Destination;

    void Start()
    {
        if (Destination == null)
        {
            Debug.LogError("Teleporter has no destination, and will not work.");
        }
        else
        {
            m_destTransform = Destination.transform;
        }


        Collider l_collider = GetComponent<Collider>();
        if (l_collider.isTrigger == false)
        {
            Debug.LogError("Collider is not marked as a Trigger, this script will not work.");
        }

        m_phase = TeleportationPhases.Idle;
    }


    void OnTriggerEnter(Collider p_collider)
    {
        if (m_destTransform == null)
            return;

        if (p_collider.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            m_playerRigidbody = p_collider.gameObject.GetComponent<Rigidbody>();

            m_playerRigidbody.angularVelocity = Vector3.zero;
            m_playerRigidbody.velocity = Vector3.zero;

            m_phase = TeleportationPhases.Move;
        }
    }

    void FixedUpdate()
    {
        switch (m_phase)
        {
            case TeleportationPhases.Idle:
                if (null != m_playerRigidbody)
                {
                    m_playerRigidbody = null;
                }
                break;

            case TeleportationPhases.Move:
                if (null != m_playerRigidbody)
                {
                    m_playerRigidbody.position = m_destTransform.position;
                    m_playerRigidbody.rotation = m_destTransform.rotation;
                    m_playerRigidbody.angularVelocity = Vector3.zero;
                    m_playerRigidbody.velocity = Vector3.zero;
                }
                m_phase = TeleportationPhases.Cleanup;
                break;

            case TeleportationPhases.Cleanup:
                if (null != m_playerRigidbody)
                {
                    if ((m_playerRigidbody.position - m_destTransform.position).magnitude < 0.1f)
                    {
                        m_phase = TeleportationPhases.Idle;
                    }
                    else
                    {
                        m_phase = TeleportationPhases.Move;
                    }
                }
                break;
        }

    }


    private Transform m_destTransform = null;
    private Rigidbody m_playerRigidbody;

    private enum TeleportationPhases
    {
        Idle,
        Move,
        Cleanup
    }

    private TeleportationPhases m_phase;


}
