using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaGame;
using Galaga_Exercise_3.GalagaStates;


namespace Galaga_Exercise_3 {
    public class Player : IGameEventProcessor<object> {
        public Entity booster;
        private List<Image> playerBooster;
        

        public Player(GameRunning game, DynamicShape shape, IBaseImage image) {
            Entity = new Entity(shape, image);
            playerBooster = new List<Image>();
        }

        public Entity Entity { get; }

        // Event handling for the player
        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                case "KEY_RIGHT":
                    Direction(new Vec2F(0.013f, 0.0f));
                    break;
                case "KEY_LEFT":
                    Direction(new Vec2F(-0.013f, 0.0f));
                    break;
                case "STOP":
                    Direction(new Vec2F(0.0f, 0.0f));
                    break;
                }
            }
        }

        /// <summary>
        ///     Updates the direction of a shape.
        /// </summary>
        /// <param name="dir"></param>
        private void Direction(Vec2F dir) {
            Entity.Shape.AsDynamicShape().Direction = dir;
        }

        /// <summary>
        ///     Moves a shape property if inside the window.
        /// </summary>
        /// 5
        public void Move() {
            if (Entity.Shape.Position.X + Entity.Shape.AsDynamicShape().Direction.X >
                0.0f - Entity.Shape.Extent.X / 3 &&
                Entity.Shape.Position.X + Entity.Shape.AsDynamicShape().Direction.X <
                1.0f - 2 * Entity.Shape.Extent.X / 3) {
                Entity.Shape.Move();
            }
        }

        /// <summary>
        ///     Adds boosters to the space ship (visual effect).
        /// </summary>
        public void AddBoost() {
            booster = new Entity(new DynamicShape(new Vec2F(Entity.Shape.Position.X,
                    Entity.Shape.Position.Y - 0.007f), 
                    new Vec2F(Entity.Shape.Extent.X,Entity.Shape.Extent.Y)),
                playerBooster[0]);
        }
    }
}