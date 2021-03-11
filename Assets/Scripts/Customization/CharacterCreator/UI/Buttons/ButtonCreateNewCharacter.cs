using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

public class ButtonCreateNewCharacter : MonoBehaviour
{
    [AssetList]
    [SerializeField] private Character characterBase;

    [SerializeField] private CharacterStand stand;

    private Character currentCharacter;

    public void CreateNew()
	{
        CharacterCustomizator customizator = FindObjectOfType<CharacterCustomizator>();

        currentCharacter = stand.ReplaceCharacter(characterBase);

        FindObjectOfType<ButtonSaveNewCharacter>().SetCustomizator(customizator);
        FindObjectOfType<ButtonLoadNewCharacter>().SetCustomizator(customizator);

        customizator.SetCharacter(currentCharacter);
    }
}
