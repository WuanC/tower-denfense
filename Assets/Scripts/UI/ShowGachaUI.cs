using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGachaUI : MonoBehaviour
{
    public event Action OnShowNext;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            OnShowNext?.Invoke();
        }
    }
}
