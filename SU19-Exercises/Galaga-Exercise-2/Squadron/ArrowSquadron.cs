using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEntities.Enemy;

namespace Galaga_Exercise_2.Squadrons {
    public class ArrowSquadron : ISquadron {

        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }

        public ArrowSquadron(int Max) {
            MaxEnemies = Max;
            Enemies = new EntityContainer<Enemy>();
        }


        public void CreateEnemies(List<Image> enemyStrides) {

            for (var i = 0.0f; i <= 0.4f; i += .06f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F(i + 0.05f, 0.8f - i),
                        new Vec2F(.06f, 0.06f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }

            for (var i = 0.4f; i >= 0.0f; i -= .06f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F(i + 0.45f, 0.4f + i),
                        new Vec2F(0.06f, 0.06f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }

            for (var i = 0.0f; i <= 0.24f; i += .06f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F(i + 0.21f, 0.8f - i),
                        new Vec2F(.06f, 0.06f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }
            for (var i = 0.24f; i >= 0.0f; i -= .06f) {
                var enemy = new Enemy(new DynamicShape(new Vec2F(i + 0.45f, 0.56f + i),
                        new Vec2F(0.06f, 0.06f), new Vec2F(0.1f, 0.0f)),
                    new ImageStride(80, enemyStrides));
                Enemies.AddDynamicEntity(enemy);
            }
        }
    }
}