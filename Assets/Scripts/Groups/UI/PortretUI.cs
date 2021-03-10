using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PortretUI : MonoBehaviour
{
    [HideInInspector] public Character character;

    [SerializeField] private Image background;
    [SerializeField] private Image portret;
    [SerializeField] private Image frame;
    [SerializeField] private Button buttonClick;

	public UnityAction<PortretUI> onClicked;
	public UnityAction<PortretUI> onSelected;

	private bool isSelected = false;

	private void Awake()
	{
		buttonClick.onClick.AddListener(Click);
	}

	private void Click()
	{
		onClicked?.Invoke(this);
		if(!isSelected)
			Select();
	}

	public void Select()
	{
		isSelected = true;

		onSelected?.Invoke(this);
	}
	public void Deselect()
	{
		isSelected = false;
	}


	public void SetCharacter(Character character)
	{
        this.character = character;
        UpdateAllUI();
    }

    private void UpdateAllUI()
	{
        if(character == null)
		{
            gameObject.SetActive(false);
			background.enabled = false;
		}
		else
		{
            gameObject.SetActive(true);
			background.enabled = true;
			background.color = character.Statistics.colors.portraitColor;
		}
	}
}