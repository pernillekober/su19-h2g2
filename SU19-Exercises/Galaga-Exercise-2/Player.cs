using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2 {
    public class Player : Entity {
        private Game game;
        private List<Image> playerShots;
        public List<Image> playerBooster;
        public Entity booster ;
        public GameEventBus<object> eventBus;

        public Player(Game game, DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            this.game = game;
            playerShots = new List<Image>();
            playerShots.Add(new Image(Path.Combine("Assets", "Images","BulletRed2.png")));
            playerBooster = new List<Image>();
            playerBooster.Add(new Image(Path.Combine("Assets", "Images","PlayerBooster.png")));
            
        }
        /// <summary>
        /// Updates the direction of a shape.
        /// </summary>
        /// <param name="dir"></param>
        public void Direction(Vec2F dir) {
            Shape.AsDynamicShape().Direction = dir;

        }

        /// <summary>
        /// Moves a shape property if inside the window.
        /// </summary>5
        public void Move() {
            if (Shape.Position.X + Shape.AsDynamicShape().Direction.X > (0.0f - Shape.Extent.X/3) &&
                Shape.Position.X + Shape.AsDynamicShape().Direction.X < (1.0f - 2 * Shape.Extent.X/3)) {
                Shape.Move();
            }
        }
    public void KeyPress(string key) {
        switch (key) {
        case "KEY_ESCAPE":
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                    "", ""));
            break;
        case "KEY_RIGHT":
            Direction(new Vec2F(0.013f, 0.0f));
            break;
        case "KEY_LEFT":
            Direction(new Vec2F(-0.013f, 0.0f));
            break;
        case "KEY_SPACE":
            Shoot();
            break;
        }
    }
    
    public void KeyRelease(string key) {
        switch (key) {
        case "KEY_LEFT":
            Direction(new Vec2F(0.0f, 0.0f));
            break;
        case "KEY_RIGHT":
            Direction(new Vec2F(0.0f, 0.0f));
            break;
        }
        
    }
        /// <summary>
        /// A method which instantiates a projectile for the player.
        /// </summary>
        public void Shoot() {
            game.playerShots.Add(new PlayerShot(game, new DynamicShape(
                    new Vec2F(Shape.Position.X + .047f, 0.2f),
                    new Vec2F(0.008f, 0.027f)),
                playerShots[0]));
        }
        /// <summary>
        /// Adds boosters to the space ship (visual effect).
        /// </summary>
        public void AddBoost() {
            booster = new Entity( new DynamicShape(new Vec2F(Shape.Position.X, 
                    Shape.Position.Y-0.015f), new Vec2F(0.1f,0.1f)),
                playerBooster[0]);
        }
    }
}