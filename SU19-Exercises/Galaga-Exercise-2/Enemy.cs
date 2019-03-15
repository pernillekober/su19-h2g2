using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

// mangler namespace
namespace Galaga_Exercise_2.GalagaEntities.Enemy {
    public class Enemy : Entity {
        private Game game;
        private Vec2F vec2F { get; }

        public Enemy(Game game, DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            this.game = game;
        }
    }
}