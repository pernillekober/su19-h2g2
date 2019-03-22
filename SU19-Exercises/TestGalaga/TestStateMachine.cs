using System;
using System.Collections.Generic;
using NUnit.Framework;
using GalagaGame.GalagaState;
using Galaga_Exercise_3.GalagaStates;
using Galaga_Exercise_3.GalagaGame;
using DIKUArcade.EventBus;

namespace Galaga_Testing {
    [TestFixture]
    public class StateMachineTesting {
        private StateMachine stateMachine;

        [SetUp]
        public void InitiateStateMachine() {
            DIKUArcade.Window.CreateOpenGLContext();
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> {
                GameEventType.GameStateEvent
            });
            stateMachine = new StateMachine();
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);

// Here you should:
// (1) Initialize a GalagaBus with proper GameEventTypes
// // (2) Instantiate the StateMachine
// (3) Subscribe the GalagaBus to proper GameEventTypes
// // and GameEventProcessors
        }

        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }

        [Test]
        public void TestEventGamePaused() {
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this,
                    "CHANGE_STATE",
                    "GAME_PAUSED", ""));
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }
    }
}