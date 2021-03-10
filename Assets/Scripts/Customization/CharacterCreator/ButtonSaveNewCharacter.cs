using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSaveNewCharacter : MonoBehaviour
{
	private Character character;

	[SerializeField] private Button buttonSave;

	private void Awake()
	{
		buttonSave.interactable = false;
	}

	public void SetCharacter(Character character)
	{
		this.character = character;

		buttonSave.interactable = true;
	}

    public void Save()
	{

	}
}
