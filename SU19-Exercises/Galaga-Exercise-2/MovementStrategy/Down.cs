using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities.Enemy;

namespace Galaga_Exercise_2.MovementStrategy {
    public class Down : IMovementStrategy{

        public Down() {
            void MoveEnemy(Enemy enemy) {
                enemy.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, -0.01f);
            }

            void MoveEnemies(EntityContainer<Enemy> enemies) {
                foreach (var enemy in enemies) {
                    MoveEnemy(enemy);
                    
                }
                
            }
        }        
    }
}