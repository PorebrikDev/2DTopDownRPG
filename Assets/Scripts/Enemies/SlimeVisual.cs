using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeVisual : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;
    private EnemyAi enemyAi; 
    private EnemyEntity enemyEntity;

    private NavMeshAgent _agent;
    private CapsuleCollider2D _capsule;
    private PolygonCollider2D _polygon;

    private Animator _animator;

    private const string IS_RUNNING = "IsRunning";
    private const string ATTACK = "Attack";
    private const string ISDIE = "IsDie";

    private void Awake()
    {
        enemyAi = GetComponentInParent<EnemyAi>();
        enemyEntity = GetComponentInParent<EnemyEntity>();


        _animator = GetComponent<Animator>();
        _agent = GetComponentInParent<NavMeshAgent>();
        _capsule = GetComponentInParent<CapsuleCollider2D>();
        _polygon = GetComponentInParent<PolygonCollider2D>();
    }
    private void OnEnable()
    {
        enemyAi.OnEnemyAttack += EnemyAi_OnEnemyAttack;
        enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
        enemyEntity.OnEnemyDeath += EnemyEntity_OnEventDeath;

        if (_capsule != null) _capsule.enabled = true;
        if (_polygon != null) _polygon.enabled = true;

        _animator.SetBool(ISDIE, false);

        _agent.enabled = false;
        _agent.enabled = true;

        _agent.speed = enemySO.moveSpeed;
        _agent.acceleration = enemySO.acceleration;
        _agent.isStopped = false;
        _agent.ResetPath();
    }

    private void EnemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger("TakeHit");
    }

    private void Update()
    {
        _animator.SetBool(IS_RUNNING, enemyAi.IsRunning);
        _animator.SetFloat("ChasingSpeedMultiplier", enemyAi.GetRoamingAnimationSpeed());
    }
    public void TrigerAttackAnimationTurnOff()
    {
        enemyEntity.PoligonColliderTurnOff();
    }
    public void TrigerAttackAnimationTurnOn()
    {
        enemyEntity.PoligonColliderTurnOn();
    }
    private void EnemyAi_OnEnemyAttack(object sender, EventArgs e)
    {
      _animator.SetTrigger(ATTACK);
    }
    private void EnemyEntity_OnEventDeath(object sender, EventArgs e)
    {
        _animator.SetBool(ISDIE, true);
        _agent.speed = 0;
        _agent.acceleration = 0;
        _agent.velocity = Vector3.zero;


        _capsule.enabled = false;
        _polygon.enabled = false;
    }
    private void DestroySlime()
    {
        enemyEntity.ReturnToPoolAnimation();
    }


    private void OnDisable()
    {
        enemyAi.OnEnemyAttack -= EnemyAi_OnEnemyAttack;
        enemyEntity.OnTakeHit -= EnemyEntity_OnTakeHit;
        enemyEntity.OnEnemyDeath -= EnemyEntity_OnEventDeath;
    }

    }
