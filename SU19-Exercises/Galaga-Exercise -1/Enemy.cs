
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise__1 {
    public class Enemy : Entity {
        private Game game;
        private List<Image> enemyStrides;
        private List<Enemy> enemies;
        
        public Enemy(Game game, DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            this.game = game;
            
            // Look at the file and consider why we place the number '4' here.
            enemyStrides = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
        }

        public void AddEnemies() {
            Enemy enemy = new Enemy(game,
                new DynamicShape(new Vec2F(0.45f, 0.9f), new Vec2F(
                0.1f, 0.1f)), new Image(Path.Combine("Assets", "Images", "Player.png")));
        }
    }
}