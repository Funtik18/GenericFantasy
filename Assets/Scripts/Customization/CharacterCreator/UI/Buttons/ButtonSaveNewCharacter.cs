using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonSaveNewCharacter : MonoBehaviour
{
	private CharacterCustomizator customizator;

	[SerializeField] private Toggle toggle;
	[SerializeField] private Button buttonSave;

	public UnityAction onSaved;

	private void Awake()
	{
		buttonSave.interactable = false;
		customizator = FindObjectOfType<CharacterCustomizator>();
		customizator.onCharacterCreated += EnableButton;
	}
	public void EnableButton()
	{
		buttonSave.interactable = customizator != null;
	}


    public void Save()
	{
		customizator.character.Save();
		onSaved?.Invoke();
	}
}