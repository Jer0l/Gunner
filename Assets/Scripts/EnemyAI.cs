using UnityEngine;
using ED262C;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float moveSpeed = 2f;

    private GameObject player;

    private SimpleArrayPriorityQueue<EnemyContext> priorityQueue;

    private void Awake()
    {
        priorityQueue = new SimpleArrayPriorityQueue<EnemyContext>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        EvaluateContexts();

        if (!priorityQueue.IsEmpty)
        {
            EnemyContext context = priorityQueue.Dequeue();
            ExecuteContext(context);
        }
    }

    private void EvaluateContexts()
    {
        priorityQueue.Clear();

        float distance = Vector2.Distance(transform.position, player.transform.position);

        priorityQueue.Enqueue(EnemyContext.Idle, 4);

        if (distance <= chaseRange)
            priorityQueue.Enqueue(EnemyContext.Chase, 2);

        if (distance <= attackRange)
            priorityQueue.Enqueue(EnemyContext.Attack, 1);
    }

    private void ExecuteContext(EnemyContext context)
    {
        switch (context)
        {
            case EnemyContext.Attack:
                Debug.Log("ATTACKING");
                break;

            case EnemyContext.Chase:
                Chase();
                break;

            case EnemyContext.Search:
                Debug.Log("SEARCHING");
                break;

            case EnemyContext.Idle:
                Debug.Log("IDLE");
                break;
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.transform.position,
            moveSpeed * Time.deltaTime
        );

        Debug.Log("SEEKING");
    }
}