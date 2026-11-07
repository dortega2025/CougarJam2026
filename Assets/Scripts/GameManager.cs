using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float heartHealth = 1000f;
    public float leftArmHealth = 250f;
    public float rightArmHealth = 250f;
    public float leftLegHealth = 250f;
    public float rightLegHealth = 250f;
    [SerializeField] TMP_Text healthInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthInfo.text = "<u>Health:</u>\nTotal Health: " + heartHealth + 
            "\nLeft Arm Health: " + leftArmHealth + "\nRight Arm Health: " + rightArmHealth + 
            "\nLeft Leg Health: " + leftLegHealth + "\nRight Leg Health: " + rightLegHealth;
    }
}
