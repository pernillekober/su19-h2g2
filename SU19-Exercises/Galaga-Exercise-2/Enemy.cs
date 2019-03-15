using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using OpenTK.Input;

// mangler namespace
namespace Galaga_Exercise_2.GalagaEntities.Enemy {
    public class Enemy : Entity {
        private Game game;
        private Vec2F vec2F { get; }

        public Enemy(Game game, DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            this.game = game;
            Vec2F position = new Vec2F(0.0f,0.0f); 
            
        }
    }
}
<<<<<<< HEAD

namespace Galaga_Exercise_2.GalagaEntities.Squadrons {
    public interface ISquadron {
        EntityContainer<Galaga_Exercise_2.GalagaEntities.Enemy.Enemy> Enemies { get; }
        int MaxEnemies { get; }
        void CreateEnemies(List<Image> enemyStrides);
    }
}
=======
>>>>>>> 9839b08a2391c5df71cf5b8d94d1a85bebdbb154
