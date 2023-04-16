using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class UI_Options : MonoBehaviour
{
	[SerializeField]
	private Button btn_Continue;
	[SerializeField]
	private Button btn_Restart;
	[SerializeField]
	private Button btn_Exit;

	// Start is called before the first frame update
	void Start()
    {
		btn_Continue.onClick.AddListener(OnContinue);
		btn_Restart.onClick.AddListener(OnRestart);
		btn_Exit.onClick.AddListener(OnExit);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void OnShow ()
	{
		Time.timeScale = 0;
		GameManager.Instance.GameIsPaused = true;
		gameObject.GetComponent<CanvasGroup>().alpha = 1;
		gameObject.GetComponent<CanvasGroup>().interactable = true;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void OnHide ()
	{
		Time.timeScale = 1;
		GameManager.Instance.GameIsPaused = false;
		gameObject.GetComponent<CanvasGroup>().alpha = 0;
		gameObject.GetComponent<CanvasGroup>().interactable = false;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	private void OnContinue ()
	{
		GameManager.Instance.Continue();
	}

	private void OnRestart ()
	{
		OnHide();
		GameManager.Instance.RestartGame();
	//	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void OnExit ()
	{
		GameManager.Instance.Exit();
	}
}
