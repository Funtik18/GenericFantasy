using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputFieldRandomString : MonoBehaviour
{
	[SerializeField] private TMPro.TMP_InputField inputField;
	[SerializeField] private Button rndButton;

	private List<string> strings;

	public UnityAction<string> onValueChanged;

	public void Initialization(List<string> strings, int initIndex = -1)
	{
		this.strings = strings;

		inputField.onValueChanged.AddListener(onValueChanged);
	}

	public void SetFieldRandom()
	{
		SetField(Random.Range(0, strings.Count));
	}
	public void SetField(int index)
	{
		index = index % strings.Count;
		string s = strings[index];
		SetField(s);
	}
	public void SetField(string s)
	{
		inputField.text = s;
	}
}