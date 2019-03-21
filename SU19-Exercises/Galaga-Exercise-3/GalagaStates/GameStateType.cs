namespace Galaga_Exercise_3.GalagaStates {
    public enum GameStateType {
        GameRunning,
        GamePaused,
        MainMenu
    }

    public class StateTransformer {

        public static GameStateType TransformStringToState(string state) {
            switch (state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            }
            return 
        }
   

    public static string TransformStateToString(GameStateType state) {
            switch (state) {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
            }
        }
    }
}