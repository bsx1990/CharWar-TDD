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
            CurrentCandidate = new Piece(1);
            NextCandidate = new Piece(1);
        }

        public override string ToString()
        {
            return this.Print();
        }

        public void Place(int row, int column)
        {
            Checkerboard[row, column] = CurrentCandidate;
            CurrentCandidate = NextCandidate;
            NextCandidate = new Piece(1);
        }
    }
}