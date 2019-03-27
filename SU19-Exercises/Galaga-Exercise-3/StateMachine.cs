using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaGame;
using Galaga_Exercise_3.GalagaStateType;
using Galaga_Exercise_3.GalagaStates;

namespace GalagaGame.GalagaState {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);

            ActiveState = MainMenu.GetInstance();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
               case (GameStateType.GameRunning):
                   ActiveState = GameRunning.GetInstance();
                   break;
               case (GameStateType.GamePaused):
                   ActiveState = GamePaused.GetInstance();
                   break;
               case (GameStateType.MainMenu):
                   ActiveState = MainMenu.GetInstance();
                   break;
            }    
        }

        // if eventype = inputevent send information to Activestate.HandleKeyEvent and 
        // GameRunning.HandleKeyEvent to check if relevant and process inputEvent further.
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            Console.WriteLine("StateMachine.ProcessEvents");
            switch (eventType) { 
            case GameEventType.GameStateEvent when gameEvent.Message == "CHANGE_STATE":
                SwitchState(StateTransformer.TransformStringToState(gameEvent.Parameter1));
                break;
            case GameEventType.GameStateEvent:
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
                break;
            }
        }
    }
}