using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventSystemSelector : MonoBehaviour
{
    EventSystem _eventSystem;

    void Awake()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if (_eventSystem.currentSelectedGameObject == null || _eventSystem.currentSelectedGameObject.activeInHierarchy == false)
        {
            if (FindObjectOfType<Selectable>() != null)
                _eventSystem.SetSelectedGameObject(FindObjectOfType<Selectable>().gameObject);
            else
                return;
        }
    }
}
