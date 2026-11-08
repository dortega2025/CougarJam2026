using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    private bool shooting = false;
    private bool healing = false;
    public LineRenderer laser;
    public float laserRange = 5f;
    public GameObject barrel;
    public GameObject cursor;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private float damageInterval;
    private float damageTimer;
    [SerializeField] private float damage;
    public LineRenderer heal;
    [SerializeField] private LayerMask healLayer;
    [SerializeField] private float healInterval;
    [SerializeField] private float healAmount;
    private float healTimer = 0f;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        laser.enabled = false;
        // laser = transform.Find("Shoulder/Arm/Barrel/Laser").GetComponent<LineRenderer>();
        // barrel = transform.Find("Shoulder/Arm/Barrel").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Look();
    }

    void LateUpdate()
    {
        if (shooting)
        {
            ShootLaser();
        }
        if (healing)
        {
            Heal();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Movement()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started && !healing)
        {
            shooting = true;
        }
        if (context.canceled)
        {
            shooting = false;
            laser.enabled = false;
        }
    }

    void ShootLaser()
    {
        laser.enabled = true;
        Vector3 direction = new Vector3(cursor.transform.up.x, cursor.transform.up.y, 0).normalized;
        Vector3 end = barrel.transform.position + (direction.normalized * laserRange);
        RaycastHit2D hit = Physics2D.Raycast(barrel.transform.position, direction, laserRange, hitLayer);
        if (hit)
        {
           end = hit.point;
           damageTimer -= Time.deltaTime;
           if (damageTimer < 0f && hit.collider.gameObject.GetComponent<Tilemap>() == null && hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Bacteria>().health -= damage;
                damageTimer = damageInterval;
            }
        }
        laser.SetPosition(0, barrel.transform.position);
        laser.SetPosition(1, end);
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.started && !shooting)
        {
            healing = true;
        }
        if (context.canceled)
        {
            healing = false;
            heal.enabled = false;
        }
    }

    void Heal()
    {
        heal.enabled = true;
        Vector3 direction = new Vector3(cursor.transform.up.x, cursor.transform.up.y, 0).normalized;
        Vector3 end = barrel.transform.position + (direction.normalized * laserRange);
        RaycastHit2D hit = Physics2D.Raycast(barrel.transform.position, direction, laserRange, hitLayer);
        if (hit)
        {
           end = hit.point;
           healTimer -= Time.deltaTime;
           if (healTimer < 0f && hit.collider.gameObject.GetComponent<Tilemap>() == null 
            && hit.collider.gameObject.tag == "Heart" && hit.collider.gameObject.GetComponent<Heart>().currHealth < hit.collider.gameObject.GetComponent<Heart>().maxHealth)
            {
                float dif = hit.collider.gameObject.GetComponent<Heart>().maxHealth - hit.collider.gameObject.GetComponent<Heart>().currHealth;
                if (dif > healAmount)
                {
                    hit.collider.gameObject.GetComponent<Heart>().currHealth += healAmount;
                } else
                {
                    hit.collider.gameObject.GetComponent<Heart>().currHealth += dif;
                }
                
                healTimer = healInterval;
            }
        }
        heal.SetPosition(0, barrel.transform.position);
        heal.SetPosition(1, end);
    }

    void Look()
    {
        Vector3 direction = new Vector3(cursor.transform.up.x, cursor.transform.up.y, 0).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        angle = Mathf.Repeat(angle, 360);
        angle -= transform.rotation.z;
        int spriteDirection;
        switch (angle)
        {
            case < 45:
                spriteDirection = 0;
                break;
            case < 135:
                spriteDirection = 1;
                break;
            case < 225:
                spriteDirection = 2;
                break;
            default:
                spriteDirection = 3;
                break;
        }
        spriteRenderer.sprite = sprites[spriteDirection];
    }
}
