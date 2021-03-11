using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoadNewCharacter : MonoBehaviour
{
	private CharacterCustomizator customizator;

	[SerializeField] private Button buttonLoad;

	private void Awake()
	{
		buttonLoad.interactable = false;
	}

	public void SetCustomizator(CharacterCustomizator customizator)
	{
		this.customizator = customizator;

		buttonLoad.interactable = this.customizator != null;
	}


	public void Load()
	{
		//CharacterStatisticsData data = SaveLoaderManager.LoadCharacter();
		//customizator.SetCharacterData(data);
	}
}
