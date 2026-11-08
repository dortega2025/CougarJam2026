using UnityEngine;

public class Heart : MonoBehaviour
{
    public float currHealth;
    public float maxHealth = 1000f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
