using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputComponent : MonoBehaviour
{
    public UnityEvent OnLeftMouseClick = new UnityEvent();
    public UnityEvent OnRightMouseClick = new UnityEvent();


    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (null != OnLeftMouseClick)
            {
                OnLeftMouseClick.Invoke();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (null != OnRightMouseClick)
            {
                OnRightMouseClick.Invoke();
            }
        }
    }
}
