using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


[DefaultExecutionOrder(-100)]
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions _playerInputActions;

    public event EventHandler OnPlayerAttack;
    public event EventHandler OnPlayerDash;
    public event EventHandler OnPlayerDashCanceled;
    public event EventHandler OnPlayerJerk;
    public event EventHandler OnInteractionTouchPerfomed;
    public event EventHandler OnInteractionInventoryStarted;
    public event EventHandler OnInteractionMenuStarted;
    public event EventHandler OnInteractionControlF1Started;

    public event EventHandler<int> OnNumberKeyStarted;




    private bool _isPaused = false;

    private void Awake()
    {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        _playerInputActions.Enable();
        _playerInputActions.Combat.Attack.started += PlayerAttack_started;
        _playerInputActions.Player.Dash.performed += PlayerDash_perfomed;
        _playerInputActions.Player.Dash.canceled += PlayerDash_canceled;
        _playerInputActions.Player.Jerk.started += PlayerJerk_started;
        _playerInputActions.Interaction.Touch.performed += InteractionTouch_perfomed;
        _playerInputActions.Interaction.Inventory.started += Interaction_InventoteStrated;
        _playerInputActions.Interaction.Menu.started += Interaction_Menu;
        _playerInputActions.Interaction.ControlF1.started += ControlF1_started;

        _playerInputActions.Interaction._1.started += ctx => OnAnyNumberStarted(ctx, 1);
        _playerInputActions.Interaction._2.started += ctx => OnAnyNumberStarted(ctx, 2);
        _playerInputActions.Interaction._3.started += ctx => OnAnyNumberStarted(ctx, 3);
        _playerInputActions.Interaction._4.started += ctx => OnAnyNumberStarted(ctx, 4);
        _playerInputActions.Interaction._5.started += ctx => OnAnyNumberStarted(ctx, 5);
        _playerInputActions.Interaction._6.started += ctx => OnAnyNumberStarted(ctx, 6);
        _playerInputActions.Interaction._7.started += ctx => OnAnyNumberStarted(ctx, 7);
        _playerInputActions.Interaction._8.started += ctx => OnAnyNumberStarted(ctx, 8);
        _playerInputActions.Interaction._9.started += ctx => OnAnyNumberStarted(ctx, 9);
        _playerInputActions.Interaction._0.started += ctx => OnAnyNumberStarted(ctx, 0);
    }

    private bool IsPointerOverBlockingUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var hit in results)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("UI_BlockInput"))
            {
                return true;
            }
        }

        return false;
}
    private void OnAnyNumberStarted(InputAction.CallbackContext ctx, int number)
    {
        OnNumberKeyStarted?.Invoke(this, number);
    }

    public bool IsPaused
    {
        get { return _isPaused; }
        set
        {
            _isPaused = value;
            if (_isPaused)
            {
                Time.timeScale = 0;
                _playerInputActions.Player.Disable();
                _playerInputActions.Combat.Disable();
            }
            else
            {
                Time.timeScale = 1;
                _playerInputActions.Player.Enable();
                _playerInputActions.Combat.Enable();
            }
        }
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
    public void EnableCombat() { _playerInputActions.Combat.Enable(); }

    public void DisableCombat() { _playerInputActions.Combat.Disable(); }

    private void Interaction_Menu(InputAction.CallbackContext obj)
    {
        if (Inventory.Instance.IsInventory == false)
        { 
            IsPaused = !IsPaused; }
        else { Inventory.Instance.InventoryOpenClose(); }
        OnInteractionMenuStarted?.Invoke(this, EventArgs.Empty);
    }

    
    private void ControlF1_started(InputAction.CallbackContext obj)
    {
        OnInteractionControlF1Started?.Invoke(this, EventArgs.Empty);
    }
    private void Interaction_InventoteStrated(InputAction.CallbackContext obj)
    {
        OnInteractionInventoryStarted?.Invoke(this, EventArgs.Empty);
    }
    private void InteractionTouch_perfomed(InputAction.CallbackContext obj)
    {
        OnInteractionTouchPerfomed?.Invoke(this, EventArgs.Empty);
    }
    private void PlayerDash_perfomed(InputAction.CallbackContext obj)
    {
        OnPlayerDash?.Invoke(this, EventArgs.Empty);
    }
    private void PlayerDash_canceled(InputAction.CallbackContext obj)
    {
        OnPlayerDashCanceled?.Invoke(this, EventArgs.Empty);
    }
  
    public void DisableMovement()
    {
        _playerInputActions.Disable();
    }
    private void PlayerAttack_started(InputAction.CallbackContext obj)
    {
        if (IsPointerOverBlockingUI()) return;

        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }
    private void PlayerJerk_started(InputAction.CallbackContext obj)
    {
        OnPlayerJerk.Invoke(this, EventArgs.Empty);
    }
    private void OnDisable()
    {
        _playerInputActions.Combat.Attack.started -= PlayerAttack_started;
        _playerInputActions.Combat.Attack.started -= PlayerDash_perfomed;
        _playerInputActions.Player.Dash.canceled -= PlayerDash_canceled;
        _playerInputActions.Player.Jerk.started -= PlayerJerk_started;
        _playerInputActions.Interaction.Touch.performed -= InteractionTouch_perfomed;
        _playerInputActions.Interaction.Inventory.started -= Interaction_InventoteStrated;
        _playerInputActions.Interaction.Menu.started -= Interaction_Menu;
        _playerInputActions.Disable();
    }
}
