using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise__1 {
    public class Player : Entity {
        private Game game;
        public Player(Game game, DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            this.game = game;
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
                 PlayerShot shot = new PlayerShot(game, new DynamicShape(new Vec2F(0.0f, 
                        0.01f),new Vec2F(0.008f,0.027f), new Vec2F(0.0f,0.01f)),
                    new ImageStride(100,Path.Combine("Assets", "Images", "BulletRed2.png")));
        }
            
        
    }
}