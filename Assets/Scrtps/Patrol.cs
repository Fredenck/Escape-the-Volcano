// Patrol.cs
using UnityEngine;
using System.Collections;


public class Patrol : MonoBehaviour
{

    public Transform[] wayPoints;
    public bool IsAggroed = false;
    public Gun Weapon = null;
    public float AggroRadius = 30f;



    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();

        m_playerTransform = GameObject.FindWithTag("Player").transform;
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (wayPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = wayPoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % wayPoints.Length;
    }


    void Update()
    {
        if (null == m_playerTransform)
            return; // bail if there is no player

        if (IsAggroed)
        {
            agent.destination = m_playerTransform.position;

            if (null != Weapon)
            {
                Weapon.Fire();
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();

        }

        if (m_checkForPlayerTimer < 0)
        {
            m_checkForPlayerTimer = k_DISTANCE_CHECK_WAIT_TIME;
            PlayerDistanceCheck();
        }
        else
        {
            m_checkForPlayerTimer -= Time.deltaTime;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WayPoint")
        {
            GotoNextPoint();
            print("Next Stop");
        }
    }


    // void OnTriggerExit(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         //aggro = false;
    //         StartCoroutine(LosePlayer());
    //     }
    // }

    IEnumerator LosePlayer()
    {
        yield return new WaitForSeconds(5);
        print(Time.time);
        IsAggroed = false;
        GotoNextPoint();
    }


    private void PlayerDistanceCheck()
    {
        if (Vector3.Distance(this.transform.position, m_playerTransform.position) < AggroRadius)
        {
            Debug.Log("AGGROOOOOO");
            IsAggroed = true;
        }
        else
        {
            IsAggroed = false;
        }
    }


    private float m_checkForPlayerTimer;
    private const float k_DISTANCE_CHECK_WAIT_TIME = 0.5f;

    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform m_playerTransform;


}