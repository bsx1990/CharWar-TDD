namespace Service
{
    public class Piece
    {
        public int? Value { get; }

        public Piece() { }

        public Piece(int value)
        {
            Value = value;
        }

        public bool IsEmpty()
        {
            return !Value.HasValue;
        }
    }
}