namespace BowlingKata
{
    class Frame
    {
        public Frame(int firstRollPins)
        {
            Roll1 = firstRollPins;
        }

        public int Roll1 { get; }
        public int? Roll2 { get; protected set; }

        public int Bonus { get; set; }

        public bool IsStrike()
        {
            return Roll1 == 10;
        }

        public bool IsSpare()
        {
            return !IsStrike() && (Roll2 != null) && (Roll1 + Roll2 == 10);
        }

        public virtual int FrameTotal()
        {
            return Roll1 + (Roll2 ?? 0) + Bonus;
        }

        public virtual bool AddRoll(int pins)
        {
            if (Roll2 != null) return false;
            if (Roll1 == 10) return false;

            Roll2 = pins;
            return true;
        }
    }
}
