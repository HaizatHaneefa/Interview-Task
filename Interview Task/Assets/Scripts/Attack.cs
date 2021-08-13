using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    BoxCollider bCollider;

    GameObject enemy, player;

    void Start()
    {
        bCollider = GetComponent<BoxCollider>();
        bCollider.enabled = false;

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy.GetComponent<EnemyStats>().EnemyTakeDamage();
            Debug.Log("sailot");
        }
    }
}
