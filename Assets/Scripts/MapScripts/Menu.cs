using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
	[Header("References")]
	[SerializeField] TextMeshProUGUI goldUI;
	[SerializeField] TextMeshProUGUI lifeUI;
	[SerializeField] TextMeshProUGUI roundUI;
	[SerializeField] private Animator anim;
	[SerializeField] private EnemySpawner _spawner;
	public Button startBtn;
	
	private bool isMenuOpen = true;

	private void Start()
	{
		Button btn = startBtn.GetComponent<Button>();
		btn.onClick.AddListener(DisableBtn);
	}

	private void Update()
	{
		if (!_spawner.isSpawning && !startBtn.interactable)
		{
			EnableButton();
		}
	}

	public void ToggleMenu()
	{
		isMenuOpen = !isMenuOpen;
		anim.SetBool("MenuOpen", isMenuOpen);
	}

	public void StartRound()
	{
		_spawner.StartWave();
	}

	private void DisableBtn()
	{
		startBtn.interactable = false;
	}

	private void EnableButton()
	{
		startBtn.interactable = true;
	}

	private void OnGUI()
	{
		goldUI.text = " Gold - " + LevelManager.main.gold.ToString();
		lifeUI.text = " Citizens - " + LevelManager.main.life.ToString();
		roundUI.text = " Round " + (EnemySpawner.main.currentWave +1).ToString();
	}
}
