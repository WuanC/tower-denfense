using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    private Animator anim;
    public event Action OnReadyAnimation;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnReadyAnim()
    {
        OnReadyAnimation?.Invoke();
        anim.ResetTrigger("Attack");
    }
    public void PlayAttack()
    {
        anim.SetTrigger("Attack");
    }

}
