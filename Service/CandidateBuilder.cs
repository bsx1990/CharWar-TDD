using System;

namespace Service
{
    public static class CandidateBuilder
    {
        public static Piece Build(in int upperLimit)
        {
             var random = new Random(DateTime.Now.Millisecond).Next(upperLimit) + 1;
             random = random > 9 ? 9 : random;
             return new Piece(random);
        }
    }
}