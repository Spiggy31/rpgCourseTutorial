﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int currentHp;
    public int maxHP;
    public float moveSpeed;

    [Header("Target")]
    public float chaseRange;
    public float attackRange;
    private Player player;

    [Header("Attack")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;

    // components
    private Rigidbody2D rig;

    private void Awake()
    {
        // get the player target
        player = FindObjectOfType<Player>();

        // get the rigid body component
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float playerDist = Vector2.Distance(transform.position, player.transform.position);
        if (playerDist <= attackRange)
        {
            // attack the player
            if(Time.time - lastAttackTime >= attackRate)
                Attack();

            rig.velocity = Vector2.zero;
        }
        // if we're in the chase range,  chase after the player
        else if (playerDist <= chaseRange)
        {
            Chase();
        }
        else
        {
            rig.velocity = Vector2.zero;
        }
    }

    // move towards the player
    private void Chase()
    {
        // calculate direction between us and the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        rig.velocity = direction * moveSpeed;
    }


    // attacks Player
    private void Attack()
    {
        lastAttackTime = Time.time;

        player.TakeDamage(damage);
    }

    // calculates damage
    public void TakeDamage(int damageTaken)
    {
        currentHp -= damageTaken;

        if (currentHp <= 0)
            Die();
    }

    // called when enemy dies
    private void Die()
    {
        Destroy(gameObject);
    }
}
