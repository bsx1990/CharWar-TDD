namespace Service
{
    public static class Printer
    {
        public static string Print(this Game game)
        {
            return $"{PrintCheckerboard(game.Checkerboard)}\r\n{PrintCandidate(game.CurrentCandidate, game.NextCandidate)}";
        }

        private static string PrintCandidate(Piece currentCandidate, Piece nextCandidate)
        {
            return $"Next:{nextCandidate.Print()}; Current:{currentCandidate.Print()}";
        }

        private static string PrintCheckerboard(Piece[,] checkerboard)
        {
            var result = string.Empty;
            for (var rowIndex = 0; rowIndex < checkerboard.GetLength(0); rowIndex++)
            {
                var rowResult = string.Empty;
                for (var columnIndex = 0; columnIndex < checkerboard.GetLength(1); columnIndex++)
                {
                    rowResult = $"{rowResult} [{checkerboard[rowIndex, columnIndex].Print()}]";
                }

                result = string.IsNullOrEmpty(result) ? rowResult.Trim() : $"{result}\r\n{rowResult.Trim()}";
            }

            return result;
        }

        private static string Print(this Piece piece) => $"{piece?.Value,2}";
    }
}