using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private bool playerDetected = false;
    private float shootTimer;
    private int originalHealth;
    private Vector3 directionToPlayer;
    private Renderer objectRenderer;
    private Color originalColor;
    [SerializeField] private int health = 100;
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private Slider enemyHealthBarIndicator;

    

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        shootTimer = shootInterval;
        originalColor = objectRenderer.material.color;
        originalHealth = health;
    }

    void Update()
    {
        if (!playerDetected)
        {
            DetectPlayer();
        }
        else
        {
            FollowAndShootPlayer();
        }
    }

    //for detecting player, by shooting raycast
    void DetectPlayer()
    {
        RaycastHit hit;
        directionToPlayer = (player != null) ? (player.position - transform.position).normalized : transform.forward;
        Debug.DrawLine(transform.position, transform.position + directionToPlayer * detectionRange, Color.red);

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange, playerLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                player = hit.transform;
                playerDetected = true;
            }
        }
    }

    void FollowAndShootPlayer()
    {
        if (player == null) return;

        // Look at the player
        Vector3 playerPosition = player.position;
        transform.LookAt(new Vector3(playerPosition.x, transform.position.y, playerPosition.z));
        shootPoint.LookAt(playerPosition);

        // Shoot at the player
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(projectileSpeed * shootPoint.forward, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // reducing the health of enemy when player's bullet touches it and gets killed
        if (LayerMask.LayerToName(other.gameObject.layer) == "Bullet")
        {
            if (health < 10)
                Destroy(gameObject);
            health -= 10;
            //health bar indication on top of enemy
            enemyHealthBarIndicator.gameObject.SetActive(true);
            enemyHealthBarIndicator.value = (float)health / originalHealth;
            StartCoroutine(ChangeEnemyColorOnHit());
        }
    }

    //enemy color changes on getting hit, for the sake of visual feedback
    private IEnumerator ChangeEnemyColorOnHit()
    {
        objectRenderer.material.color = Color.magenta;
        yield return new WaitForSeconds(0.5f);
        objectRenderer.material.color = originalColor;
    }

}
