using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatView : MonoBehaviour
{
    [Range(0f, 360f)]
    public float range;
    public float radius;
    public float speed;

    public GameObject Mouse;
    private Rigidbody rb;

    public LayerMask Target;
    public LayerMask Obstruction;

    public bool InView;

    public Transform[] positions;
    public int start;

    private int i;

    private void Start()
    {
        Mouse = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FOVRoutine());

        transform.position = positions[start].position;
    }
    private void Update()
    {
        if (InView)
        {
            MoveTowardsPlayer();
        }
        
        if (!InView)
        {
            MoveToPosition();
        }
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FOVCheck();
        }
    }

    private void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, Target);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < range / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, Obstruction))
                {
                    InView = true;

                    Vector3 direction = (target.position - transform.position).normalized;
                    direction.y = 0;  // ensure we only move horizontally
                    rb.velocity = direction * speed;
                    transform.forward = direction;  // make the cat face the player
                }
                else
                {
                    InView = false;
                }
            }
            else
            {
                InView = false;
            }
        }
        else if (InView)
        {
            InView = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (Mouse.transform.position - transform.position).normalized;
        direction.y = 0;  // Ensure movement is only horizontal
        transform.position += direction * speed * Time.deltaTime;
    }

    private void MoveToPosition()
    {
        Vector3 direction = (positions[i].position - transform.position).normalized;

        if (Vector2.Distance(transform.position, positions[i].position) < 0.1f)
        {
            i++;

            if (i >= positions.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, positions[i].position, speed * Time.deltaTime);
        transform.forward = direction;
    }
}
