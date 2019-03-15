using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2 {
    public class Player : IGameEventProcessor<object> {
        public Entity Entity { get; private set; }
        private Game game;
        private List<Image> playerShots;
        private List<Image> playerBooster;
        public Entity booster ;
        private GameEventBus<object> eventBus;

        public Player(Game game, DynamicShape shape, IBaseImage image) {
            Entity = new Entity(shape, image);
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
        private void Direction(Vec2F dir) {
            Entity.Shape.AsDynamicShape().Direction = dir;

        }

        /// <summary>
        /// Moves a shape property if inside the window.
        /// </summary>5
        public void Move() {
            if (Entity.Shape.Position.X + Entity.Shape.AsDynamicShape().Direction.X > 
                (0.0f - Entity.Shape.Extent.X/3) &&
                Entity.Shape.Position.X + Entity.Shape.AsDynamicShape().Direction.X < 
                (1.0f - 2 * Entity.Shape.Extent.X/3)) {
                Entity.Shape.Move();
            }
        }
        /// <summary>
        /// A method which instantiates a projectile for the player.
        /// </summary>
        private void Shoot() {
            game.playerShots.Add(new PlayerShot(game, new DynamicShape(
                    new Vec2F(Entity.Shape.Position.X + .047f, 0.2f),
                    new Vec2F(0.008f, 0.027f)),
                playerShots[0]));
        }
        /// <summary>
        /// Adds boosters to the space ship (visual effect).
        /// </summary>
        public void AddBoost() {
            booster = new Entity( new DynamicShape(new Vec2F(Entity.Shape.Position.X, 
                    Entity.Shape.Position.Y-0.015f), new Vec2F(0.1f,0.1f)),
                playerBooster[0]);
        }
        // Event handling for the player
        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                case "KEY_RIGHT":
                    Direction(new Vec2F(0.013f, 0.0f));
                    break;
                case "KEY_LEFT":
                    Direction(new Vec2F(-0.013f, 0.0f));
                    break;
                case "STOP":
                    Direction(new Vec2F(0.0f, 0.0f));
                    break;
                case "KEY_SPACE":
                    Shoot();
                    break;
                }
            }
        }
    }
}