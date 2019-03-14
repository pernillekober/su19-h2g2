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
    
    // mangler namespace
    public interface ISquadron {
        EntityContainer<Enemy> Enemies { get; }
        int MaxEnemies { get; }

        void CreateEnemies(List<Image> enemyStrides);
    }
}