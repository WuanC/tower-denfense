using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
     private Animator animator;
    [SerializeField] private Tower tower;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        tower.OnTowerAttack += Tower_OnTowerAttack;
    }

    private void Tower_OnTowerAttack()
    {
        animator.SetTrigger("Ready");
    }

    private void SpawnProjectile()
    {
       StartCoroutine(tower.Fire());
    }
}
