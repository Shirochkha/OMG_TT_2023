using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using AssemblyCSharp.Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class ChessGridNavigator : IChessGridNavigator
    {
        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            Dictionary<Vector2Int, Vector2Int> previous = new Dictionary<Vector2Int, Vector2Int>();

            queue.Enqueue(from);
            previous[from] = from;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == to)
                {
                    var path = new List<Vector2Int>();
                    while (current != from)
                    {
                        path.Add(current);
                        current = previous[current];
                    }
                    path.Reverse();
                    return path;
                }

                var validNeighbors = GetValidNeighbors(unit, current, grid);

                foreach (var neighbor in validNeighbors)
                {
                    if (!previous.ContainsKey(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        previous[neighbor] = current;
                    }
                }
            }

            return null;
        }

        private List<Vector2Int> GetValidNeighbors(ChessUnitType unit, Vector2Int current, ChessGrid grid)
        {
            var chess = ChessHelper.GetChess(unit);
            return chess.GetFreeCells(grid, current);
        }
    }
}
