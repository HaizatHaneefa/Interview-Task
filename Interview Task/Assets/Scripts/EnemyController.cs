using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Transform target;
    [SerializeField] private NavMeshAgent agent;


    [SerializeField] private float _lookRadius, _attackDelay, _walkPointRange;

    bool _waitToAttack;

    [SerializeField] Animator anim;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();

        _attackDelay = 2f;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= _lookRadius)
        {

            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                Attack();
            }
            else if (distance > agent.stoppingDistance)
            {
                _waitToAttack = false;

                anim.SetBool("et", true);
            }
        }
        else if (distance >= _lookRadius)
        {
            OnPatrol();
        }

        if (_waitToAttack)
        {
            _attackDelay -= Time.deltaTime;
        }

        if (_attackDelay <= 0)
        {
            _attackDelay = 2f;
            _waitToAttack = false;
        }
    }

    void OnPatrol()
    {
        anim.SetBool("et", false);

        agent.SetDestination(transform.position);
    }

    void Attack()
    {
        if (!_waitToAttack)
        {
            target.GetComponent<PlayerStats>().TakeDamage();

            agent.SetDestination(transform.position);

            anim.SetBool("attack", true);
        }
        else if (_waitToAttack)
        {
            anim.SetBool("et", false);
            anim.SetBool("attack", false);
        }

        _waitToAttack = true;
    }


    void FaceTarget()
    {
        Vector3 _direction = (target.position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
    }
}
