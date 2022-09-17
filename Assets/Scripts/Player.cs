using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float accelerationSpeed = 1f;
    public float steeringSpeed = 1f;

    public Camera camera;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer fireRenderer;
    public SpriteRenderer damageOverlayRenderer;
    public Sprite[] damageOverlays;

    public int maxHealth = 5;

    public WeaponSystem[] weaponSystems;

    Rigidbody2D rb;
    float acceleration;
    float steering;
    Rect visibleGameArea;
    int currentHealth;
    int currentDamageOverlayIndex = -1;
    int currentWeaponSystemIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        FindObjectOfType<HealthManager>().SetHealth(currentHealth);
        rb = GetComponent<Rigidbody2D>();
        DetermineVisibleGameArea();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        FindObjectOfType<HealthManager>().SetHealth(currentHealth);
        currentDamageOverlayIndex++;
        currentDamageOverlayIndex = Mathf.Min(currentDamageOverlayIndex, 2);
        damageOverlayRenderer.sprite = damageOverlays[currentDamageOverlayIndex];
        if (currentHealth < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void DetermineVisibleGameArea()
    {
        float halfWidth = camera.orthographicSize * camera.aspect;
        float halfHeight = camera.orthographicSize;
        Vector2 shipSize = spriteRenderer.size;
        visibleGameArea = new Rect(-halfWidth - shipSize.x, -halfHeight - shipSize.y,
            halfWidth * 2 + shipSize.x, halfHeight * 2 + shipSize.y);

    }

    // Update is called once per frame
    void Update()
    {
        // Tastur W S +1 -1
        // Controller Thumbstick links 1 - - 1
        acceleration = Mathf.Max(Input.GetAxis("Vertical"), 0);

        fireRenderer.enabled = acceleration > 0;

        // A - D 1 - -1
        steering = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
        {
            weaponSystems[currentWeaponSystemIndex].Fire();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeaponSystemIndex--;
            if (currentWeaponSystemIndex < 0)
            {
                currentWeaponSystemIndex = weaponSystems.Length - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentWeaponSystemIndex++;
            if (currentWeaponSystemIndex >= weaponSystems.Length)
            {
                currentWeaponSystemIndex = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 1f;
        }
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector2(0, acceleration * accelerationSpeed));
        rb.AddTorque(-steering * steeringSpeed);

        ConfineInPlayableArea();
    }

    void ConfineInPlayableArea()
    {
        // Links
        if (rb.position.x < visibleGameArea.xMin)
        {
            rb.position = new Vector2(visibleGameArea.xMax, rb.position.y);
        }
        // Rechts
        if (rb.position.x > visibleGameArea.xMax)
        {
            rb.position = new Vector2(visibleGameArea.xMin, rb.position.y);
        }
        // Oben
        if (rb.position.y > visibleGameArea.yMax)
        {
            rb.position = new Vector2(rb.position.x, visibleGameArea.yMin);
        }
        // Unten
        if (rb.position.y < visibleGameArea.yMin)
        {
            rb.position = new Vector2(rb.position.x, visibleGameArea.yMax);
        }
    }
}
