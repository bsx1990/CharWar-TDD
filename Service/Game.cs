using System;
using System.Collections.Generic;
using System.Linq;
using Service.Utility;
using Service.Utility.Exceptions;

namespace Service
{
    public interface IGame
    {
        Piece CurrentCandidate { get; set; }
        Piece NextCandidate { get; set; }
        Piece[,] Checkerboard { get; }
        int Score { get; }
        void Place(int row, int column);
    }

    public class Game : IGame
    {
        public Piece CurrentCandidate { get; set; }
        public Piece NextCandidate { get; set; }

        public Piece[,] Checkerboard { get; }
        public int Score { get; private set; }

        public Game()
        {
            Score = 0;
            Checkerboard = new Piece[GameConfig.RowsOfCheckerboard, GameConfig.ColumnsOfCheckerboard];
            InitCheckerboard();
            CurrentCandidate = new Piece(1);
            NextCandidate = new Piece(1);
        }

        private void InitCheckerboard()
        {
            for (var rowIndex = 0; rowIndex < Checkerboard.GetLength(0); rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < Checkerboard.GetLength(1); columnIndex++)
                {
                    Checkerboard[rowIndex, columnIndex] = new Piece();
                }
            }
        }

        public void Place(int row, int column)
        {
            UpdateCheckerboard(row, column);
            CurrentCandidate = NextCandidate;
            NextCandidate = CandidateBuilder.Build(Checkerboard.MaxValue());
            var combinedScore = Combine(Checkerboard, row, column);
            Score += combinedScore;
        }

        public static int Combine(Piece[,] checkerboard, int row, int column)
        {
            var aroundPieces = GetAroundPieces(checkerboard, row, column);
            var centerPiece = checkerboard[row, column];
            var combinedScore = 0;
            var equalsToCenterPiece = new Func<Piece, bool>(piece => piece.Value == centerPiece.Value);
            while (aroundPieces.Any(equalsToCenterPiece))
            {
                foreach (var piece in aroundPieces.Where(equalsToCenterPiece))
                {
                    combinedScore += piece.Value!.Value;
                    piece.Reset();
                }

                centerPiece.Upgrade();
            }

            return combinedScore;
        }

        private static List<Piece> GetAroundPieces(Piece[,] checkerboard, int row, int column)
        {
            (int, int) leftTopOffset = (-1, -1), topOffset = (0, -1), rightTopOffset = (1, -1),
                leftOffset = (-1, 0), rightOffset = (1, 0),
                leftBottomOffset = (-1, 1), bottomOffset = (0, 1), rightBottomOffset = (1, 1);
            var aroundDirections = new[] { leftTopOffset, topOffset, rightTopOffset, leftOffset, rightOffset, leftBottomOffset, bottomOffset, rightBottomOffset };
            var aroundPositions = aroundDirections
                .Select(direction => (row + direction.Item1, column + direction.Item2))
                .Where(position => position.Item1 >= 0 && position.Item1 < GameConfig.RowsOfCheckerboard 
                                                       && position.Item2 >= 0 && position.Item2 < GameConfig.ColumnsOfCheckerboard);
            return aroundPositions
                .Select(position => checkerboard[position.Item1, position.Item2])
                .Where(piece => !piece.IsEmpty())
                .ToList();
        }

        private void UpdateCheckerboard(int row, int column)
        {
            if (!Checkerboard[row, column].IsEmpty()) { throw new PlaceException($"row:{row} column:{column} not empty"); }
            
            Checkerboard[row, column] = CurrentCandidate;
        }

        public bool IsGameOver()
        {
            throw new NotImplementedException();
        }
    }
}