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
    public class Knight : ChessUnit, IChessUnit
    {
        public Knight(ChessUnitColor color) : base(new(ChessUnitType.Knight, color))
        {
        }

        public List<Vector2Int> GetFreeCells(ChessGrid grid, Vector2Int from)
        {
            var freeCells = new List<Vector2Int>();
            int[] knightDx = { 2, 2, 1, 1, -1, -1, -2, -2 };
            int[] knightDy = { 1, -1, 2, -2, 2, -2, 1, -1 };

            for (int i = 0; i < knightDx.Length; i++)
            {

                if (IsValidMove(from, from + new Vector2Int(knightDx[i], knightDy[i]), grid))
                {
                    freeCells.Add(from + new Vector2Int(knightDx[i], knightDy[i]));
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

            return (deltaX == 1 && deltaY == 2) || (deltaX == 2 && deltaY == 1);
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

