using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ActiveWeapon activeWeapon;

    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }
 

    public Animator GetAnimator() { return animator; }



    public void UpdateWeaponAnimator(Vector2 move)
    {
        animator.SetFloat("Speed", move.sqrMagnitude);
        if (move.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("MoveX", move.x);
            animator.SetFloat("MoveY", move.y);
        }
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool("IsDie", true);
    }
    private void OnDisable()
    {
        Player.Instance.OnPlayerDeath -= Player_OnPlayerDeath;
    }
}
