using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece
{
    public interface IChessUnit
    {
        List<Vector2Int> GetFreeCells(ChessGrid grid, Vector2Int from);

        bool IsValidMove(Vector2Int from, Vector2Int to, ChessGrid grid);
    }
}
