using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using AssemblyCSharp.Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece
{
    public class King : ChessUnit, IChessUnit
    {
        public King(ChessUnitColor color) : base(new(ChessUnitType.King, color))
        {
        }

        public List<Vector2Int> GetFreeCells(ChessGrid grid, Vector2Int from)
        {
            var freeCells = new List<Vector2Int>();

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }

                    if (IsValidMove(from, from + new Vector2Int(dx, dy), grid))
                    {
                        freeCells.Add(from + new Vector2Int(dx, dy));
                    }
                }
            }

            return freeCells;
        }

        public bool IsValidMove(Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            if (!IsMoveValid(to, grid))
            {
                return false;
            }

            int deltaX = Mathf.Abs(to.x - from.x);
            int deltaY = Mathf.Abs(to.y - from.y);

            return deltaX <= 1 && deltaY <= 1;
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
