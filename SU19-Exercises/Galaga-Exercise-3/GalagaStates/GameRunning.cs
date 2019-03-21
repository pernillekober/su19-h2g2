using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;

namespace Galaga_Exercise_3.GalagaStates {
    public class GameRunning : IGameState {
        
        private static GameRunning instance = null;
        
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
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