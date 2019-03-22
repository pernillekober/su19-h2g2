using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using DIKUArcade.Entities;
using Galaga_Exercise_2.GalagaEntities.Enemy;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities.Enemy;
using Galaga_Exercise_2.Squadrons;

namespace Galaga_Exercise_2.MovementStrategy {
    public class ZigZagMove : IMovementStrategy {
        public EntityContainer<Enemy> Enemies { get; }
        
        
        /// <summary>
        /// Makes an enemy move in a zigzag pattern with the given parametres of s,p and a. 
        /// </summary>
        /// <param name="enemy">the enemy entity/shape to move</param>
        public void MoveEnemy(Enemy enemy) {
            var s = -0.0003f;     // speed of downwards movement
            var p = 0.045f;       // period of sine wave
            var a = 0.05f;        //amplitude
            
            
            //calculate enemy position
            var currentY = enemy.Shape.Position.Y + s;
            var currentX = enemy.startPos.X + a * (float) Math.Sin(2 * Math.PI * (enemy.startPos.Y - currentY)/p);
            
            
            // change enemy position
            enemy.Shape.Position.X = currentX;
            enemy.Shape.Position.Y = currentY;
        }
        
        /// <summary>
        /// Calls MoveEnemy on each of the entities in the enemy entity cantainer.
        /// </summary>
        /// <param name="enemies">EntityContainer of enemy entities to move.</param>
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}