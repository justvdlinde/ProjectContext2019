using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class ActorMovementController : MonoBehaviour
{
    public Vector3 DestinationPosition { get; private set; }

    public GameObject targetObject;

    [SerializeField, AnimatorParameter] private string speedParameter;

    [SerializeField] private float turnSmoothing = 15f;
    [SerializeField] private float speedDampTime = 0.1f;
    [SerializeField] private float slowingSpeed = 0.175f;
    [SerializeField] private float turnSpeedThreshold = 0.5f;
    [SerializeField] private float inputHoldDelay = 0.5f;

    [SerializeField, HideInInspector] private NavMeshAgent agent;
    [SerializeField, HideInInspector] private Animator animator;

    private const float STOP_DISTANCE_PROPOPRTION = 0.1f;

    private void OnValidate()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (targetObject != null)
        {
            MoveToDestination(targetObject.transform.position);
        }
    }

    private void Update()
    {
        if (agent.isStopped || agent.pathPending) { return; }

        float speed = agent.desiredVelocity.magnitude;

        if (agent.remainingDistance <= agent.stoppingDistance * STOP_DISTANCE_PROPOPRTION)
        {
            Stop();
        }
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Slowing(out speed, agent.remainingDistance);
        }
        else if (speed > turnSpeedThreshold)
        {
            OnMoving();
        }

        animator.SetFloat(speedParameter, speed, speedDampTime, Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
    }

    public void MoveToDestination(Vector3 destination)
    {
        DestinationPosition = destination;
        agent.SetDestination(destination);

        agent.isStopped = false;
    }

    private void OnMoving()
    {
        Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
    }

    private void Slowing(out float speed, float distanceToDestination)
    {
        agent.isStopped = true;

        float proportionalDistance = 1f - distanceToDestination / agent.stoppingDistance;

        Quaternion targetRotation = transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);

        transform.position = Vector3.MoveTowards(transform.position, DestinationPosition, slowingSpeed * Time.deltaTime);

        speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.speed = 0f;
    }
}
