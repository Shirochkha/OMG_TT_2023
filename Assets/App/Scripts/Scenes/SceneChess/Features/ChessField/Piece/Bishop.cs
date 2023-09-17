using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece
{
    public class Bishop : ChessUnit, IChessUnit
    {
        public Bishop(ChessUnitColor color = ChessUnitColor.White) : base(new(ChessUnitType.Bishop, color))
        {
        }

        public List<Vector2Int> GetFreeCells(ChessGrid grid, Vector2Int from)
        {
            var freeCells = new List<Vector2Int>();
            bool canMoveLeftTop = true, canMoveRightTop = true, canMoveLeftBottom = true, canMoveRightBottom = true;
            for (int dx = 1; dx <= 7; dx++)
            {
                if (canMoveLeftTop)
                {
                    if (IsValidMove(from, from + new Vector2Int(-dx, dx), grid))
                    {
                        freeCells.Add(from + new Vector2Int(-dx, dx));
                    }
                    else
                    {
                        canMoveLeftTop = false;
                    }
                }
                if (canMoveLeftBottom)
                {
                    if (IsValidMove(from, from + new Vector2Int(-dx, -dx), grid))
                    {
                        freeCells.Add(from + new Vector2Int(-dx, -dx));
                    }
                    else
                    {
                        canMoveLeftBottom = false;
                    }
                }
                if (canMoveRightTop)
                {
                    if (IsValidMove(from, from + new Vector2Int(dx, dx), grid))
                    {
                        freeCells.Add(from + new Vector2Int(dx, dx));
                    }
                    else
                    {
                        canMoveRightTop = false;
                    }
                }
                if (canMoveRightBottom)
                {
                    if (IsValidMove(from, from + new Vector2Int(dx, -dx), grid))
                    {
                        freeCells.Add(from + new Vector2Int(dx, -dx));
                    }
                    else
                    {
                        canMoveRightBottom = false;
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

            return deltaX == deltaY;
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
