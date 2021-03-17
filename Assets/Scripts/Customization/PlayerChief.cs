using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class PlayerChief : MonoBehaviour
{
    public QuartetUI quartetUI;
    public Quartet quartet;

	private void Awake()
	{
		quartetUI.Initialization(quartet);
	}


	[Button]
	private void Save()
	{
		SaveLoaderManager.SavePlayerQuartet(quartet);//сохраняем
	}

	[Button]
	private void ClearSaves()
	{
		SaveLoaderManager.IsFirstTime = true;
		SaveLoaderManager.DestroyDirectory();
	}
}