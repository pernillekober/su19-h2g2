using System;
using NUnit.Framework;
using Galaga_Exercise_3;
using Galaga_Exercise_3.GalagaStates;

namespace TestGalaga {
    [TestFixture]
    public class Tests {
        [Test]
        public void TestStateToStringGameRunning() {
            Assert.AreEqual("GAME_RUNNING",
                StateTransformer.TransformStateToString(GameStateType.GameRunning));
        }

        [Test]
        public void TestStateToStringMainMenu() {
            Assert.AreEqual("MAIN_MENU",
                StateTransformer.TransformStateToString(GameStateType.MainMenu));
        }
        [Test]
        public void TestStateToStringGamePaused() {
            Assert.AreEqual("GAME_PAUSED",
                StateTransformer.TransformStateToString(GameStateType.GamePaused));
        }

        [Test]
        public void TestStringToStateGameRunning() {
            Assert.AreEqual(GameStateType.GameRunning,
                StateTransformer.TransformStringToState("GAME_RUNNING"));
        }
        [Test]
        public void TestStringToStateMainMenu() {
            Assert.AreEqual(GameStateType.MainMenu,
                StateTransformer.TransformStringToState("MAIN_MENU"));
        }
        [Test]
        public void TestStringToStateGamePaused() {
            Assert.AreEqual(GameStateType.GamePaused,
                StateTransformer.TransformStringToState("GAME_PAUSED"));
        }

        [Test]
        public void TestStringToStateException() {
            Assert.Fail("State not valid", StateTransformer.TransformStringToState("Hello_World"));
        }
    }
}