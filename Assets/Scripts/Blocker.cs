using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blocker : MonoBehaviour
{
    private HashSet<GameObject> inside;
    [SerializeField] GameObject player;
    [SerializeField] Collider2D block;
    private int waitSteps = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inside = new HashSet<GameObject>();
    }

    void FixedUpdate()
    {
        if (block.enabled)
        {
            if (waitSteps > 0)
            {
                waitSteps--;
            }
            else
            {
                Block();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        inside.Add(collision.gameObject);
    }

    void Block()
    {
        if (inside.Contains(player))
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.position = Vector2.zero;
            inside.Remove(player);
        }
        foreach (GameObject i in inside)
        {
            if (i.GetComponent<Tilemap>() == null)
            {
                Destroy(i);
            }
        }
        inside.Clear();
        Destroy(gameObject);
    }
}
