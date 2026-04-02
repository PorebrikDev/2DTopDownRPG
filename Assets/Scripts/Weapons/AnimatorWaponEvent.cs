using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorWaponEvent : MonoBehaviour
{

    private bool isAttacking = false;

    public bool CanAttack => !isAttacking;

    public void AnimationStarted()
    {
        isAttacking = true;
    }
    public void Attack_Hero_Start()
    {
        ActiveWeapon.Instance.GetActiveWeapon().BoxOn();
    }
    public void Attack_Hero_End()
    {
        ActiveWeapon.Instance.GetActiveWeapon().BoxOff();
        isAttacking = false;
    }
}
