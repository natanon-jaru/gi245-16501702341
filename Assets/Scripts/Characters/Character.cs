using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public enum CharState
{
    Idle,
    Walk,
    WalkToEnemy,
    Attack,
    Hit,
    Die
}

public abstract class Character : MonoBehaviour
{
    protected NavMeshAgent navAgent;

    protected Animator anim;

    public Animator Anim
    {
        get { return anim; }
    }

    [SerializeField] protected CharState state;

    public CharState State
    {
        get { return state; }
    }

    [SerializeField] protected GameObject ringSelection;

    public GameObject RingSelection
    {
        get { return ringSelection; }
    }

    [SerializeField] protected int curHP = 10;

    public int CurHP
    {
        get { return curHP; }
    }

    [SerializeField] protected Character curCharTarget; 
    public Character CurCharTarget {get{return curCharTarget;} set { curCharTarget = value; } }

    [SerializeField] protected float attackRange = 2f;
    public float AttackRange { get { return attackRange; } }

    [SerializeField] protected int attackDamage = 3;

    [SerializeField] protected float attackCoolDown = 2f;

    [SerializeField] protected float attackTimer = 0f;

    [SerializeField] 
    protected float findingRange = 20f;
    public float FindingRange { get { return findingRange; } }

    protected void WalkUpdate()
    {
        float distance = Vector3.Distance(transform.position, navAgent.destination);
        Debug.Log(distance);

        if (distance <= navAgent.stoppingDistance)
            SetState(CharState.Idle);
    }

    protected virtual IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5f);
            Destroy(gameObject);
    }

    protected virtual void Die()
    {
        navAgent.isStopped = true;
        SetState(CharState.Die);
        
        anim.SetTrigger("Die");

        StartCoroutine(DestroyObject());
    }

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(CharState s)
    {
        state = s;

        if (state == CharState.Idle)
        {
            navAgent.isStopped = true;
            navAgent.ResetPath();
        }
    }

    public void WalkToPosition(Vector3 dest)
    {
        if (navAgent != null)
        {
            navAgent.SetDestination(dest);
            navAgent.isStopped = false;
        }

        SetState(CharState.Walk);
    }

    public void ToggleRingSelection(bool flag)
    {
        ringSelection.SetActive(flag);
    }

    public void ToAttackCharacter(Character target)
    {
        if(curHP <= 0 || state == CharState.Die)
            return;
        
        //lock target
        curCharTarget = target;
        
        //start walking to enemy
        navAgent.SetDestination(target.transform.position);
        navAgent.isStopped = false;
        
        SetState(CharState.WalkToEnemy);
    }

    public void WalkToEnemyUpdate()
    {
        if (curCharTarget == null)
        {
            SetState(CharState.Idle);
            return;
        }

        float distance = Vector3.Distance(transform.position, curCharTarget.transform.position);

        if (distance <= attackRange)
        {
            SetState(CharState.Attack);
            Attack(); // First Attack
        }
        else
        {
            ToAttackCharacter(curCharTarget);
        }
    }

    protected void Attack()
    {
        transform.LookAt(curCharTarget.transform);
        
        anim.SetTrigger("Attack");
        
        //attack logic
        AttackLogic();
    }

    protected void AttackUpdate()
    {
        if (curCharTarget == null || curCharTarget.CurHP <= 0)
        {
            SetState(CharState.Idle);
            return;
        }

        navAgent.isStopped = true;

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            attackTimer = 0f;
            Attack();
        }

        float distance = Vector3.Distance(transform.position, curCharTarget.transform.position);

        if (distance > attackRange)
            SetState(CharState.WalkToEnemy);
           
    }

    public void ReceiveDamage(Character enemy)
    {
        if(curHP <= 0 || state == CharState.Die)
            return;

        curHP -= enemy.attackDamage;
        if (curHP <= 0)
        {
            curHP = 0;
            Die();
        }
    }

    protected void AttackLogic()
    {
        Character target = curCharTarget.GetComponent<Character>();
        
        if(target != null)
            target.ReceiveDamage(this);
    }

    public bool IsMyEnemy(string targetTag)
    {
        string myTag = gameObject.tag;

        if ((myTag == "Hero" || myTag == "Player") && targetTag == "Enemy")
            return true;

        if (myTag == "Enemy" && (targetTag == "Hero" || targetTag == "Player"))
            return true;

        return false;
    }
}
