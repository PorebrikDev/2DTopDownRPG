using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    [SerializeField] private Tool tool;
    [SerializeField] private HitBoxNew hitBox;

    private Animator animator;
    private const string ATTACK = "Attack";
    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }
    private void OnEnable()
    {
        tool.OnToolUse += Tool_OnToolUse;
    }
    private void Tool_OnToolUse(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }
    public void Update()
    {
        Vector2 move = GameInput.Instance.GetMovementVector();
        animator.SetFloat("Speed", move.sqrMagnitude);
        if (move.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("MoveX", move.x);
            animator.SetFloat("MoveY", move.y);
        }
    }
    //public void Attack_Hero_Start()
    //{ hitBox.AttackColiderTurnOn(); }
    //public void Attack_Hero_End() 
    //{ hitBox.AttackColiderTurnOff(); }

    private void OnDisable()
    {
        tool.OnToolUse -= Tool_OnToolUse;

    }
}





