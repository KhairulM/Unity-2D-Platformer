using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager: MonoBehaviour {
    
    public int mainMenuSceneIdx;
    public CameraFollow cameraFollow; 
    public int initEnemiesSize = 1;
    public float enemySpawnSeconds = 3f;
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    public Text scoreText;
    public Text healthText;
    public GameObject playerObject;
    public UsernameController usernameController;
    public GameObject usernameInputUI;

    private int score;
    private ArrayList enemies;
    private Transform playerTransform;
    private PlayerHealth playerHealth;
    private PlayerMovementScript playerMovement;
    private PlayerShooting playerShooting;
    private float timer;

    private void Start() {
        playerTransform = playerObject.GetComponent <Transform>();
        playerHealth = playerObject.GetComponent <PlayerHealth>();
        playerMovement = playerObject.GetComponent <PlayerMovementScript>();
        playerShooting = playerObject.GetComponent<PlayerShooting>();

        usernameInputUI.SetActive(false);
        
        
        cameraFollow.Setup(() => playerTransform.position);
        enemies = new ArrayList();
        SpawnAllEnemies();
        EnablePlayer();
        DisableMusic();

        // setup input controller
        usernameController.Setup(() => this.score.ToString(), mainMenuSceneIdx);

        StartCoroutine(GameLoop());
    }

    private void Update() {
        timer += Time.deltaTime;
    }

    private void SpawnAllEnemies() {
        for (int i = 0; i < initEnemiesSize; i++) {
            EnemyManager enemyManager = new EnemyManager();

            System.Random rand = new System.Random();
            Transform enemySpawnPoint = enemySpawnPoints[rand.Next(enemySpawnPoints.Length)];

            enemyManager.instance = Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation) as GameObject;
            enemyManager.Setup(() => playerTransform.position,
                playerHealth, this);
            enemies.Add(enemyManager);
        }
    }

    private void SpawnNewEnemies() {
        if (timer >= enemySpawnSeconds) {
            timer = 0f;

            // spawn new enemies
            EnemyManager enemyManager = new EnemyManager();

            System.Random rand = new System.Random();
            Transform enemySpawnPoint = enemySpawnPoints[rand.Next(enemySpawnPoints.Length)];

            enemyManager.instance = Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation) as GameObject;
            enemyManager.Setup(() => playerTransform.position,
                playerHealth, this);
            enemies.Add(enemyManager);
        }
    }

    public void AddScore(int score) {
        this.score += score;
    }

    public void SetHealthText(float health) {
        healthText.text = "Health : " + health.ToString();
    }

    public void SetScoreText() {
        scoreText.text = "Score : " + this.score.ToString();
    }

    private IEnumerator GameLoop() {
        yield return StartCoroutine(Playing());

        if (GameOver()) {
            DisablePlayer();
            //StartCoroutine(PostScore());
            DisplayGameOverUI();
        }
    }

    private bool GameOver() {
        return playerHealth.health <= 0f;
    }

    private IEnumerator Playing() {
        do {
            // update UI
            SetHealthText(playerHealth.health);
            SetScoreText();

            // spawn new enemies
            SpawnNewEnemies();
            
            // return next frame
            yield return null;
        } while (!GameOver());
    }

    private void DisplayGameOverUI() {
        usernameInputUI.SetActive(true);
    }

    private void EnablePlayer() {
        playerMovement.enabled = true;
        playerShooting.enabled = true;
    }

    private void DisablePlayer() {
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    private void DisableMusic() {
        if (PlayerPrefs.GetInt("sounds") == 0) {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.mute = true;
        }
    }
}