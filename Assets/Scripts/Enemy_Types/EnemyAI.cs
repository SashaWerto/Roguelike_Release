using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Seeker seeker;
    public Rigidbody2D rigidbody;
    [SerializeField] protected Transform gfx;
    public Animator animator;
    [Header("Preferences")]
    public float speed = 200f;
    [Header("Combat")]
    private float nextWaypointDistance = 0.2f;
    [HideInInspector] public Transform target;
    public AnimatorStateInfo animatorState;
    private Path path;
    private int currentWaypoint;
    [HideInInspector] public float distanceToPlayer;
    private bool reachedEndOfPath;
    protected Vector2 force;
    [HideInInspector] public Vector2 direction;
    protected virtual void Start()
    {
        StartCoroutine(UpdatePath());
    }
    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected virtual void Update()
    {
         animatorState = animator.GetCurrentAnimatorStateInfo(0);
         distanceToPlayer = (transform.position - target.position).magnitude;
    }
    public IEnumerator UpdatePath()
    {            
        while (true)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rigidbody.position, target.position, OnPathComplete);
                FlipToTarget();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    protected virtual void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody.position).normalized;
        force = direction * speed * Time.deltaTime;
        float distance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (animatorState.IsTag("block"))
            return;       
    }
    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void FlipToTarget()
    {
        if(!target) return;
        if (target.position.x < gfx.position.x) gfx.rotation = Quaternion.Euler(0, 180, gfx.localEulerAngles.z);
        else if (target.position.x > gfx.position.x) gfx.rotation = Quaternion.Euler(0, 0, gfx.localEulerAngles.z);
    }
}
