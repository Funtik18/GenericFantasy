using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSaveNewCharacter : MonoBehaviour
{
	private CharacterCustomizator customizator;

	[SerializeField] private Button buttonSave;

	private void Awake()
	{
		buttonSave.interactable = false;
	}

	public void SetCustomizator(CharacterCustomizator customizator)
	{
		this.customizator = customizator;

		buttonSave.interactable = this.customizator != null;
	}

    public void Save()
	{
		SaveLoaderManager.SaveCharacter(customizator.GetData());
	}
}