using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterCustomizator : MonoBehaviour
{
	private Character character;
	[SerializeField] private CanvasGroup canvasGroup;
	public void SetCharacter(Character character)
	{
		this.character = character;

		canvasGroup.interactable = this.character == null;
	}
}
