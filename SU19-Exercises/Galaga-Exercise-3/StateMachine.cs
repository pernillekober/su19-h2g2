using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaGame;
using Galaga_Exercise_3.GalagaStateType;
using GalagaGame.GalagaState;

namespace GalagaGame.GalagaState {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            ActiveState = MainMenu.GetInstance();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
               case (GameStateType.GameRunning):
                   
               case (GameStateType.GamePaused):
                   ActiveState = GameStateType.GamePaused;
               case (GameStateType.MainMenu):
                   ActiveState = GameStateType.MainMenu;
               default:
                   ActiveState = GameStateType.MainMenu;
                   
            }

        public static ProcessEvent(GameEventType gameEventType, GameEvent<object> gameEvent) {
            throw new NotImplementedException();
        }    
        }
    }
}