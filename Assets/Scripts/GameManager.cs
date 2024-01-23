using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] GameObject titleUI;
	[SerializeField] TMP_Text livesUI;
	//[SerializeField] TMP_Text timerUI;
	[SerializeField] Slider healthUI;

	[SerializeField] FloatVariable health;

	[SerializeField] GameObject respawn = null;

	[Header("Events")]
	//[SerializeField] IntEvent scoreEvent;
	[SerializeField] VoidEvent gameStartEvent;
	[SerializeField] GameObjectEvent respawnEvent;

	public enum State
	{
		TITLE,
		START_GAME,
		PLAY_GAME,
		GAME_OVER,
		WIN
	}

	public State state = State.TITLE;
	//[SerializeField] float timer = 0;
    [SerializeField] int lives = 0;
	[SerializeField] AudioSource musicSource;
	[SerializeField] AudioClip[] musicList;
	[SerializeField] GameObject[] enemies;

	public int Lives {  
		get { return lives; } 
		set { 
			lives = value; 
			livesUI.text = lives.ToString(); 
		} 
	}

	//public float Timer
	//{
	//	get { return timer; }
	//	set 
	//	{ 
	//		timer = value;
	//		timerUI.text = string.Format("{0:F1}", timer); //timer.ToString();
	//	}
	//}

    private void OnEnable()
    {
        //scoreEvent.Subscribe(OnAddPoints);
    }
    private void OnDisable()
    {
        //scoreEvent.Unsubscribe(OnAddPoints);
    }


    void Start()
	{
		PlayMusic(1);
		foreach (var enemy in enemies)
		{
			enemy.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		switch (state)
		{
			case State.TITLE:
				titleUI.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
                Lives = 3;
                break;
			case State.START_GAME:
				titleUI.SetActive(false);
				//timer = 60;
				health.value = 100;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				gameStartEvent.RaiseEvent();
				respawnEvent.RaiseEvent(respawn);
				EventManager.OnTimerStart();
				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
				break;
			case State.GAME_OVER:
				musicSource.Stop();
				break;
			case State.WIN:
				break;
			default:
				break;
		}

		healthUI.value = health.value / 100.0f;
	}

	

	public void OnStartGame()
	{
		PlayMusic(1);
		foreach (var enemy in enemies)
		{
			enemy.gameObject.SetActive(true);
		}
		state = State.START_GAME;
	}

	public void OnPlayerDead()
	{
		Lives--;
        state = State.START_GAME;
        if ( Lives < 0 )
		{
			state = State.TITLE;
        }
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