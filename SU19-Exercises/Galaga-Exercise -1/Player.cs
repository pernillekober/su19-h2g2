using System;
using DIKUArcade;
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
            Console.WriteLine((Shape.Position.X + Shape.AsDynamicShape().Direction.X));
            if (Shape.Position.X + Shape.AsDynamicShape().Direction.X < .9f) {
                Shape.MoveX(0.01f);
            }

        }
    }
}