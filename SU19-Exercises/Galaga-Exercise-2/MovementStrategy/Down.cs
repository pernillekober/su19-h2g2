using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities.Enemy;
using Galaga_Exercise_2.Squadrons;

namespace Galaga_Exercise_2.MovementStrategy {
    public class Down : IMovementStrategy {
        public EntityContainer<Enemy> Enemies { get; }

        /*public Down() {
            Enemies = new EntityContainer<Enemy>();
        }*/
        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.AsDynamicShape().Direction = 
                new Vec2F(0.0f , enemy.startPos.Y -0.01f); 
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(MoveEnemy);
            }
        }
    }