using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Limb : MonoBehaviour
{
    public GameObject[] bacteriaPos;
    public Queue<int> toBeAdded;
    [SerializeField] private float spawnInterval;
    private float spawnTimer = 0f;
    [SerializeField] private float cutInterval;
    private float cutTimer = 0f;
    private bool canSpawn;
    [SerializeField] private GameObject bacteriaPrefab;
    [SerializeField] private GameObject spawner;
    public HashSet<Collider2D> bacteriaCount;
    private bool cooldownStarted;
    [SerializeField] private Tilemap cutMap;
    public float currHealth;
    [SerializeField] private GameObject heart;
    private float damageTimer;
    [SerializeField] private int arraySize;
    [SerializeField] private Tilemap blocker;
    [SerializeField] private GameObject blockerCollider;
    private bool dying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bacteriaCount = new HashSet<Collider2D>();
        toBeAdded = new Queue<int>();
        for (int i = 0; i < arraySize; i++)
        {
            toBeAdded.Enqueue(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn)
        {
            cutTimer -= Time.deltaTime;
            if (cutTimer <= 0f)
            {
                canSpawn = true;
                cutMap.GetComponent<TilemapRenderer>().enabled = true;
                spawnTimer = 0f;
            }
        }
        if (canSpawn)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                Spawn();
                spawnTimer = spawnInterval;
            }
        }
        if (CollisionCount() == 0 && canSpawn && toBeAdded.Count == arraySize && !cooldownStarted)
        {
            canSpawn = false;
            cutTimer = cutInterval;
            cooldownStarted = true;
            cutMap.GetComponent<TilemapRenderer>().enabled = false;
        }
        if (toBeAdded.Count < arraySize)
        {
            cooldownStarted = false;
        }
        damageTimer -= Time.deltaTime;
        if (damageTimer < 0f)
        {
            TakeDamage();
            damageTimer = 1f;
        }
        if (currHealth < 0f && !dying)
        {
            dying = true;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        heart.GetComponent<Heart>().maxHealth -= 250f;
        blockerCollider.GetComponent<Collider2D>().enabled = true;
        blocker.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


    void Spawn()
    {
        if (toBeAdded.Count != 0)
        {
            GameObject bacteria;
            bacteria = Instantiate(bacteriaPrefab, spawner.transform.position, Quaternion.identity);
            bacteria.GetComponent<Bacteria>().pos = toBeAdded.Dequeue();
            bacteria.GetComponent<Bacteria>().limb = gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bacteriaCount.Add(collision.GetComponent<Collider2D>());
    }

    public int CollisionCount()
    {
        bacteriaCount.RemoveWhere(c => c == null || !c.gameObject.activeInHierarchy);
        return bacteriaCount.Count;
    }

    void TakeDamage()
    {
        currHealth -= 1f * CollisionCount();
        heart.GetComponent<Heart>().currHealth -= 1f * CollisionCount();
    }
}
