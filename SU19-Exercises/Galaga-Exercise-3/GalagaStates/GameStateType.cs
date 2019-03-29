using System;

namespace Galaga_Exercise_3.GalagaStateType {
    public enum GameStateType {
        GameRunning, 
        GamePaused,
        MainMenu
    } 

    public class StateTransformer {

        /// <summary>
        /// Transform the given string to the corresponding GameStateType. 
        /// </summary>
        /// <param name="state">string to translate</param>
        /// <returns>GameStateType</returns>
        /// <exception cref="ArgumentException">exception for when state does not match the defined
        /// cases.</exception>
        public static GameStateType TransformStringToState(string state) {
            switch (state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            default:
                throw new ArgumentException("Invalid String");
            }
        }

        /// <summary>
        /// Transform the received GameStateType to the corresponding string. 
        /// </summary>
        /// <param name="state">enum of GameStateType</param>
        /// <returns>String</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string TransformStateToString(GameStateType state) {
            switch (state) {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                default:
                    throw new ArgumentException("Invalid State");
            }
        }
    }
}