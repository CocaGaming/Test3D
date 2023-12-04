using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAITest : MonoBehaviour
{
    [SerializeField]
    private Transform playerTF;

    private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        playerTF = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(playerTF.position);//di chuyển tới con player và tự tránh các vật cản trên đường
    }
}
