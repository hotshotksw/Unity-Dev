using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceGameManager : Singleton<GameManager>
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
	[SerializeField] GameObject TitleUI;
	[SerializeField] GameObject LoadUI;
	[SerializeField] GameObject GameUI;
	[SerializeField] GameObject WinUI;
	[SerializeField] GameObject PauseUI;
	[SerializeField] GameObject GameOverUI;

	[SerializeField] TMP_Text livesUI;
	[SerializeField] Slider healthUI;

	[SerializeField] GameObject[] UIList;

	[Header("Variables")]
	[SerializeField] FloatVariable health;
	[SerializeField] IntVariable score;
	[SerializeField] GameObject respawn;

	[Header("Audio")]
	[SerializeField] AudioSource musicSource;
	[SerializeField] AudioClip[] musicList;

	[Header("Misc.")]
	[SerializeField] GameObject[] enemies;
	[SerializeField] int lives = 0;

	public State state = State.TITLE;
	bool musicPlayed = false;
	GameObject[] pickups;

	private float timer = 0.0f;

	[Header("Events")]
	[SerializeField] VoidEvent gameStartEvent;
	[SerializeField] GameObjectEvent respawnEvent;

	public int Lives
	{
		get { return lives; }
		set
		{
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
		pickups = GameObject.FindGameObjectsWithTag("Interactor");

		foreach (var enemy in enemies)
		{
			enemy.gameObject.SetActive(false);
		}

		//DespawnPickups();
	}



	public void OnStartGame()
	{
		foreach (var pickup in pickups)
		{
			pickup.SetActive(false);
		}
		PlayMusic(1);
		timer = 0;

		state = State.START_GAME;
	}

	public void Pause(bool p)
	{
		if (p)
		{
			LoadScreen(3);
			state = State.PAUSE;
		}
		else
		{
			LoadScreen(2);
			state = State.PLAY_GAME;
		}
	}

	public void OnPlayerWin()
	{
		state = State.WIN;
		EventManager.OnTimerStop();

	}

	public void OnWinContinue()
	{
		musicPlayed = false;
		EventManager.OnTimerUpdate(0);
		state = State.TITLE;
	}

	public void OnPlayerDead()
	{
		if (state != State.WIN)
		{
			Lives--;
			state = State.START_GAME;
			if (Lives < 0)
			{
				musicPlayed = false;
				state = State.GAME_OVER;
				return;
			}
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

	public void LoadScreen(int selection)
	{
		for (int i = 0; i < UIList.Length; i++)
		{
			if (UIList[selection] == UIList[i])
			{
				UIList[i].SetActive(true);
			}
			else
			{
				UIList[i].SetActive(false);
			}
		}
	}

	public void QuitGame()
	{
		LoadScreen(0);
		EventManager.OnTimerStop();
		EventManager.OnTimerUpdate(0);
		state = State.TITLE;
	}

	public void SpawnPickups()
	{
		foreach (var pickup in pickups)
		{
			pickup.SetActive(true);
		}
	}

	public void DespawnPickups()
	{
		foreach (var pickup in pickups)
		{
			pickup.SetActive(false);
		}
	}

	public void SpawnEnemies()
	{
		foreach (var enemy in enemies)
		{
			enemy.gameObject.SetActive(true);
		}
	}

	public void DespawnEnemies()
	{
		foreach (var enemy in enemies)
		{
			enemy.gameObject.SetActive(false);
		}
	}
}
