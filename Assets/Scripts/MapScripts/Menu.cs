using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
	[Header("References")]
	[SerializeField] TextMeshProUGUI goldUI;
	[SerializeField] TextMeshProUGUI lifeUI;
	[SerializeField] private Animator anim;
	
	private bool isMenuOpen = true;

	public void ToggleMenu()
	{
		isMenuOpen = !isMenuOpen;
		anim.SetBool("MenuOpen", isMenuOpen);
	}

	private void OnGUI()
	{
		goldUI.text = " Gold - " + LevelManager.main.gold.ToString();
		lifeUI.text = " Citizens - " + LevelManager.main.life.ToString();
	}
}
