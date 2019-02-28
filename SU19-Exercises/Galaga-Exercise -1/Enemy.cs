
using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise__1 {
    public class Enemy : Entity {
        private Game game;
        
        public Enemy(Game game, DynamicShape shape, IBaseImage image) :
            base(shape, image) {
            this.game = game;
        }
    }
}