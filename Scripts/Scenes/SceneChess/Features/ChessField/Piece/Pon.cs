using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece
{
        public class Pon : ChessUnit, IChessUnit
        {
            public Pon(ChessUnitColor color) : base(new(ChessUnitType.Pon, color))
            {
            }

            public List<Vector2Int> GetFreeCells(ChessGrid grid, Vector2Int from)
            {
                var freeCells = new List<Vector2Int>();
                if (IsValidMove(from, from + Vector2Int.up, grid))
                {
                    freeCells.Add(from + Vector2Int.up);
                }

                if (IsValidMove(from, from + Vector2Int.down, grid))
                {
                    freeCells.Add(from + Vector2Int.down);
                }

                return freeCells;
            }

            public bool IsValidMove(Vector2Int from, Vector2Int to, ChessGrid grid)
            {
            if (!IsMoveValid(to, grid))
            {
                return false;
            }

            return (from.y == to.y + 1 || from.y == to.y - 1) && from.x == to.x;
        }

        private bool IsMoveValid(Vector2Int to, ChessGrid grid)
        {
            if (to.y > grid.Size.y - 1 || to.x > grid.Size.x - 1 || to.y < 0 || to.x < 0)
            {
                return false;
            }

            if (grid.Get(to) != null)
            {
                return false;
            }

            return true;
        }
    }
}
