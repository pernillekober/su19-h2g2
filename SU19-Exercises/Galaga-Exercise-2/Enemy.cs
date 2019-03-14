using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

// mangler namespace
namespace Galaga_Exercise_2 {
    public class Enemy : Entity {
        private Game game;

        public Enemy(Game game, DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            this.game = game;
        }
    }
<<<<<<< HEAD
    
    // mangler namespace
=======
}
/*
namespace Galaga_Exercise_2.Squadrons {
>>>>>>> f94a39fb3ef96a1f0ab04b7460e9ca181e07f595
    public interface ISquadron {
        EntityContainer<Enemy> Enemies { get; }
        int MaxEnemies { get; }

        void CreateEnemies(List<Image> enemyStrides);
    }
<<<<<<< HEAD
}
=======
}*/
>>>>>>> f94a39fb3ef96a1f0ab04b7460e9ca181e07f595
