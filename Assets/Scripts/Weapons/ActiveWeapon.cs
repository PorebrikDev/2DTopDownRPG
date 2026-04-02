using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }
    public ToolType CurrentToolType => currentToolType;
    [SerializeField] private Tool tool = null;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private ToolType currentToolType;

    [SerializeField] private Tool SwordTool;
    [SerializeField] private Tool PickaxeTool;
    [SerializeField] private Tool AxeTool;
    [SerializeField] private Tool ZeroTool;


    public void Awake()
    {
        Instance = this;
        currentToolType = ToolType.Zero;

    }
    private void Update()
    {
        Vector2 move = GameInput.Instance.GetMovementVector();
        UpdateAnimator(weaponAnimator, move);
        playerVisual.UpdateWeaponAnimator(move);
    }

    public Tool GetActiveWeapon()
    {
        return tool;
    }
    public void SetActiveWeapon(int index)
    {
        switch (index)
        {
            case 0:
                tool = SwordTool;    
                currentToolType = ToolType.Sword;
                break;
            case 1: 
                tool = PickaxeTool;   
                currentToolType = ToolType.Pickaxe;
                break;
            case 2: 
                tool = AxeTool;     
                currentToolType = ToolType.Axe;
                break;
            case 3:
                tool = ZeroTool;
                currentToolType = ToolType.Zero;
                break;
        }


    }


    void UpdateAnimator(Animator anim, Vector2 move)
    {
        anim.SetFloat("Speed", move.sqrMagnitude);
        if (move.sqrMagnitude > 0.01f)
        {
            anim.SetFloat("MoveX", move.x);
            anim.SetFloat("MoveY", move.y);
        }
    }
    public void VisualAttack()
    {
       
      
            weaponAnimator.SetInteger("ToolType", (int)tool.Type);
            weaponAnimator.SetTrigger("Attack");

            Animator bodyanimator = playerVisual.GetAnimator();
            bodyanimator.SetInteger("ToolType", (int)tool.Type);
            bodyanimator.SetTrigger("Attack");
      

    }
}
