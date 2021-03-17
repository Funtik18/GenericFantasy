using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuartetUI : MonoBehaviour
{
	private Quartet quartet;

	[SerializeField] private List<PortretUI> portrets = new List<PortretUI>();
	private Character selectedCharacter;

	[SerializeField] private Button buttonRemove;
	[Space]
	[SerializeField] private CharacterStand stand;
	[Space]
	[SerializeField] private CharacterSelection characterSelection;

	public UnityAction onCharacterSelected;
	public UnityAction<Character> onCharacterRemoved;

	public void Initialization(Quartet quartet)
	{
		this.quartet = quartet;
		this.quartet.onQuartetChanged += UpdateQuartetUI;

		characterSelection.onCharacterSelected = DeselectAll;

		buttonRemove.onClick.AddListener(QuartetSelectedCharacterRemove);

		for(int i = 0; i < portrets.Count; i++)
		{
			portrets[i].onSelected = QuartetCharacterSelected;
		}

		UpdateQuartetUI();
	}

	private void QuartetSelectedCharacterRemove()
	{
		buttonRemove.gameObject.SetActive(false);

		//deselect all
		for(int i = 0; i < portrets.Count; i++)
		{
			portrets[i].Deselect();
		}
		//remove
		if(quartet.RemoveCharacter(selectedCharacter))
		{
			onCharacterRemoved?.Invoke(selectedCharacter);
		}

		stand.ReplaceCharacter(null);
	}
	private void QuartetCharacterSelected(PortretUI portretUI)
	{
		selectedCharacter = portretUI.character;

		//deselect
		for(int i = 0; i < portrets.Count; i++)
		{
			if(portrets[i] != portretUI)
			{
				portrets[i].Deselect();
			}
		}

		stand.ReplaceCharacter(selectedCharacter);

		buttonRemove.gameObject.SetActive(true);

		onCharacterSelected?.Invoke();
	}
	private void DeselectAll()
	{
		buttonRemove.gameObject.SetActive(false);
		for(int i = 0; i < portrets.Count; i++)
		{
			portrets[i].Deselect();
		}
	}



	private void UpdateQuartetUI()
	{
		for(int i = 0; i < quartet.maxCharacters; i++)
		{
			portrets[i].SetCharacter(quartet.GetCharacter(i));
		}
	}
}