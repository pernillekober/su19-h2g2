using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Security.AccessControl;
using System.Windows.Forms;
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
        private GameEventBus<object> eventBus = GalagaBus.GetBus();
        

        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
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
            //backGroundImage = new Entity(new DynamicShape(); 
            backGroundImage.RenderEntity();
            // instantiate buttons??? ny klasse eller instansiere i MainMenu eller Game?
            // render new game button
            // render quit button 

            // instance
            NewGame = new Text("New Game", 
                new Vec2F(0.5f, 0.4f), new Vec2F(0.1f,0.1f));
            QuitGame = new Text("Quit Game", 
                new Vec2F(0.5f,0.5f), new Vec2F(0.1f,0.1f) );

            menubuttons[0] = NewGame;
            menubuttons[1] = QuitGame;
            
            menubuttons[activeMenuButton].SetColor(Color.DarkViolet);
            
            // instance background
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), 
                new Vec2F(1.0f,1.0f)), new Image(Path.Combine("Assets", "Images", "TitleImage.png")) );

        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                switch (keyValue){
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
                        eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.GameStateEvent, 
                            this, "CHANGE_STATE","GAME_RUNNING", ""));
                    } else {
                        eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(GameEventType.GameStateEvent,
                            this, "CHANCE_STATE","CLOSE_WINDOW",""));
                    }

                    break;
                }
                
                    
            }
        }
    }
}