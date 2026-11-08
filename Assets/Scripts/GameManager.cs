using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] GameObject leftArm;
    [SerializeField] GameObject rightArm;
    [SerializeField] GameObject leftLeg;
    [SerializeField] GameObject rightLeg;
    [SerializeField] TMP_Text health;
    [SerializeField] TMP_Text healthInfo;
    [SerializeField] TMP_Text leftArmInfo;
    [SerializeField] TMP_Text rightArmInfo;
    [SerializeField] TMP_Text leftLegInfo;
    [SerializeField] TMP_Text rightLegInfo;
    [SerializeField] TMP_Text leftArmEnemy;
    [SerializeField] TMP_Text rightArmEnemy;
    [SerializeField] TMP_Text leftLegEnemy;
    [SerializeField] TMP_Text rightLegEnemy;
    [SerializeField] TMP_Text timer;
    [SerializeField] private float timeLeft = 30f;
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject failurePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health.text = "<u>Health:</u>";
        timer.text = MathF.Floor(timeLeft).ToString();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timer.text = MathF.Floor(timeLeft).ToString();
        if (heart.GetComponent<Heart>().currHealth <= 0)
        {
            healthInfo.text = "Total Health: 0" + "/" + heart.GetComponent<Heart>().maxHealth;
        } else
        {
            healthInfo.text = "Total Health: " + heart.GetComponent<Heart>().currHealth + "/" + heart.GetComponent<Heart>().maxHealth;
        }
        if (leftArm != null)
        {
            leftArmInfo.text = "Left Arm Health: " + leftArm.GetComponent<Limb>().currHealth;
            leftArmEnemy.text = "Left Arm Enemy Count: " + leftArm.GetComponent<Limb>().CollisionCount();
        } else
        {
            leftArmInfo.text = "Left Arm Dead";
            leftArmEnemy.text = "Left Arm Dead";
        }
        if (rightArm != null)
        {
            rightArmInfo.text = "Right Arm Health: " + rightArm.GetComponent<Limb>().currHealth;
            rightArmEnemy.text = "Right Arm Enemy Count: " + rightArm.GetComponent<Limb>().CollisionCount();
        } else
        {
            rightArmInfo.text = "Right Arm Dead";
            rightArmEnemy.text = "Right Arm Dead";
        }
        if (leftLeg != null)
        {
            leftLegInfo.text = "Left Leg Health: " + leftLeg.GetComponent<Limb>().currHealth;
            leftLegEnemy.text = "Left Leg Enemy Count: " + leftLeg.GetComponent<Limb>().CollisionCount();
        } else
        {
            leftLegInfo.text = "Left Leg Dead";
            leftLegEnemy.text = "Left Leg Dead";
        }
        if (rightLeg != null)
        {
            rightLegInfo.text = "Right Leg Health: " + rightLeg.GetComponent<Limb>().currHealth;
            rightLegEnemy.text = "Right Leg Enemy Count: " + rightLeg.GetComponent<Limb>().CollisionCount();
        } else
        {
            rightLegInfo.text = "Right Leg Dead";
            rightLegEnemy.text = "Right Leg Dead";
        }
        if (MathF.Floor(timeLeft) == 0f)
        {
            Success();
        }
        if ((rightArm == null && leftArm == null && rightLeg == null && leftLeg == null) | heart.GetComponent<Heart>().currHealth <= 0)
        {
            Failure();
        }
    }

    void Success()
    {
        Time.timeScale = 0f;
        successPanel.SetActive(true);
    }

    void Failure()
    {
        Time.timeScale = 0f;
        failurePanel.SetActive(true);
    }
}
