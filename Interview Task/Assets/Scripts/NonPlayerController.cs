using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NonPlayerController : MonoBehaviour
{
    Transform target;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float _lookRadius, _walkPointRange;

    [SerializeField] private GameManager manager;

    bool _isTalking;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= _lookRadius)
        {
            FaceTarget();

            if (!_isTalking)
            {
                manager._PopChat();
                _isTalking = true;
            }
        }
        else if (distance >= _lookRadius)
        {

        }
    }

    void FaceTarget()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
    }
}
