using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody2D laser;
    public Transform fireTransform;
    public AudioClip fireClip;
    public float launchForce;

    [SerializeField] private Camera camera;
    private Vector2 targetPoint;
    private AudioSource shootingAudio;
    private PlayerHealth playerHealth;
    private bool shootingEnabled;


    // Start is called before the first frame update
    void Start()
    {
        shootingAudio = GetComponent<AudioSource>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnEnable() {
        shootingEnabled = true;
    }

    void OnDisable() {
        shootingEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0) && playerHealth.health > 0f && shootingEnabled) {
           targetPoint = camera.ScreenToWorldPoint(Input.mousePosition) - fireTransform.position;
           
           Fire();
       }
    }

    void Fire() {
        Rigidbody2D laserInstance = Instantiate(laser, fireTransform.position, fireTransform.rotation) as Rigidbody2D;
        float C = targetPoint.x/targetPoint.y;
        float Xvel = launchForce * C/Mathf.Sqrt(Mathf.Pow(C, 2) + 1);
        float Yvel = launchForce/Mathf.Sqrt(Mathf.Pow(C, 2) + 1);

        Transform laserTrasform = laserInstance.GetComponent<Transform>();
        
        laserInstance.velocity = new Vector2(Xvel, Yvel);
        laserTrasform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(targetPoint.y/targetPoint.x) * Mathf.Rad2Deg);

        shootingAudio.clip = fireClip;

        int sounds = PlayerPrefs.GetInt("sounds");
        if (sounds == 1) {
            shootingAudio.Play();
        }
    }
}
