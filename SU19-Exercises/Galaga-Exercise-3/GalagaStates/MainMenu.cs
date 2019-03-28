using System;
using System.IO;
using System.Drawing;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaGame;
using Image = DIKUArcade.Graphics.Image;


namespace GalagaGame.GalagaState {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;

        private Entity backGroundImage;
        private Text[] menubuttons = new Text[2];
        private Text NewGame;
        private Text QuitGame;
        private int activeMenuButton = 0;
        private int maxMenuButtons = 1;

        
        public MainMenu(){
            // instance menubuttons
            NewGame = new Text("New Game", new Vec2F(0.35f, 0.30f),
                new Vec2F(0.3f, 0.3f));

            QuitGame = new Text("Quit Game", new Vec2F(0.35f, 0.20f),
                new Vec2F(0.3f, 0.3f));

            //Set colours for buttons' text (overwrite default black)
            NewGame.SetColor(Color.Teal);
            QuitGame.SetColor(Color.Teal);

            //Set font size
            NewGame.SetFontSize(60);
            QuitGame.SetFontSize(55);

            // Insert menubuttons objects in menubuttons list
            menubuttons[0] = NewGame;
            menubuttons[1] = QuitGame;

            // instance background
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f),
                    new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "TitleImage.png")));
            
        }

        
        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }


        public void GameLoop() {

        }

        public void InitializeGameState() {
        }

        public void UpdateGameLogic() {
        }

        public void RenderState() {
            //render background picture
            backGroundImage.RenderEntity();

            // render menubuttons and highligt selected menu button with colour 
            menubuttons[activeMenuButton].SetColor(Color.DarkViolet);
            menubuttons[activeMenuButton].SetFontSize(70);
            foreach (var button in menubuttons) {
                button.RenderText();
                if (button != menubuttons[activeMenuButton]    ) {
                    button.SetColor(Color.Teal);
                    button.SetFontSize(55);
                }
            }
        }
        
 // Calls KeyPress or KeyRelease if button inputevent is registered.
        public void HandleKeyEvent(string KeyValue, string keyAction) {
            switch (keyAction) {
            case "KEY_PRESS":
                KeyPress(KeyValue);
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
                            GameEventType.WindowEvent,
                            this, "CLOSE_WINDOW", "",
                            ""));
                }
                break;
            }
        }
    }
}

