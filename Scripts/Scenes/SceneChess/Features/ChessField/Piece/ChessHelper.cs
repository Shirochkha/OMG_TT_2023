using App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece;
using System;

namespace AssemblyCSharp.Assets.App.Scripts.Scenes.SceneChess.Features.ChessField.Piece
{
    public static class ChessHelper
    {
        public static IChessUnit GetChess(ChessUnitType chessUnitType)
        {
            return chessUnitType switch
            {
                ChessUnitType.Pon => new Pon(ChessUnitColor.White),
                ChessUnitType.Bishop => new Bishop(ChessUnitColor.White),
                ChessUnitType.Queen => new Queen(ChessUnitColor.White),
                ChessUnitType.King => new King(ChessUnitColor.White),
                ChessUnitType.Rook => new Rook(ChessUnitColor.White),
                ChessUnitType.Knight => new Knight(ChessUnitColor.White),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
