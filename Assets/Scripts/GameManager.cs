using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_OVER,
        WIN,
		PAUSE
    }

    [Header("UI")]
    [SerializeField] GameObject titleUI;
	[SerializeField] GameObject LevelUI;
	[SerializeField] GameObject GameUI;
	[SerializeField] GameObject WinUI;
	[SerializeField] TMP_Text livesUI;
	[SerializeField] Slider healthUI;

	[SerializeField] TMP_Text levelTime;
	[SerializeField] TMP_Text winTime;

    [Header("Variables")]
    [SerializeField] FloatVariable health;
	[SerializeField] GameObject respawn = null;

    public State state = State.TITLE;
    //[SerializeField] float timer = 0;
    [SerializeField] int lives = 0;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip[] musicList;
    bool musicPlayed = false;
    [SerializeField] GameObject[] enemies;
    GameObject[] pickups;

    private float timer = 0.0f;

    [Header("Events")]
	//[SerializeField] IntEvent scoreEvent;
	[SerializeField] VoidEvent gameStartEvent;
	[SerializeField] GameObjectEvent respawnEvent;

	public int Lives {  
		get { return lives; } 
		set { 
			lives = value; 
			livesUI.text = lives.ToString(); 
		} 
	}

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }


    void Start()
	{
		pickups = GameObject.FindGameObjectsWithTag("Pickup");

        foreach (var enemy in enemies)
		{
			enemy.gameObject.SetActive(false);
		}

		DespawnPickups();
	}

	void Update()
	{
		switch (state)
		{
			case State.TITLE:
				titleUI.SetActive(true);
                
                if (!musicPlayed)
				{
                    PlayMusic(0);
                    musicPlayed = true;
                }
				timer = 0;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
                Lives = 3;
                break;
			case State.START_GAME:
                musicPlayed = false;
				titleUI.SetActive(false);
				LevelUI.SetActive(true);
				timer += Time.deltaTime;
				if (timer >= 2.0f)
				{
					LevelUI.SetActive(false);
					GameUI.SetActive(true);
					health.value = 100;
                    gameStartEvent.RaiseEvent();
					SpawnPickups();
					SpawnEnemies();
                    respawnEvent.RaiseEvent(respawn);
                    state = State.PLAY_GAME;
                }
				break;
			case State.PLAY_GAME:
                EventManager.OnTimerStart();
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (Input.GetKeyDown(KeyCode.Escape)) 
				{
					Pause(true);
				}
                break;
			case State.GAME_OVER:
				EventManager.OnTimerStop();
				musicSource.Stop();
				break;
			case State.WIN:
				WinUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
				winTime.text = levelTime.text;
                if (!musicPlayed)
				{
					musicSource.Stop();
					musicSource.PlayOneShot(musicList[2]);
					musicPlayed=true;
				}
				break;
			case State.PAUSE:
				Time.timeScale = 0;
                EventManager.OnTimerStop();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (Input.GetKeyDown(KeyCode.Escape)) 
				{
					Pause(false);
				}
                break;
			default:
				break;
		}

		healthUI.value = health.value / 100.0f;
	}

	public void Pause(bool p)
	{
		if(p)
		{
            GameUI.SetActive(false);
            state = State.PAUSE;
        } else
		{
            GameUI.SetActive(true);
            state = State.PLAY_GAME;
        }
	}

	public void SpawnEnemies()
	{
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }

	public void DespawnPickups()
	{
        foreach (var pickup in pickups)
        {
            pickup.SetActive(false);
        }
    }

    public void SpawnPickups()
    {
        foreach (var pickup in pickups)
        {
            pickup.SetActive(true);
        }
    }

    public void OnStartGame()
	{
        foreach (var pickup in pickups)
        {
            pickup.SetActive(false);
        }
        PlayMusic(1);
		state = State.START_GAME;
	}

	public void OnPlayerDead()
	{
		if (state != State.WIN)
		{
            Lives--;
            state = State.START_GAME;
            if (Lives < 0)
            {
                state = State.TITLE;
                return;
            }
        }
	}

	public void OnPlayerWin()
	{
		state = State.WIN;
        GameUI.SetActive(false);
        EventManager.OnTimerStop();
    }

	public void OnWinContinue()
	{
		WinUI.SetActive(false);
		musicPlayed = false;
		state = State.TITLE;
	}

	public void OnAddPoints(int points)
	{
		print(points);
	}

	public void PlayMusic(int id)
	{
		musicSource.Stop();
		musicSource.clip = musicList[id];
		musicSource.Play();
	}
}