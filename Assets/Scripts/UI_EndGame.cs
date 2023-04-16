using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndGame : MonoBehaviour
{
    [SerializeField]
    private Button btn_NewGame;
    [SerializeField]
    private Button btn_Quit;

    // Start is called before the first frame update
    void Start()
    {
		btn_NewGame.onClick.AddListener(OnNewGame);
		btn_Quit.onClick.AddListener(OnQuit);
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

	private void OnQuit ()
	{
		GameManager.Instance.Exit();
	}

	private void OnNewGame ()
	{
		GameManager.Instance.NewGame();
	}
}
