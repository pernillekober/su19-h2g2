
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise__1 {
    public class PlayerShot : Entity {
        private Game game;
        public PlayerShot(Game game, DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            this.game = game;
        }
        
        public void Direction(Vec2F dir) {
            Shape.AsDynamicShape().Direction = dir;
        }
    }
}