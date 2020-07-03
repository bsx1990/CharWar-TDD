using Service.Utility;
using Service.Utility.Exceptions;

namespace Service
{
    public class Game
    {
        public Piece CurrentCandidate { get; set; }
        public Piece NextCandidate { get; set; }

        public Piece[,] Checkerboard { get; }

        public Game()
        {
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
            NextCandidate = new Piece(1);
        }

        private void UpdateCheckerboard(int row, int column)
        {
            if (!Checkerboard[row, column].IsEmpty()) { throw new PlaceException($"row:{row} column:{column} not empty"); }
            
            Checkerboard[row, column] = CurrentCandidate;
        }
    }
}