using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text winText;
    [SerializeField] FloatVariable health;
    [SerializeField] PhysicsCharacterController characterController;
    [Header("Events")]
    [SerializeField] IntEvent scoreEvent = default;
    [SerializeField] VoidEvent gameStartEvent = default;
    [SerializeField] VoidEvent playerDeadEvent = default;
    
    private int score = 0;

    public int Score 
    { 
        get { return score; } 
        set { 
            score = value; 
            if (score == 0)
            {
                scoreText.text = "000";
                winText.text = "000";
            } else if (score < 10)
            {
                scoreText.text = "00" + score.ToString();
                winText.text = "00" + score.ToString();
            } else if (score < 100)
            {
                scoreText.text = "0" + score.ToString();
                winText.text = "0" + score.ToString();
            } else
            {
                scoreText.text = score.ToString();
                winText.text = score.ToString();
            }
            
            scoreEvent.RaiseEvent(score);
        } 
    }

    private void OnEnable()
    {
        gameStartEvent.Subscribe(OnStartGame);
    }

    private void Start()
    {
        health.value = 5.5f;
    }

    public void AddPoints(int points)
    {
        Score += points;
    }

    private void OnStartGame()
    {
        characterController.enabled = true;
    }

    public void Damage(float damage)
    {
        health.value -= damage;
    }

    public void ApplyDamage(float damage)
    {
        health.value -= damage;
        if (health.value <= 0)
        {
            playerDeadEvent.RaiseEvent();
        }
    }

    public void OnRespawn(GameObject respawn)
    {
        Score = 0;
        transform.position = respawn.transform.position;
        transform.rotation = respawn.transform.rotation;
        characterController.Reset();
    }
}
