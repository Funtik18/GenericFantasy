using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

public class SaveLoaderManager
{
	private readonly static string expansion = ".txt";
	private readonly static string directorySaves = "/saves/";
	private readonly static string directoryCharacters = directorySaves + "Characters/";
	private readonly static string directoryDefalutCharacters = directoryCharacters + "defaults/";
	private readonly static string fileNamePlayerStatistics = "playerStatistics" + expansion;

	private static int isFirstTime = -1;
	public static bool IsFirstTime
	{
		set
		{
			isFirstTime = value ? 1 : 0;
			PlayerPrefs.SetInt("OnFirstTime", isFirstTime);
		}
		get
		{
			if(isFirstTime == -1)
			{
				isFirstTime = PlayerPrefs.GetInt("OnFirstTime", -1);
				if(isFirstTime == -1)
				{
					IsFirstTime = true;
				}
			}
			return isFirstTime == 1 ? true : false;
		}
	}

	public static void SaveCharacter(CharacterData data, string fileName)
	{
		SaveDataToJson(data, directoryCharacters, fileName);
	}
	public static List<CharacterData> LoadAllCharacters()
	{
		DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath + directoryCharacters);
		List<FileInfo> filesInfo = new List<FileInfo>();
		
		try
		{
			filesInfo = info.GetFiles().ToList();
		}
		catch(Exception e)
		{
			Debug.LogError("Empty characters folder");
		}

		List<CharacterData> files = new List<CharacterData>();

		for(int i = 0; i < filesInfo.Count; i++)
		{
			files.Add(LoadDataFromJson<CharacterData>(filesInfo[i].FullName));
		}

		return files;
	}


	public static void SavePlayerQuartet(Quartet quartet)
	{
		SaveDataToJson(quartet, directorySaves, fileNamePlayerStatistics);
	}

	public static void SavePlayerStatistics(EntityStatisticsData statistics)
	{
		SaveDataToJson(statistics, directorySaves, fileNamePlayerStatistics);
	}
	public static EntityStatisticsData LoadPlayerStatistics()
	{
		return LoadDataFromJson<EntityStatisticsData>(directorySaves, fileNamePlayerStatistics);
	}


	/// <summary>
	/// Сохранение объекта в Json.
	/// </summary>
	private static void SaveDataToJson<T>(T data, string directory, string fileName)
	{
		string dir = Application.persistentDataPath + directory;

		if(!Directory.Exists(dir))
		{
			Directory.CreateDirectory(dir);
		}

		string jsonData = JsonUtility.ToJson(data, true);
		File.WriteAllText(dir + fileName, jsonData);
	}

	/// <summary>
	/// Загрузка объекта из Json в объект.
	/// </summary>
	private static T LoadDataFromJson<T>(string directory, string fileName)
	{
		string fullPath = Application.persistentDataPath + directory + fileName;

		if(File.Exists(fullPath))
		{
			string json = File.ReadAllText(fullPath);
			return JsonUtility.FromJson<T>(json);
		}
		else
		{
			Debug.LogError("File doesn't exist");
		}

		return default;
	}
	private static T LoadDataFromJson<T>(string fullPath)
	{
		if(File.Exists(fullPath))
		{
			string json = File.ReadAllText(fullPath);
			return JsonUtility.FromJson<T>(json);
		}
		else
		{
			Debug.LogError("File doesn't exist");
		}

		return default;
	}


	public static void DestroyDirectory(string directory = "/saves/")
	{
		Directory.Delete(Application.persistentDataPath + directory, true);
	}
}