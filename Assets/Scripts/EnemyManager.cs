using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class EnemyManager{
    public GameObject instance;
    EnemyMovementScript enemyMovement;
    EnemyHit enemyHit;
    EnemyHealth enemyHealth;


    public void Setup(Func<Vector2> getPlayerFunc, PlayerHealth playerHealth, GameManager gameManager) 
    {
        enemyMovement = instance.GetComponent<EnemyMovementScript>();
        enemyMovement.Setup(getPlayerFunc);
        enemyMovement.enabled = true;

        enemyHit = instance.GetComponent<EnemyHit>();
        enemyHit.playerHealth = playerHealth;

        enemyHealth = instance.GetComponent<EnemyHealth>();
        enemyHealth.Setup(gameManager);
    }
}
