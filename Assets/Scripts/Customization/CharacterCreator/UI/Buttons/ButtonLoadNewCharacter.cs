using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoadNewCharacter : MonoBehaviour
{
	private CharacterCustomizator customizator;

	[SerializeField] private Button buttonLoad;
	[SerializeField] private TMPro.TMP_Dropdown dropdown;

	private List<CharacterStatisticsData> allCharacters;

	private void Awake()
	{
		buttonLoad.interactable = false;


		customizator = FindObjectOfType<CharacterCustomizator>();
		FindObjectOfType<ButtonSaveNewCharacter>().onSaved += LoadAllCharacters;

		LoadAllCharacters();
	}

	private void LoadAllCharacters()
	{
		allCharacters = SaveLoaderManager.LoadAllCharacters();

		if(allCharacters.Count > 0)
		{
			List<string> options = new List<string>();

			for(int i = 0; i < allCharacters.Count; i++)
			{
				//options.Add(allCharacters[i].informationData.firstName + "_" + allCharacters[i].informationData.secondName);
			}

			if(dropdown.options.Count > 0)
			{
				dropdown.options.Clear();
			}

			dropdown.AddOptions(options);

			buttonLoad.interactable = true;
			buttonLoad.onClick.AddListener(SelectCharacter);
		}
		else
			buttonLoad.interactable = false;
	}
	public void SelectCharacter()
	{
		//customizator.LoadCharacter(allCharacters[dropdown.value]);
	}
}