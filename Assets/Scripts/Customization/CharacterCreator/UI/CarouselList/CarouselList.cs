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

	private bool isEnable;
	public bool IsEnable
	{
		get => isEnable;
		private set
		{
			isEnable = value;
		}
	}

	private void Awake()
	{
		buttonLeft.onClick.AddListener(delegate { this.piece.CurrentIndex--; });
		inputField.onValueChanged.AddListener((x) => { this.piece.CurrentIndex = Convert.ToInt32(x); });
		buttonRight.onClick.AddListener(delegate { this.piece.CurrentIndex++; });
	}

	public void SetCarousel(CharacterPiece piece, bool enebled = true)
	{
		this.piece = piece;

		this.piece.onIndexChanged += SetText;
		SetText(this.piece.CurrentIndex);

		IsEnable = enebled;
	}

	private void SetText(int value)
	{
		if(value >= 0)
			inputField.text = value.ToString();
	}

	public void Enable()
	{
		IsEnable = true;
	}
	public void Disable()
	{
		IsEnable = false;
	}
}