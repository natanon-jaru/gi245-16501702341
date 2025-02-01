using UnityEngine;
using UnityEngine.AI;
public enum CharState
{
    Idle,
    Walk,
    Attack,
    Hit,
    Die
}

public abstract class Character : MonoBehaviour
{
    protected NavMeshAgent navAgent;
    
    protected Animator anim;
    public Animator Animator { get { return anim; } }

    [SerializeField] 
    protected CharState state;
    public CharState State { get { return state; } }

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
    }
}
