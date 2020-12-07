using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    int currentNode;
    int previousNode;
    public enum EnemyState
    {
        patrol,
        chase
    }

    EnemyState es = EnemyState.patrol;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentNode = Random.Range(0, GameManager.gm.nodes2.Length);
        previousNode = currentNode;
    }

    // Update is called once per frame
    void Update()
    {
        switch (es)
        {
            case EnemyState.patrol: Patrol(); break;
            case EnemyState.chase: Chase(); break;
            default: break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "node2")
        {
            currentNode = Random.Range(0, GameManager.gm.nodes2.Length);
            while (currentNode == previousNode)
            {
                currentNode = Random.Range(0, GameManager.gm.nodes2.Length);
            }
            previousNode = currentNode;
        }
        if (other.tag == "Player")
        {
            es = EnemyState.chase;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            es = EnemyState.patrol;
        }
    }
    void Patrol()
    {
        agent.destination = GameManager.gm.nodes2[currentNode].position;
    }

    void Chase()
    {
        agent.destination = GameManager.gm.player.transform.position;
    }
}
