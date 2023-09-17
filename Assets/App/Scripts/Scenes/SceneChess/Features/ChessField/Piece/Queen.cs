using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using AssemblyCSharp.Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece
{
    public class Queen : ChessUnit, IChessUnit
    {
        public Queen(ChessUnitColor color) : base(new(ChessUnitType.Queen, color))
        {
        }

        public List<Vector2Int> GetFreeCells(ChessGrid grid, Vector2Int from)
        {
            var freeCells = new Rook().GetFreeCells(grid, from);
            freeCells.AddRange(new Bishop().GetFreeCells(grid, from));
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

            return deltaX == 0 || deltaY == 0 || deltaX== deltaY;
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
