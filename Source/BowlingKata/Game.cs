using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class Game
    {
        private readonly int numberOfFramesPerGame = 10;
        private List<Frame> _frames = new List<Frame>();

        public void Roll(int pins)
        {
            if ((pins < 0) || (pins > 10))
            {
                throw new ArgumentOutOfRangeException(nameof(pins));
            }

            var rollAdded = false;
            var currentFrame = _frames.LastOrDefault();
            if (currentFrame != null)
            {
                rollAdded = currentFrame.AddRoll(pins);
            }

            if (!rollAdded)
            {
                var normalFramesPerGame = (numberOfFramesPerGame - 1);
                if (_frames.Count == normalFramesPerGame)
                {
                    _frames.Add(new TenthFrame(pins));
                }
                else if (_frames.Count < normalFramesPerGame)
                {
                    _frames.Add(new Frame(pins));
                }
                else
                {
                    throw new InvalidOperationException("Game is already complete");
                }
            }
        }

        public int Score()
        {
            if (_frames.Count != numberOfFramesPerGame) throw new InvalidOperationException($"Incomplete Game - {_frames.Count}");
            
            SetFrameBonusAmounts();

            return _frames
                .Select(f => f.FrameTotal())
                .Sum();
        }
        
        private void SetFrameBonusAmounts()
        {
            for (var frameIndex = 0; frameIndex < _frames.Count; frameIndex++)
            {
                var isLastFrame = frameIndex == _frames.Count - 1;
                if (isLastFrame) continue;

                var thisFrame = FindFrame(frameIndex);
                var framePlus1 = FindFrame(frameIndex + 1);
                var framePlus2 = FindFrame(frameIndex + 2);

                if (thisFrame.IsStrike())
                {
                    thisFrame.Bonus = framePlus1.Roll1 + (framePlus1.Roll2 ?? framePlus2.Roll1);
                }
                else if (thisFrame.IsSpare())
                {
                    thisFrame.Bonus = framePlus1.Roll1;
                }
            }
        }

        private Frame FindFrame(int index)
        {
            return index < _frames.Count
                    ? _frames[index]
                    : null;
        }
    }
}
