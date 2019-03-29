using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using GalagaGame.GalagaState;
using Galaga_Exercise_3.GalagaGame;
using Galaga_Exercise_3.GalagaStateType;

namespace Galaga_Exercise_3.GalagaStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);

            ActiveState = MainMenu.GetInstance();
        }
        /// <summary>
        /// Returns a new ActiveState depending on the received StateType.
        /// </summary>
        /// <param name="stateType">enum to distinguish between types of states</param>
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

       
        /// <summary>
        /// ProcessEvent is called from Game.Gameloop every time an event is registered. ProcessEvent
        /// evaluates the eventType and calls the relevant method. In the case of a GamesStateEvent
        /// it calls SwitchState to change the current ActiveState according to the gameEvent.message.
        /// In the case of an InputEvent it calls the HandleKeyEvent with the gameEvent information
        /// for further processing and action.  
        /// </summary>
        /// <param name="eventType">enum to distinguish event system parts.</param>
        /// <param name="gameEvent">information/encodign about event registered in the eventBus.
        /// </param>
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            switch (eventType) {
            case GameEventType.GameStateEvent when gameEvent.Message == "CHANGE_STATE":
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.Parameter1));
                    break;
            case GameEventType.InputEvent:
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
                break;
            }
        }
    }
}