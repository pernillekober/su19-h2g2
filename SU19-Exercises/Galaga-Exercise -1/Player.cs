using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise__1 {
    public class Player : Entity {
        private Game game;
        private List<Image> shotStrides;

        public Player(Game game, DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            this.game = game;

            shotStrides = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "Bulletred2.png"));
        }

        public void Direction(Vec2F dir) {
            Shape.AsDynamicShape().Direction = dir;
        }

        public void Move() {
            Shape.Move();
            if (Shape.Position.X + Shape.AsDynamicShape().Direction.X < -.02f) {
                Shape.Position.X = -.02f;
            } else if (Shape.Position.X + Shape.AsDynamicShape().Direction.X > .92f) {
                Shape.Position.X = .92f;
            }
        }

        // A method which instantiates a projectile for the player
        public void Shoot() {
            game.playerShots.Add(new PlayerShot(game, new DynamicShape(
                    new Vec2F(Shape.Position.X + .047f, 0.2f),
                    new Vec2F(0.008f, 0.027f)),
                new Image(Path.Combine("Assets", "Images", "BulletRed2.png"))));
        }
    }
}