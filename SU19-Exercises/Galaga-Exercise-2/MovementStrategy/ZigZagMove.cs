using DIKUArcade.Entities;
using Galaga_Exercise_2.GalagaEntities.Enemy;

namespace Galaga_Exercise_2.MovementStrategy {
    public class ZigZagMove : IMovementStrategy {
        void MoveEnemy(Enemy enemy);
        void MoveEnemies(EntityContainer<Enemy> enemies);
    }
}