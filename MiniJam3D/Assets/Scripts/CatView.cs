using UnityEngine;

public class CatView : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float speed = 5f;
    public float detectionRadius = 5f;
    public float attackRange = 2.0f;
    public float _attackDamage = 50f;
    
    private Animator _animator;
    private GameObject player;
    private Vector3[] wanderPoints;
    private int currentPoint = 0;
    private bool isChasingPlayer = false;
    private float canAttack = 0f;

    private Vector3 currentForward = Vector3.forward;
    public float rotationSpeed = 1.0f;
    private float angleThreshold = 5.0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        wanderPoints = GenerateWanderPoints();
    }

    private Vector3[] GenerateWanderPoints()
    {
        Vector3[] points = new Vector3[3];
        for (int i = 0; i < 3; i++)
        {
            points[i] = (Random.insideUnitSphere * wanderRadius) + transform.position;
            points[i].y = transform.position.y;
        }
        return points;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        
        if (distanceToPlayer <= detectionRadius && !isChasingPlayer)
        {
            isChasingPlayer = true;
        }
        
        if (isChasingPlayer)
        {
            MoveTowards(player.transform.position);

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            MoveTowards(wanderPoints[currentPoint]);
            if (Vector3.Distance(transform.position, wanderPoints[currentPoint]) < 0.1f)
            {
                currentPoint = (currentPoint + 1) % 3; // Loop through the points
            }
        }
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;

        float angleToTarget = Vector3.Angle(transform.forward, direction);

        // Wenn der Gegner den Spieler verfolgt, bewegt er sich immer
        if(isChasingPlayer)
        {
            transform.position += direction * speed * Time.deltaTime;
            _animator.SetBool("isMoving", true);
        }
        else
        {
            if (angleToTarget <= angleThreshold)
            {
                transform.position += direction * speed * Time.deltaTime;
                _animator.SetBool("isMoving", true);
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }
        }

        currentForward = Vector3.Slerp(currentForward, direction, rotationSpeed * Time.deltaTime);
        transform.forward = currentForward;
    }

    private void AttackPlayer()
    {
        _animator.SetTrigger("isAttacking");
        // You can add more attack logic here, for now, it just triggers the animation
    }

    public void CanAttackPlayer(float canAttackValue)
    {
        canAttack = canAttackValue;
    }

    public void RequestDoDamageToPlayer(IDamageable player)
    {
        if (canAttack == 1f)
            player.TakeDamage(_attackDamage);
    }
}
