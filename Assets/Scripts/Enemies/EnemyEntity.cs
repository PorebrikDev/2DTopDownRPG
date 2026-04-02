using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private EnemyPool _enemyPool;

    public event EventHandler OnTakeHit;
    public event EventHandler OnEnemyDeath;

    private PolygonCollider2D _poligonCollider2D;
    private KnockBack _knockBack;
    private float _currentHealth;
    private bool _canTakeDamage;
    private bool _isAlive;
    private float _damageRecaveryRutineSo;
    private bool BoolNavMeshUsing;



    private void Awake()
    {
        _poligonCollider2D = GetComponent<PolygonCollider2D>();
        _knockBack = GetComponent<KnockBack>();
    }
    private void OnEnable()
    {
        _canTakeDamage = true;
        _isAlive = true;
        _currentHealth = _enemySO.enemyHealth;
        _damageRecaveryRutineSo = _enemySO.damageRecoveryTime;
    }
    public void Init(EnemyPool pool) { _enemyPool = pool; }
    public void PoligonColliderTurnOff()
    {
        _poligonCollider2D.enabled = false;
    }
    public void PoligonColliderTurnOn()
    {
        _poligonCollider2D.enabled = true;
    }


    public void TakeHit(Tool tool)
    {
        //if (tool.Type != ToolType.Sword)
        //    return;
        TakeDamage(tool.DamageAmount, tool.transform.root);

    }



    public void TakeDamage( int damage, Transform damageSource)
    {
        if (_canTakeDamage && _isAlive)
        {
            _canTakeDamage = false;
            _currentHealth -= damage;
            _knockBack.GetKnockedBack(damageSource);
            OnTakeHit?.Invoke(this, EventArgs.Empty);
            DetectDeath();
            Debug.Log("текущее хп" + _currentHealth + "урон нанесен" + damage);
            DamagePoolUi.instance.ShowDamage(damage, transform, Color.blue);
            StartCoroutine(DamageRecoveryTimeSlime());
        }
    }
    private IEnumerator DamageRecoveryTimeSlime()
    {
        yield return new WaitForSeconds(_damageRecaveryRutineSo);
        _canTakeDamage=true;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, _enemySO.enemyDamageAmount);
        }
    }
    private void DetectDeath()
    {
        if (_currentHealth <= 0 && _isAlive)
        {
            _isAlive = false;
            OnEnemyDeath?.Invoke(this, EventArgs.Empty);
           
        }
    }
    public void ReturnToPoolAnimation()
    {
        _enemyPool.ReturnToPool(gameObject);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
