namespace BowlingKata
{
    class TenthFrame : Frame
    {
        public TenthFrame(int firstRollPins) : base(firstRollPins)
        {
        }

        public int? Roll3 { get; set; }

        public override int FrameTotal()
        {
            return base.FrameTotal() + (Roll3 ?? 0);
        }

        public override bool AddRoll(int pins)
        {
            if (Roll2 == null)
            {
                Roll2 = pins;
                return true;
            }
            if (Roll3 == null)
            {
                Roll3 = pins;
                return true;
            }
            return false;
        }
    }
}
