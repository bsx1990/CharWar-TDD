namespace Service
{
    public class Piece
    {
        public int? Value { get; private set; }

        public Piece() { }

        public Piece(int value)
        {
            Value = value;
        }

        public bool IsEmpty() => !Value.HasValue;
        public void Reset() => Value = null;
        public void Upgrade() => Value = Value.HasValue ? Value + 1 : 1;
    }
}