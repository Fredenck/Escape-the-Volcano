using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerEvent : MonoBehaviour
{
    public UnityEvent OnEnterTrigger = new UnityEvent();
    public UnityEvent OnExitTrigger = new UnityEvent();
    public List<string> CollisionTags = new List<string>();


    void OnTriggerEnter(Collider p_collider)
    {
        foreach (var l_tag in CollisionTags)
        {
            if (p_collider.gameObject.tag == l_tag)
            {
                if (null != OnEnterTrigger)
                {
                    OnEnterTrigger.Invoke();
                }
            }
        }
    }

    void OnTriggerExit(Collider p_collider)
    {
        foreach (var l_tag in CollisionTags)
        {
            if (p_collider.gameObject.tag == l_tag)
            {
                if (null != OnExitTrigger)
                {
                    OnExitTrigger.Invoke();
                }
            }
        }
    }
}
