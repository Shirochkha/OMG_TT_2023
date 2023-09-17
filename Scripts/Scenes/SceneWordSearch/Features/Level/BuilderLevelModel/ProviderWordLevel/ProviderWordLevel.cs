using System;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using UnityEngine;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel.ProviderWordLevel
{
    public class ProviderWordLevel : IProviderWordLevel
    {
        public LevelInfo LoadLevelData(int levelIndex)
        {
            string jsonFileName = "WordSearch/Levels/" + levelIndex.ToString();
            TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

            if (jsonFile != null)
            {
                try
                {
                    LevelInfo levelInfo = JsonUtility.FromJson<LevelInfo>(jsonFile.text);
                    return levelInfo;
                }
                catch (Exception e)
                {
                    Debug.LogError("Ошибка парсинга JSON файла для уровня " + levelIndex + ": " + e.Message);
                    return null;
                }
            }
            else
            {
                Debug.LogError("JSON файл не найден: " + jsonFileName);
                return null;
            }
        }
    }
}