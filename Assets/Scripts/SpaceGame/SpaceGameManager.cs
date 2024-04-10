using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
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
	[SerializeField] GameObject[] UIList;

	[SerializeField] TMP_Text livesUI;
	[SerializeField] TMP_Text ScoreUI;
	[SerializeField] Slider healthUI;
	[SerializeField] float healthValue;


	[Header("Variables")]
	[SerializeField] FloatVariable health;
	[SerializeField] IntVariable score;
	[SerializeField] GameObject player;
	[SerializeField] GameObject playerSpline;
	[SerializeField] float splineSpeed = 0;

	[Header("Audio")]
	[SerializeField] AudioSource musicSource;
	[SerializeField] AudioClip[] musicList;

	[Header("Misc.")]
	
	[SerializeField] int lives = 0;

	public State state = State.TITLE;
	bool musicPlayed = false;
	GameObject[] pickups;
	GameObject[] enemies;

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
		pickups = GameObject.FindGameObjectsWithTag("Interact");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Lives = lives;
		state = State.TITLE;
	}

	private void Update()
	{
		switch (state)
		{
			case State.TITLE:
				LoadScreen(0);
				Time.timeScale = 0;

				DespawnPickups();
				DespawnEnemies();
				player.SetActive(false);
				playerSpline.GetComponent<PathFollower>().speed = 0;
				playerSpline.GetComponent<PathFollower>().tdistance = 0;
				if (!musicPlayed)
				{
					PlayMusic(1);
					musicPlayed = true;
				}
				
				break;
			case State.START_GAME:
				musicPlayed = false;

				LoadScreen(2);
				Time.timeScale = 1;

				PlayMusic(2);
				SpawnEnemies();
				SpawnPickups();
				player.SetActive(true);
				playerSpline.GetComponent<PathFollower>().speed = splineSpeed;
				playerSpline.GetComponent<PathFollower>().tdistance = 0;
				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:

				if(playerSpline.GetComponent<PathFollower>() != null)
				{
					if(playerSpline.GetComponent<PathFollower>().tdistance >= 1)
					{
						state = State.WIN;
					}
				}

				if(Input.GetKeyDown(KeyCode.Tab))
				{
					player.GetComponent<Inventory>().nextItem();
				}
				break;
			case State.PAUSE:
				break;
			case State.WIN:
				Time.timeScale = 0;

				DespawnPickups();
				DespawnEnemies();
				player.SetActive(false);
				playerSpline.GetComponent<PathFollower>().speed = 0;
				playerSpline.GetComponent<PathFollower>().tdistance = 0;

				LoadScreen(3);
				break;
			case State.GAME_OVER:
				LoadScreen(4);

				DespawnPickups();
				DespawnEnemies();
				player.SetActive(false);
				playerSpline.GetComponent<PathFollower>().speed = 0;
				playerSpline.GetComponent<PathFollower>().tdistance = 0;
				if (!musicPlayed)
				{
					PlayMusic(3);
					musicPlayed = true;
				}
				break;
			default:
				break;
		}

		healthUI.value = health.value / healthValue;
		ScoreUI.text = "" + score.value;
	}

	public void OnStartGame()
	{
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
			player.SetActive(false);
			Lives--;
			if (Lives <= 0)
			{
				musicPlayed = false;
				state = State.GAME_OVER;
				return;
			}
			StartCoroutine(RespawnPlayer());
		}
	}

	public void OnDeadContinue()
	{
		musicPlayed = false;
		state = State.TITLE;
	}

	public void QuitGame()
	{
		LoadScreen(0);
		state = State.TITLE;
	}

	public void OnAddPoints(int points)
	{
		print(points);
	}

	public void PlayMusic(int id)
	{
		if (musicList.Length > 0 && musicList[id] != null)
		{
			musicSource.Stop();
			musicSource.clip = musicList[id];
			musicSource.Play();
		}
	}

	public void LoadScreen(int selection)
	{
		
		for (int i = 0; i < UIList.Length; i++)
		{
			if (UIList[i] != null && UIList[selection] == UIList[i])
			{
				UIList[i].SetActive(true);
			}
			else
			{
				if (UIList[i] != null)
				{
					UIList[i].SetActive(false);
				}
			}
		}
	}

	IEnumerator RespawnPlayer()
	{
		Debug.Log("Respawn Entered");
		float time = 3f;
		yield return new WaitForSeconds(time);
		player.GetComponent<PlayerShip>().ApplyHealth(healthValue);
		player.SetActive(true);
		StopCoroutine(RespawnPlayer());
	}

	#region Spawn/Despawn
	public void SpawnPickups()
	{
		if (pickups == null) return;

		foreach (var pickup in pickups)
		{
			pickup.SetActive(true);
		}
	}

	public void DespawnPickups()
	{
		if (pickups == null) return;
		
		foreach (var pickup in pickups)
		{
			pickup.SetActive(false);
		}
	}

	public void SpawnEnemies()
	{
		if (enemies.Length <= 0) return;

		foreach (var enemy in enemies)
		{
			enemy.gameObject.SetActive(true);
		}
	}

	public void DespawnEnemies()
	{
		if (enemies.Length <= 0) return;

		foreach (var enemy in enemies)
		{
			enemy.gameObject.SetActive(false);
		}
	}
	#endregion
}
