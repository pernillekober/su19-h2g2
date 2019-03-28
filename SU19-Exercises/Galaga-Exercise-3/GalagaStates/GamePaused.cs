using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using System.IO;
using System.Drawing;
using DIKUArcade.EventBus;
using Galaga_Exercise_3.GalagaGame;
using Image = DIKUArcade.Graphics.Image;

namespace Galaga_Exercise_3.GalagaStates {
    public class GamePaused : IGameState {
        
        private static GamePaused instance = null;
        private Entity backGroundImage;
        private Text[] pauseMenu = new Text[2];
        private Text ResumeGame;
        private Text ExitGame;
        private int activeMenuButton = 0;
        private int maxMenuButtons = 1;

        public GamePaused() {
            ResumeGame = new Text("Resume", new Vec2F(0.35f, 0.30f),
                new Vec2F(0.3f, 0.3f));

            ExitGame = new Text("Main Menu", new Vec2F(0.35f, 0.20f),
                new Vec2F(0.3f, 0.3f));
            
            //Set colours for buttons' text (overwrite default black)
            ResumeGame.SetColor(Color.Teal);
            ExitGame.SetColor(Color.Teal);

            //Set font size
            ResumeGame.SetFontSize(60);
            ExitGame.SetFontSize(55);

            // Insert menubuttons objects in menubuttons list
            pauseMenu[0] = ResumeGame;
            pauseMenu[1] = ExitGame;
            
            // instance background
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f),
                    new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        }
        
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
     
        }

        public void RenderState() {
            backGroundImage.RenderEntity();

            // render menubuttons and highligt selected menu button with colour 
            pauseMenu[activeMenuButton].SetColor(Color.DarkViolet);
            pauseMenu[activeMenuButton].SetFontSize(70);
            foreach (var button in pauseMenu) {
                button.RenderText();
                if (button != pauseMenu[activeMenuButton]    ) {
                    button.SetColor(Color.Teal);
                    button.SetFontSize(55);
                }
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyAction) {
            case "KEY_PRESS":
                KeyPress(keyValue);
                break;
            }
        }

        public void KeyPress(string KeyValue) {
            switch (KeyValue) {
            case "KEY_UP":
                if (activeMenuButton != 0) {
                    activeMenuButton -= 1;
                }
                break;
            case "KEY_DOWN":
                if (activeMenuButton != 1) {
                    activeMenuButton += 1; 
                }
                break;
            case "KEY_ENTER":
                if (activeMenuButton == 0) {
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this, "CHANGE_STATE", "GAME_RUNNING",
                            ""));
                } else {
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this, "CHANGE_STATE", "MAIN_MENU",
                            ""));
                }
                break;
            }
        }
    }
}