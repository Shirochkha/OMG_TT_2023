using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using App.Scripts.Infrastructure.LevelSelection;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        private List<FillwordLevel> levels;
        private List<string> dictionary;

        public ProviderFillwordLevel()
        {
            TextAsset levelData = Resources.Load<TextAsset>("Fillwords/pack_0");
            TextAsset wordData = Resources.Load<TextAsset>("Fillwords/words_list");
            if (wordData != null)
            {
                LoadDictionaryFromFile(wordData);
            }

            if (levelData != null)
            {
                LoadLevelsFromFile(levelData);
            }
        }
        private void LoadDictionaryFromFile(TextAsset textAsset)
        {
            string[] lines = textAsset.text.Split('\n');
            dictionary = new List<string>(lines);
        }
        private void LoadLevelsFromFile(TextAsset textAsset)
        {
            levels = new List<FillwordLevel>();
            string[] levelData = textAsset.text.Split('\n');

            foreach (string line in levelData)
            {
                levels.Add(ParseLevel(line));
            }
        }
        private FillwordLevel ParseLevel(string line)
        {
            string[] parts = line.Split(' ');

            if (parts.Length < 2)
                throw new Exception("Неверный формат строки уровня: " + line);

            List<string> words = new List<string>();
            List<List<int>> positions = new List<List<int>>();

            for (int i = 0; i < parts.Length - 1; i += 2)
            {
                int wordIndex = int.Parse(parts[i]);
                string positionsStr = parts[i + 1];

                if (wordIndex < 0 || wordIndex >= dictionary.Count)
                    throw new Exception("Неверный индекс слова в уровне.");

                string[] positionsArray = positionsStr.Split(';');
                List<int> wordPositions = new List<int>();

                foreach (string position in positionsArray)
                {
                    int positionValue;
                    if (int.TryParse(position, out positionValue))
                    {
                        wordPositions.Add(positionValue);
                    }
                    else
                    {
                        throw new Exception("Неверный формат позиции в уровне: " + position);
                    }
                }

                words.Add(dictionary[wordIndex]);
                positions.Add(wordPositions);
            }

            return new FillwordLevel { Words = words, WordPositions = positions };
        }
        public GridFillWords LoadModel(int index)
        {
            if (index < 1 || index > levels.Count)
            {
                throw new Exception($"Уровень с индексом {index} не существует.");
            }

            int currentIndex = index - 1;

            while (currentIndex < index)
            {
                FillwordLevel selectedLevel = levels[currentIndex];
                if (IsLevelValid(selectedLevel))
                {
                    Vector2Int gridSize = CalculateGridSize(selectedLevel);
                    GridFillWords gridFillWords = CreateGridFillWords(selectedLevel, gridSize);
                    return gridFillWords;
                }
                else
                {
                    Debug.LogWarning("Пропущен неверный уровень: " + (currentIndex + 1));
                    return null;
                }
            }

            throw new Exception("Не удалось загрузить ни один из уровней.");
        }
        private bool IsLevelValid(FillwordLevel level)
        {
            int gridSize = CalculateGridSize(level).x;
            List<int> gridIndices = new();

            if (gridSize * gridSize != level.WordPositions.SelectMany(wp => wp).Distinct().Count())
            {
                Debug.LogError("Ошибка валидации уровня: недопустимые индексы.");
                return false;
            }

            foreach (var positions in level.WordPositions)
            {
                foreach (var position in positions)
                {
                    if (position < 0 || position >= gridSize * gridSize || gridIndices.Contains(position))
                    {
                        Debug.LogError("Ошибка валидации уровня: неверная позиция.");
                        return false;
                    }
                        
                    gridIndices.Add(position);
                }
            }

            if (gridIndices.Count != gridSize * gridSize)
            {
                Debug.LogError("Ошибка валидации уровня: неверное количество клеток.");
                return false;
            }

            foreach (var word in level.Words)
            {
                if (!dictionary.Contains(word))
                {
                    Debug.LogError("Ошибка валидации уровня: неверное слово из словаря.");
                    return false;
                }
            }

            return true;
        }
        private Vector2Int CalculateGridSize(FillwordLevel level)
        {
            int gridSize = (int)Math.Sqrt(level.WordPositions.SelectMany(wp => wp).Count());
            return new Vector2Int(gridSize, gridSize);
        }
        private GridFillWords CreateGridFillWords(FillwordLevel level, Vector2Int gridSize)
        {
            char[,] grid = new char[gridSize.x, gridSize.y];

            for (int i = 0; i < level.Words.Count; i++)
            {
                string word = level.Words[i];
                List<int> positions = level.WordPositions[i];

                foreach (int position in positions)
                {
                    int row = position / gridSize.x;
                    int col = position % gridSize.x;
                    grid[row, col] = word[positions.IndexOf(position)];
                }
            }

            GridFillWords gridFillWords = new(gridSize);

            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    gridFillWords.Set(i, j, new CharGridModel(grid[i, j]));
                }
            }

            return gridFillWords;
        }
        public class FillwordLevel
        {
            public List<string> Words { get; set; }
            public List<List<int>> WordPositions { get; set; }
        }
    }
}