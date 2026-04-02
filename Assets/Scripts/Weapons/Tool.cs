using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tool : MonoBehaviour
{
    public event EventHandler OnToolUse;

    [SerializeField] private ToolType toolType;
    [SerializeField] private int damageAmount = 1;
    public ToolType Type => toolType;
    public int DamageAmount => damageAmount;

    private HitBoxNew _hitBox;

    private void Awake()
    {
        _hitBox = GetComponentInChildren<HitBoxNew>();
    }
    public void BoxOn()
    {
        _hitBox.HitBoxOn();
    }
    public void BoxOff()
    {
        _hitBox.HitBoxOff();
    }


    public void Attack()
    {
        
        OnToolUse?.Invoke(this, EventArgs.Empty);
    }
}
