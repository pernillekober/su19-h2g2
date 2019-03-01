using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise__1 {
    public class Player : Entity {
        private Game game;
        private List<Image> playerShots;
        public List<Image> playerBooster;
        public Entity booster ;

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
        /// </summary>
        public void Move() {
            Shape.Move();
            if (Shape.Position.X + Shape.AsDynamicShape().Direction.X < -.02f) {
                Shape.Position.X = -.02f;
            } else if (Shape.Position.X + Shape.AsDynamicShape().Direction.X > .92f) {
                Shape.Position.X = .92f;
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