using DIKUArcade.Entities;
using Galaga_Exercise_3.GalagaEntities.Enemy;

namespace Galaga_Exercise_3.MovementStrategy {
    public interface IMovementStrategy {
        void MoveEnemy(Enemy enemy);
        void MoveEnemies(EntityContainer<Enemy> enemies);
    }
}
