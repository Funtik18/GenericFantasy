using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class DropdownEnum<T> : MonoBehaviour where T : Enum
{
    [SerializeField] protected TMPro.TMP_Dropdown dropdown;

	public UnityAction<int> onValueChanged;
	public UnityAction<T> onEnumChanged;

	protected virtual void Awake()
	{
		MakeDropdownToEnumList<T>();
		dropdown.onValueChanged.AddListener(ValueChanged);
	}

	protected virtual void ValueChanged(int value)
	{
		onValueChanged?.Invoke(value);
		onEnumChanged?.Invoke(GetEnumOption(value));
	}
	protected void MakeDropdownToEnumList<ENUM>()
	{
		List<ENUM> list = Enum.GetValues(typeof(ENUM)).Cast<ENUM>().ToList();
		List<string> liststrings = new List<string>();
		for(int i = 0; i < list.Count; i++)
		{
			liststrings.Add(list[i].ToString());
		}
		dropdown.AddOptions(liststrings);
	}

	public T GetEnumOption(int index)
	{
		return (T)Enum.Parse(typeof(T), dropdown.options[index].text);
	}
	public void SetEnumOption(T option)
	{
		dropdown.value = FindIndexByOption(option);
	}
	private int FindIndexByOption(T option)
	{
		string stringOption = option.ToString();

		for(int i = 0; i < dropdown.options.Count; i++)
		{
			if(dropdown.options[i].text == stringOption)
			{
				return i;
			}
		}
		return -1;
	}
}