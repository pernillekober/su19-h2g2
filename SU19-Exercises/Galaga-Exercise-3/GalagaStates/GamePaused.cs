using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;

namespace Galaga_Exercise_3.GalagaStates {
    public class GamePaused : IGameState {
        
        private static GamePaused instance = null;
        
        private Text[] pauseMenu = new Text[2];
        private Text ResumeGame;
        private Text ExitGame;
        private int activeMenuButton = 0;
        private int maxMenuButtons = 1;
        
        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }
        
        public void GameLoop() {
            throw new System.NotImplementedException();
        }

        public void InitializeGameState() {
            throw new System.NotImplementedException();
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }

        public void RenderState() {
            throw new System.NotImplementedException();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            throw new System.NotImplementedException();
        }
    }
}