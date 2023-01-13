using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;


public class BotMovement : MonoBehaviour
{


    public NavMeshData m_NavMeshData;
    private NavMeshDataInstance m_NavMeshInstance;
    private Vector3 _target;
    public NavMeshAgent myNavMeshAgent;
    [SerializeField]
    private Rigidbody _enemyRb;




    void OnEnable()
    {
        m_NavMeshInstance = NavMesh.AddNavMeshData(m_NavMeshData);
    }

    void OnDisable()
    {
        NavMesh.RemoveNavMeshData(m_NavMeshInstance);
    }

    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    private void Update()
    {
        myNavMeshAgent.SetDestination(_target);
        
    }

    private void FixedUpdate()
    {

            myNavMeshAgent.enabled = false; // disabling the navmesh agent.
            _enemyRb.AddForce(-transform.forward * 10, ForceMode.Impulse);
        
    }

    internal void AddTarget(GameObject target)
    {
        _target = target.transform.position;
    }

    internal void MoveRandomly()
    {
        _target = new Vector3(UnityEngine.Random.Range(0f, 25f), 0f, UnityEngine.Random.Range(0f, 25f));
    }





}




