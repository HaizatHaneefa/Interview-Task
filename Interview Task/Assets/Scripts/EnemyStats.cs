using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] public float _enemyHealth;

    [SerializeField] private GameManager manager;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        _enemyHealth = 100f;

        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void EnemyTakeDamage()
    {
        _enemyHealth -= playerStats._givedamage;

        CheckEnemyHealth();
    }

    void CheckEnemyHealth()
    {
        if (_enemyHealth <= 0)
        {
            manager.EndGame();

            Debug.Log("you dead");
            // dead edi
        }
    }

    void Update()
    {
        CheckEnemyHealth();
    }
}
