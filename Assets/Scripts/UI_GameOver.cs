using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class UI_GameOver : MonoBehaviour
{
	[SerializeField]
	private Button btn_Restart;
	[SerializeField]
	private Button btn_Revive;

	// Start is called before the first frame update
	void Start()
    {
		btn_Restart.onClick.AddListener(OnRestart);
		btn_Revive.onClick.AddListener(OnRevive);
    }

	// Update is called once per frame
	void Update()
    {
        
    }

	public void OnShow ()
	{
		gameObject.GetComponent<CanvasGroup>().alpha = 1;
		gameObject.GetComponent<CanvasGroup>().interactable = true;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void OnHide ()
	{
		gameObject.GetComponent<CanvasGroup>().alpha = 0;
		gameObject.GetComponent<CanvasGroup>().interactable = false;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	private void OnRestart ()
	{
		GameManager.Instance.RestartGame();
	//	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void OnRevive ()
	{
		OnHide();
		GameManager.Instance.Revive();
	}
}
