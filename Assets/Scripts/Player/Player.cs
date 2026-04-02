using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

[SelectionBase]
public class Player : MonoBehaviour
{
    private Vector2 _lastMoveDirection = Vector2.down;
    public Vector2 LastMoveDirection => _lastMoveDirection;
    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnFlashBlink;
    public event EventHandler OnChangeHp;
    [SerializeField] private Player_Interact player_Interact;
    [SerializeField] private AnimatorWaponEvent player_WaponEvent;
    [SerializeField] private HeroSO heroSO;     //Hero SO
    private float _heroBaseSpeed;               //Hero SO
    private float _heroCurrentSpeed;
    private float _maxHealth;                     //Hero SO
    public float MaxHealth => _maxHealth;
    [SerializeField] private float _currentHealth;  
    public float CurrentHealth => _currentHealth;
    private float _dashMultiplier;                //Hero SO
    private float _jerkTimer;                       //Hero SO
    [Header("Windows")]
    [Space(height:20)]
    [SerializeField] private float jerkCoolDownTime = 5f;
    [SerializeField] private KnockBack _knockBack;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private DamagePoolUi damagePoolUi;
    private Vector2 inputVector;
    private Rigidbody2D _rb;
    private bool _canTakeDamage;
    private bool _isAlive;
    private bool _isDashing;
    private bool _isJerking;


    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
        player_Interact = GetComponent<Player_Interact>();
    }
    private void Start()
    {
        _maxHealth = heroSO.maxHealth;
        _dashMultiplier = heroSO.heroDashMultiplier;
        _heroBaseSpeed = heroSO.heroBaseSpeed;
        _heroCurrentSpeed = _heroBaseSpeed;
        _jerkTimer = heroSO.herpJerkTimer;
        _currentHealth = _maxHealth;
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        GameInput.Instance.OnPlayerDash += GameInput_OnPlayerDash;
        GameInput.Instance.OnPlayerDashCanceled += GameInput_OnPlayerDashCanceled;
        GameInput.Instance.OnPlayerJerk += GameInput_OnPlayerJerkStarted;
        GameInput.Instance.OnInteractionTouchPerfomed += GameInput_OnInteractionTouchPerfomed;
        GameInput.Instance.OnInteractionInventoryStarted += GameInput_OnInteractionInventoryStarted;

        _canTakeDamage = true;
        _isAlive = true;
        _isDashing = false;
        _isJerking = false;

        CheñkHealth();
    }
    private void GameInput_OnInteractionInventoryStarted(object sender, System.EventArgs e)
    {
        Inventory.Instance.InventoryOpenClose();
    }
    private void GameInput_OnInteractionTouchPerfomed(object sender, System.EventArgs e)
    {
        player_Interact.TryInteract();
    }
    private void GameInput_OnPlayerDash(object sender, System.EventArgs e)
    {
        if (_isDashing) { return; }
        _isDashing = true;
        _heroCurrentSpeed *= _dashMultiplier;
    }
    private void GameInput_OnPlayerDashCanceled(object sender, System.EventArgs e)
    {
        _isDashing = false;
        _heroCurrentSpeed = _heroBaseSpeed;
    }
    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        Tool active = ActiveWeapon.Instance.GetActiveWeapon();
        if (active != null & player_WaponEvent.CanAttack)
        {
            ActiveWeapon.Instance.GetActiveWeapon().Attack();
            ActiveWeapon.Instance.VisualAttack();
            
        }

    }
    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVector();
        if(inputVector.sqrMagnitude > 0.01f)
            { inputVector = inputVector.normalized;
            _lastMoveDirection = inputVector;
        }
    }
    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockedBack)
            return;
        HandleMovement();
    }  
    private void HandleMovement() 
    {
        _rb.MovePosition(_rb.position + inputVector * (_heroCurrentSpeed * Time.fixedDeltaTime));
    }
    public void TakeDamage(Transform damageSource, int damage)
    {
        if (_canTakeDamage && _isAlive)
        {
            _canTakeDamage = false;
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            Debug.Log($"ïîëó÷åíî óðîíà = {damage}; òåêóùåå çäîðîâüå = { _currentHealth}.");
            _knockBack.GetKnockedBack(damageSource);
            OnFlashBlink?.Invoke(this, EventArgs.Empty);
            damagePoolUi.ShowDamage(damage, transform, Color.red);
            StartCoroutine(DamageRecoveryRoutine());


        }
        CheñkHealth();
        DetectedDeath();
    }

        private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(heroSO.damageRecoveryTime);
        _canTakeDamage = true;
    }
    public void HpRecovery(ItemSO item)
    {
        _currentHealth += item.healingPower;
        CheñkHealth();

        DetectedDeath();
    }
    private void CheñkHealth()
    {
        
        if (_currentHealth > _maxHealth) { _currentHealth = _maxHealth; }
        OnChangeHp?.Invoke(this, EventArgs.Empty);
    }
    private void DetectedDeath()
    {
        if (_currentHealth <= 0 && _isAlive)
        {
            _isAlive= false;
            //_knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }
    private void GameInput_OnPlayerJerkStarted(object sender, System.EventArgs e)
    {
        if (_isJerking) { return; }
        StartCoroutine(CoruytineJerkStart());
    }
    IEnumerator CoruytineJerkStart()
    {
        _isJerking = true;
        trailRenderer.emitting = true ;

        _heroCurrentSpeed *= heroSO.heroJerkMultiplier;
        yield return new WaitForSeconds(_jerkTimer);
        trailRenderer.emitting = false;
        _heroCurrentSpeed = _heroBaseSpeed;
        yield return new WaitForSeconds(jerkCoolDownTime);
        _isJerking = false;

    }
    private void OnDisable()
    {
        GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
        GameInput.Instance.OnPlayerDash -= GameInput_OnPlayerDash;
        GameInput.Instance.OnPlayerDashCanceled -= GameInput_OnPlayerDashCanceled;
    }
    public bool IsAlive() => _isAlive; 
}