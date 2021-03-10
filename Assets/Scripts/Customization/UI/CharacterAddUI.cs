using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterAddUI : MonoBehaviour
{
	[HideInInspector] public Character character;

	[SerializeField] private Button buttonSelect;
	[SerializeField] private Image characterIcon;
	[SerializeField] private TMPro.TextMeshProUGUI characterName;
	[SerializeField] private GameObject panelRequit;
	[SerializeField] private Button buttonRequit;

	public UnityAction<CharacterAddUI> onSelected;
	public UnityAction<CharacterAddUI> onRequit;

	private bool isSelected = false;

	private void Awake()
	{
		buttonSelect.onClick.AddListener(Select);
		buttonRequit.onClick.AddListener(Requit);

		Deselect();
	}

	private void Select()
	{
		if(isSelected) return;

		panelRequit.SetActive(true);

		onSelected?.Invoke(this);

		isSelected = true;
	}
	public void Deselect()
	{
		panelRequit.SetActive(false);

		isSelected = false;
	}
	private void Requit()
	{
		onRequit?.Invoke(this);
	}
	public void Enable()
	{
		buttonSelect.interactable = true;
	}
	public void Disable()
	{
		buttonSelect.interactable = false;
		Deselect();
	}


	public void SetCharacter(Character character)
	{
		this.character = character;

		UpdateAllUI();
	}
	private void UpdateAllUI()
	{
		characterIcon.color = character.Statistics.colors.portraitColor;
	}
}