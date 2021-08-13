using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float _playerHealth, _getDamage, _givedamage;

    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private GameManager manager;

    public BoxCollider colSword;

    Animator anim;

    bool _isAttacking;

    void Start()
    {
        _playerHealth = 100;
        _getDamage = 10;
        _givedamage = 25;

        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        anim = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        Debug.Log("Player Health: " + _playerHealth);
        CheckPlayerHealth();

        _playerHealth -= _getDamage;
    }

    public void Attack()
    {
        colSword.enabled = true;
        _isAttacking = true;

        StartCoroutine(SetAttack());
    }

    IEnumerator SetAttack()
    {
        anim.SetBool("attack", true);

        yield return new WaitForSeconds(0.1f);

        anim.SetBool("attack", false);
        colSword.enabled = false;
    }

    private void Update()
    {
        CheckPlayerHealth();
    }

    void CheckPlayerHealth()
    {
        if (_playerHealth <= 0)
        {
            manager.EndGame();

            Debug.Log("you dead");
            // dead edi
        }
    }
}
