using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Libs.Factory;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel
{
    public class FactoryLevelModel : IFactory<LevelModel, LevelInfo, int>
    {
        public LevelModel Create(LevelInfo value, int levelNumber)
        {
            var model = new LevelModel();

            model.LevelNumber = levelNumber;

            model.Words = value.words;
            model.InputChars = BuildListChars(value.words);

            return model;
        }

        private List<char> BuildListChars(List<string> words)
        {
            var neededChars = new Hashtable();

            foreach (var word in words)
            {
                foreach (var pair in word.GroupBy(c => c).Select(c => new { Symbol = c.Key, Count = c.Count() }).ToArray())
                {
                    if (!neededChars.Contains(pair.Symbol))
                    {
                        neededChars.Add(pair.Symbol, pair.Count);
                    }
                    else
                    {
                        if ((int)neededChars[pair.Symbol] <= pair.Count)
                        {
                            neededChars[pair.Symbol] = pair.Count;
                        }
                    }
                }
            }

            var output = new List<char>();
            foreach (var key in neededChars.Keys)
            {
                for (int i = 0; i < (int)neededChars[key]; i++)
                {
                    output.Add((char)key);
                }
            }
            return output;
        }
    }
}