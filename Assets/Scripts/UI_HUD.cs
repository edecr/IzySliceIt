using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_HUD : MonoBehaviour
{
	[SerializeField]
	private Transform HUD_Coin = default;

	[SerializeField]
	private Transform UI_Points = default;

	private TMP_Text _pointsText;

	private TMP_Text _coinText;

//	private int _counterCoin;

	[SerializeField]
	private TextMeshProUGUI textMesh;
	
	public float inDuration = 0.2f;
	public float outDuration = 0.5f;

	private bool isAnimating = false;

	private string _scoretemp = "";

	private void Start ()
	{
		_coinText = HUD_Coin.Find("coinText").GetComponent<TMP_Text>();
		_pointsText = UI_Points.Find("txt_Points").GetComponent<TMP_Text>();

		RefreshScore();
	}

	private void RefreshScore ()
	{
		_coinText.SetText(GameManager.Instance.Score.ToString());
	}

	public void SetCoinAmount (int amount)
	{
		_coinText.SetText((amount).ToString());
	}

	public void SetPoints ()
	{
	//	Debug.Log("somar+1");
		_scoretemp += "\n+1";
		_pointsText.SetText(_scoretemp);
		StartFadeIn();
	}

	public void StartFadeIn ()
	{
		if (!isAnimating)
		{
			isAnimating = true;
			StartCoroutine(FadeIn());
		}
	}
	
	IEnumerator FadeIn ()
	{
		float alpha = 0.0f;

		while (alpha < 1.0f)
		{
			alpha += Time.deltaTime * inDuration;
			Color color = textMesh.color;
			color.a = alpha;
			textMesh.color = color;
			yield return null;
		}

		yield return new WaitForSeconds(0.5f);

		while (alpha > 0.0f)
		{
			alpha -= Time.deltaTime * outDuration;
			Color color = textMesh.color;
			color.a = alpha;
			textMesh.color = color;
			yield return null;
		}

		isAnimating = false;
		_scoretemp = "";
	}
}
