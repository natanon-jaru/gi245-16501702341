using System;
using UnityEngine;
using UnityEngine.AI;
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
    public Animator Anim { get { return anim; } }

    [SerializeField] 
    protected CharState state;
    public CharState State { get { return state; } }

    [SerializeField] protected GameObject ringSelection;
    public GameObject RingSelection
    {
        get { return ringSelection; }
    }

    [SerializeField] 
    protected int curHP = 10;
    public int CurHP { get { return curHP; } }

    [SerializeField] protected Character curCharTarget;

    [SerializeField] protected float attackRange = 2f;

    [SerializeField] protected float attackCoolDown = 2f;

    [SerializeField] protected float attackTimer = 0f;

    protected void WalkUpdate()
    {
        float distance = Vector3.Distance(transform.position, navAgent.destination);
        Debug.Log(distance);

        if (distance <= navAgent.stoppingDistance)
            SetState(CharState.Idle);
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
        
        if(distance <= attackRange)
            SetState(CharState.Attack);
    }
}
