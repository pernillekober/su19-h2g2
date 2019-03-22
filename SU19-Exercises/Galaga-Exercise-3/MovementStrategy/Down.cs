using DIKUArcade.Entities;
using Galaga_Exercise_3.GalagaEntities.Enemy;


namespace Galaga_Exercise_3.MovementStrategy {
    public class Down : IMovementStrategy {
        public EntityContainer<Enemy> Enemies { get; }

        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.MoveY(-0.0003f);
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}