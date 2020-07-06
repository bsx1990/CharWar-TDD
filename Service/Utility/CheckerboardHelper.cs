using System;

namespace Service.Utility
{
    public static class CheckerboardHelper
    {
        public static int MaxValue(this Piece[,] checkerboard)
        {
            var result = 1;
            for (var rowIndex = 0; rowIndex < checkerboard.GetLength(0); rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < checkerboard.GetLength(1); columnIndex++)
                {
                    result = Math.Max(result, checkerboard[rowIndex, columnIndex].Value ?? 0);
                }
            }

            return result;
        }
    }
}