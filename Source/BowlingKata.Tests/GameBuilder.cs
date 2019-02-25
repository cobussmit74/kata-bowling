using System.Collections.Generic;
using System.Linq;

namespace BowlingKata.Tests
{
    public class GameBuilder
    {
        private const int numberOfRollsPerGame = 20;
        private const int numberOfFramesPerGame = 10;
        private readonly List<int> _rolls = new List<int>();
        
        public GameBuilder WithRoll(int pins)
        {
            _rolls.Add(pins);
            return this;
        }

        public GameBuilder WithXRolls(int rolls, int pinsPerRoll)
        {
            Enumerable.Range(1, rolls)
                .ToList()
                .ForEach(i => WithRoll(pinsPerRoll));

            return this;
        }

        public GameBuilder WithGutterBall()
        {
            return WithRoll(0);
        }

        public GameBuilder WithAllGutterBalls()
        {
            _rolls.Clear();

            return WithXGutterBalls(numberOfRollsPerGame);
        }

        public GameBuilder WithXGutterBalls(int rolls)
        {
            Enumerable.Range(1, rolls)
                .ToList()
                .ForEach(i => WithGutterBall());

            return this;
        }

        public GameBuilder WithRestOfGameAllGutterBalls()
        {
            return WithXGutterBalls(numberOfRollsPerGame - _rolls.Count);
        }

        public GameBuilder WithRestOfFramesAllGutterBalls()
        {
            var remain = numberOfFramesPerGame - (_rolls.Count / 2) - (_rolls.Count % 2);
            return WithXGutterBalls(remain*2);
        }

        public Game Build()
        {
            var game = new Game();

            foreach (var rollPins in _rolls)
            {
                game.Roll(rollPins);
            }

            return game;
        }
    }
}
