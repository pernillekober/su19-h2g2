using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities.Enemy;

namespace Galaga_Exercise_3.Squadrons {
    public class ZigZagSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies { get; } 
        public int MaxEnemies { get; }

        public ZigZagSquadron(int Max) {
            MaxEnemies = Max;
            Enemies = new EntityContainer<Enemy>();
        }

        public void CreateEnemies(List<Image> enemyStrides) {
            for (var i = 0.0f; i <= .9f; i += 0.1f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F( i + .05f, .8f),
                        new Vec2F(0.06f, 0.06f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }

            for (var i = .0f; i <= .8f; i += .1f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F(i + .1f, .7f),
                        new Vec2F(0.06f, 0.06f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }

            for (var i = .0f; i <= .9f; i += .1f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F(i + .05f, .6f),
                        new Vec2F(0.06f, 0.06f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }
        }
    }
}