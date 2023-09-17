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

namespace Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece
{
    public class Rook : ChessUnit, IChessUnit
    {
        public Rook(ChessUnitColor color = ChessUnitColor.White) : base(new(ChessUnitType.Rook, color))
        {
        }

        public List<Vector2Int> GetFreeCells(ChessGrid grid, Vector2Int from)
        {
            var freeCells = new List<Vector2Int>();
            bool top = true, bottom = true, left = true, right = true;
            for (int dx = 1; dx <= 7; dx++)
            {
                if (top)
                {
                    if (IsValidMove(from, from + new Vector2Int(0, dx), grid))
                    {
                        freeCells.Add(from + new Vector2Int(0, dx));
                    }
                    else
                    {
                        top = false;
                    }
                }
                if (bottom)
                {
                    if (IsValidMove(from, from + new Vector2Int(0, -dx), grid))
                    {
                        freeCells.Add(from + new Vector2Int(0, -dx));
                    }
                    else
                    {
                        bottom = false;
                    }
                }
                if (left)
                {
                    if (IsValidMove(from, from + new Vector2Int(-dx, 0), grid))
                    {
                        freeCells.Add(from + new Vector2Int(-dx, 0));
                    }
                    else
                    {
                        left = false;
                    }
                }
                if (right)
                {
                    if (IsValidMove(from, from + new Vector2Int(dx, 0), grid))
                    {
                        freeCells.Add(from + new Vector2Int(dx, 0));
                    }
                    else
                    {
                        right = false;
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

            return from.x == to.x || from.y == to.y;
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

