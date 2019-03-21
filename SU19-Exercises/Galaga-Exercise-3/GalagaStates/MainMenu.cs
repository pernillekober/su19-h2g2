using System.Net.Mime;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;


namespace GalagaGame.GalagaState {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;

        private Entity backGroundImage;
        private Text[] menubuttons;
        private int activeMenuButton;
        private int maxMenuButtons;

        public static GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }
    }
}