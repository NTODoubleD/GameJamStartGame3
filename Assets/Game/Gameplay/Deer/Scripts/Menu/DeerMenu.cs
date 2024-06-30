using UnityEngine;
using UnityEngine.AI;

public class DeerMenu : MonoBehaviour
{
    public Transform[] points; 
    private int destPoint = 0;
    private NavMeshAgent agent;

    [SerializeField] private int startNum;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        destPoint = startNum;
        
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}