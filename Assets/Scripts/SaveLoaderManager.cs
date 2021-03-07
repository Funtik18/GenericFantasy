using System.IO;

using UnityEngine;

public class SaveLoaderManager
{
	private readonly static string expansion = ".txt";
	private readonly static string directorySaves = "/saves/";
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


	public static void DestroyDirectory(string directory = "/saves/")
	{
		Directory.Delete(Application.persistentDataPath + directory, true);
	}
}