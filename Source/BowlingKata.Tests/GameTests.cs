using FluentAssertions;
using NUnit.Framework;
using System;

namespace BowlingKata.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Constructor_ShouldNotThrow()
        {
            //arrange
            Action construct = () => CreateGame();
            //act
            //assert
            construct.Should().NotThrow();
        }

        [TestFixture]
        public class Roll
        {
            [Test]
            public void Given0_ShouldNotThrow()
            {
                //arrange
                var game = CreateGame();
                //act
                //assert
                game.Invoking(g => g.Roll(0)).Should().NotThrow();
            }

            [Test]
            public void Given10_ShouldNotThrow()
            {
                //arrange
                var game = CreateGame();
                //act
                //assert
                game.Invoking(g => g.Roll(10)).Should().NotThrow();
            }

            [Test]
            public void GivenNeg1_ShouldThrow()
            {
                //arrange
                var game = CreateGame();
                //act
                //assert
                game.Invoking(g => g.Roll(-1)).Should().Throw<ArgumentOutOfRangeException>()
                    .Which.ParamName.Should().Be("pins");
            }

            [Test]
            public void Given11_ShouldThrow()
            {
                //arrange
                var game = CreateGame();
                //act
                //assert
                game.Invoking(g => g.Roll(11)).Should().Throw<ArgumentOutOfRangeException>()
                    .Which.ParamName.Should().Be("pins");
            }
        }


        [TestFixture]
        public class Score
        {
            [Test]
            public void GivenAllGutterBalls_ShouldReturn0()
            {
                //arrange
                var game = new GameBuilder().WithAllGutterBalls().Build();
                //act
                var actual = game.Score();
                //assert;
                actual.Should().Be(0);
            }

            [Test]
            public void Given1PinAndRestGutters_ShouldReturn1()
            {
                //arrange
                var game = new GameBuilder()
                    .WithRoll(1)
                    .WithRestOfGameAllGutterBalls()
                    .Build();
                //act
                var actual = game.Score();
                //assert;
                actual.Should().Be(1);
            }

            [Test]
            public void GivenOnly18GutterBallRolls_ShouldThrow()
            {
                //arrange
                var game = new GameBuilder()
                    .WithXGutterBalls(18)
                    .Build();
                //act
                //assert;
                game.Invoking(g => g.Score())
                    .Should()
                    .Throw<InvalidOperationException>()
                    .WithMessage("Incomplete Game*");
            }

            [Test]
            public void Given20SinglePinRolls_ShouldReturn20()
            {
                //arrange
                var game = new GameBuilder()
                    .WithXRolls(20, 1)
                    .Build();
                //act
                var actual = game.Score();
                //assert;
                actual.Should().Be(20);
            }


            [TestFixture]
            public class Spares
            {
                [Test]
                public void GivenFrame1IsSpare_RestAreGutters_ShouldReturn10()
                {
                    //arrange
                    var game = new GameBuilder()
                        .WithRoll(4)
                        .WithRoll(6)
                        .WithRestOfGameAllGutterBalls()
                        .Build();
                    //act
                    var actual = game.Score();
                    //assert;
                    actual.Should().Be(10);
                }

                [Test]
                public void GivenFrame1IsSpare_Frame2Is5_RestAreGutters_ShouldReturn20()
                {
                    //arrange
                    var game = new GameBuilder()
                        .WithRoll(4)
                        .WithRoll(6)
                        .WithRoll(5)
                        .WithRestOfGameAllGutterBalls()
                        .Build();
                    //act
                    var actual = game.Score();
                    //assert;
                    actual.Should().Be(20);
                }

                [Test]
                public void GivenFrame1IsSpare_Frame2IsGutterThen5_RestAreGutters_ShouldReturn15()
                {
                    //arrange
                    var game = new GameBuilder()
                        .WithRoll(4)
                        .WithRoll(6)
                        .WithGutterBall()
                        .WithRoll(5)
                        .WithRestOfGameAllGutterBalls()
                        .Build();
                    //act
                    var actual = game.Score();
                    //assert;
                    actual.Should().Be(15);
                }
            }

            [TestFixture]
            public class Strikes
            {
                [Test]
                public void GivenRoll1IsStrike_Only18FurtherGuttersAreNeedForACompleteGame()
                {
                    //arrange
                    var game = new GameBuilder()
                        .WithRoll(10)
                        .WithXGutterBalls(18)
                        .Build();
                    //act
                    var actual = game.Score();
                    //assert;
                    actual.Should().Be(10);
                }


                [Test]
                public void GivenRoll1IsStrike_Frame2_4And4_RestGutters_ShouldReturn()
                {
                    //arrange
                    var game = new GameBuilder()
                        .WithRoll(10)
                        .WithRoll(4)
                        .WithRoll(4)
                        .WithRestOfFramesAllGutterBalls()
                        .Build();
                    //act
                    var actual = game.Score();
                    //assert;
                    actual.Should().Be(26);
                }
            }

            [TestFixture]
            public class TenthFrame
            {
                [Test]
                public void Given18GutterBalls_AndStrike_Adds2ExtraRolls_ShouldReturn12()
                {
                    //arrange
                    var game = new GameBuilder()
                        .WithXGutterBalls(18)
                        .WithRoll(10)
                        .WithRoll(1)
                        .WithRoll(1)
                        .Build();
                    //act
                    var actual = game.Score();
                    //assert;
                    actual.Should().Be(12);
                }

                [Test]
                public void Given18GutterBalls_And3_And7_Adds1ExtraRoll_ShouldReturn11()
                {
                    //arrange
                    var game = new GameBuilder()
                        .WithXGutterBalls(18)
                        .WithRoll(3)
                        .WithRoll(7)
                        .WithRoll(1)
                        .Build();
                    //act
                    var actual = game.Score();
                    //assert;
                    actual.Should().Be(11);
                }
            }
            
            [Test]
            public void GivenPerfectGame_ShouldReturn300()
            {
                //arrange
                var game = new GameBuilder()
                    .WithXRolls(12,10)
                    .Build();
                //act
                var actual = game.Score();
                //assert;
                actual.Should().Be(300);
            }
        }

        private static Game CreateGame()
        {
            return new Game();
        }
    }
}
