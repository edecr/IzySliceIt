using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;

//	[SerializeField]
	private SlicerController _slicerController;

	public int Score;

//	[SerializeField]
	private UI_HUD _UI_HUD;

//	[SerializeField]
	private UI_GameOver _UI_GameOver;
	private UI_Options _UI_Options;
	private UI_EndGame _UI_EndGame;

	private int _sceneToLoad;

	public bool GameIsPaused = false;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}

	private void OnEnable ()
	{
		//	Debug.Log("GameManager.OnEnable");
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// called when the game is terminated
	void OnDisable ()
	{
		//	Debug.Log("GameManager.OnDisable");
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		Debug.Log("GameManager.OnSceneLoaded: " + scene.name);
		//	Debug.Log(mode);
		InitGame();
	}

	private void InitGame ()
	{
		LoadObjectsReferences();
		_slicerController.OnDie += _slicerController_OnDie;
		_slicerController.OnCheckPoint += _slicerController_OnCheckPoint;
	}

	private void _slicerController_OnCheckPoint ()
	{
		_sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

		if (_sceneToLoad < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(_sceneToLoad);
		} else
		{
			Debug.Log("FIMMM");
			_slicerController.enabled = false;
			_UI_EndGame.OnShow();
		}
	}





	private void _slicerController_OnDie ()
	{
		_slicerController.enabled = false;
		_UI_GameOver.OnShow();
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void SetScore ()
	{
		Score += 1;
		_UI_HUD.SetCoinAmount(Score);
	}

	public void RestartGame ()
	{
		_slicerController.OnDie -= _slicerController_OnDie;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Revive ()
	{
		_slicerController.enabled = true;
		_slicerController.MoveToLastPosition();
	}

	private void LoadObjectsReferences ()
	{
		_slicerController = GameObject.Find("Slicer").GetComponent<SlicerController>();
		_UI_HUD = GameObject.Find("UI_HUD").GetComponent<UI_HUD>();
		_UI_GameOver = GameObject.Find("UI_GameOver").GetComponent<UI_GameOver>();//GameObject.FindObjectOfType<UI_GameOver>();
		_UI_Options = GameObject.Find("UI_Options").GetComponent<UI_Options>();
		_UI_EndGame = GameObject.Find("UI_EndGame").GetComponent<UI_EndGame>();
	}

	public void Continue ()
	{
		_slicerController.enabled = true;
		_UI_Options.OnHide();
	}

	public void NewGame ()
	{
		SceneManager.LoadScene(0);

	}

	public void Exit ()
	{
		Application.Quit();
	}
}
