using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_HUD : MonoBehaviour
{
	[SerializeField]
	private Transform HUD_Coin = default;

	private TMP_Text _coinText;

	private int _counterCoin;

	private void Start ()
	{
		_coinText = HUD_Coin.Find("coinText").GetComponent<TMP_Text>();

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
}
