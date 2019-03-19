using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

// mangler namespace
namespace Galaga_Exercise_2.GalagaEntities.Enemy {
    public class Enemy : Entity {
        
        public Enemy(DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            startPos = shape.Position;
            new Entity(shape, image);
        }

        private Vec2F startPos { get; }
    }
}