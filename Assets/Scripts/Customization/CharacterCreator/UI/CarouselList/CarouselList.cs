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

	private List<GameObject> gameObjects = new List<GameObject>();

	public UnityAction<int> onIndexChanged;

	private bool isEnable;
	public bool IsEnable
	{
		get => isEnable;
		private set
		{
			isEnable = value;
			
			UpdateList();
		}
	}


	private int currentIndex;
	public int CurrentIndex
	{
		set
		{
			if(value < 0) value += gameObjects.Count;

			currentIndex = value % gameObjects.Count;
			inputField.text = currentIndex.ToString();

			onIndexChanged?.Invoke(currentIndex);

			UpdateList();
		}
		get
		{
			return currentIndex;
		}
	}

	private bool once = true;

	public void SetCarousel(List<GameObject> gameObjects, bool enebled = true, int initIndex = 0)
	{
		if(this.gameObjects.Count > 0)
		{
			DisableAll();
			this.gameObjects.Clear();
		}

		if(once)
		{
			buttonLeft.onClick.AddListener(delegate { CurrentIndex--; });
			inputField.onValueChanged.AddListener((x) => { CurrentIndex = Convert.ToInt32(x); });
			buttonRight.onClick.AddListener(delegate { CurrentIndex++; });

			once = false;
		}
	

		this.gameObjects.AddRange(gameObjects);

		CurrentIndex = initIndex;

		IsEnable = enebled;

		UpdateList();
	}

	private void UpdateList()
	{
		DisableAll();


		if(IsEnable)
			gameObjects[CurrentIndex].SetActive(true);
	}
	private void DisableAll()
	{
		for(int i = 0; i < gameObjects.Count; i++)
		{
			gameObjects[i].SetActive(false);
		}
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