using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class Character : Entity<CharacterScriptableData> 
{
	public override EntityStatistics Statistics
	{
		get
		{
			if(statistics == null)
			{
				//if(SaveLoaderManager.IsFirstTime)
				{
					statistics = new EntityStatistics(new EntityStatisticsData(data.data));//базовые значения

					//SaveLoaderManager.SavePlayerStatistics(statistics.GetData());//сохраняем

					//SaveLoaderManager.IsFirstTime = false;
				}
				//else
				{
					//statistics = new EntityStatistics(SaveLoaderManager.LoadPlayerStatistics());//загрузка базы
				}
			}
			return statistics;
		}
	}


	[Button]
	private void Save()
	{
		SaveLoaderManager.SavePlayerStatistics(statistics.GetData());//сохраняем
	}

	[Button]
	private void ClearSaves()
	{
		SaveLoaderManager.IsFirstTime = true;
		SaveLoaderManager.DestroyDirectory();
	}
}