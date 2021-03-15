using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CarouselList : MonoBehaviour
{
    [SerializeField] private Button buttonLeft;
    [SerializeField] private TMPro.TMP_InputField inputField;
    [SerializeField] private Button buttonRight;

	private CharacterPiece piece;

	private bool isCanBeMinus = false;

	private bool isEnable;
	public bool IsEnable
	{
		get => isEnable;
		set
		{
			isEnable = value;

			if(isEnable == false)
			{
				piece.CurrentIndex = -1;
			}
			else
			{
				if(piece.CurrentIndex == -1)
				{
					if(!isCanBeMinus)
						piece.CurrentIndex = 0;
				}
				else
				{
					piece.CurrentIndex = piece.CurrentIndex;
				}
			}
		}
	}

	private void Awake()
	{
		buttonLeft.onClick.AddListener(delegate { piece.Left(); });
		//inputField.onValueChanged.AddListener((x) => { piece.CurrentIndex = Convert.ToInt32(x); });
		buttonRight.onClick.AddListener(delegate { piece.Right(); });
	}

	public void SetCarousel(CharacterPiece piece, bool isCanBeMinus = false, bool enebled = true)
	{
		this.piece = piece;

		this.piece.onIndexChanged = null;
		this.piece.onIndexChanged += SetText;

		this.isCanBeMinus = isCanBeMinus;
	
		IsEnable = enebled;

		SetText(this.piece.CurrentIndex);
	}

	private void SetText(int value)
	{
		inputField.text = value.ToString();
	}
}