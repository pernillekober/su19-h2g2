using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities.Enemy;

namespace Galaga_Exercise_2.Squadrons {
    public class ZigZagSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies { get; } 
        public int MaxEnemies { get; }

        public ZigZagSquadron(int Max) {
            MaxEnemies = Max;
            Enemies = new EntityContainer<Enemy>();
        }

        public void CreateEnemies(List<Image> enemyStrides) {
            for (var i = 0.05f; i <= 0.85f; i += .2f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F(i, .8f),
                        new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }

            for (var i = 0.15f; i <= 0.75f; i += .2f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F(i, .7f),
                        new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }

            for (var i = 0.05f; i <= 0.85f; i += .2f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F(i, .6f),
                        new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }
        }
    }
}