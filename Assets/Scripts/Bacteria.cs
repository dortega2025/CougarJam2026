using Unity.VisualScripting;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    public float health = 10f;
    public int pos;
    public GameObject limb;
    [SerializeField] private float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, limb.GetComponent<Limb>().bacteriaPos[pos].transform.position, moveSpeed * Time.deltaTime);
        if (health < 0f)
        {
            limb.GetComponent<Limb>().toBeAdded.Enqueue(pos);
            Destroy(gameObject);
        }
    }
}
