using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Math;
using System.IO;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using GalagaGame.GalagaState;
using Galaga_Exercise_3;
using Galaga_Exercise_3.GalagaEntities.Enemy;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;


namespace Galaga_Exercise_3.GalagaStates {
    public class GameRunning : IGameState {
        
        private static GameRunning instance = null;
        
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        
        private int explosionLength = 500;
        private AnimationContainer explosions;
        private Player player;
        private List<Image> enemyStrides;
        private List<Image> greenEnemies;
        private List<Image> redEnemies;
        private List<Image> explosionStrides;
        private Score scoreTable;
       

        // Instantiate Squadrons
        private List<ISquadron> monsterList = new List<ISquadron>() {
            new ZigZagSquadron(15),
            new ArrowSquadron(20),
            new WallSquadron(30)
        };
        // Instantiate list of movementstrategies
        private List<IMovementStrategy> movesStrategies = new List<IMovementStrategy>() {
            new ZigZagMove(),
            new NoMove(),
            new Down()
        };
    
        private int i = 0;
        // Enemy Image List
        private List<List<Image>> strideList;
        
        public List<PlayerShot> playerShots { get; private set; }
    
        // Adding movement
        private IMovementStrategy down;
        private IMovementStrategy ZigZag;
        private IMovementStrategy NoMove;
        
        /// <summary>
        ///     Adds an explosion animation at given position.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="extentX"></param>
        /// <param name="extentY"></param>
        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }

        /// <summary>
        ///     Here we check if the enemies are hit by a shot, and if they are we delete them.
        /// </summary>

        public void IterateShots() {
            var newShots = new List<PlayerShot>();
            foreach (var shot in playerShots) {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                } else {
                    foreach (ISquadron squadron in monsterList) {
                        squadron.Enemies.Iterate(delegate(Enemy enemy) {
                            if (CollisionDetection.Aabb((DynamicShape)
                                shot.Shape, enemy.Shape).Collision) {
                                AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y,
                                    enemy.Shape.Extent.X, enemy.Shape.Extent.Y);
                                enemy.DeleteEntity();
                                shot.DeleteEntity();
                                scoreTable.AddPoint();
                            }
                        });
                    }
                }
                if (!shot.IsDeleted()) {
                    newShots.Add(shot);
                }
                playerShots = newShots;
            }
        }
        public void SpawnEnemies() {
            if (monsterList[i].Enemies.CountEntities() == 0) {
                monsterList[i].Enemies.ClearContainer();

                playerShots.Clear();
                i++;
                if (monsterList.Count - 1 < i) {
                    i = 0;
                }
                monsterList[i].CreateEnemies(strideList[i]);
            }
            movesStrategies[i].MoveEnemies(monsterList[i].Enemies);
        }
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }


        public GameRunning() {
            
            // ScoreTable
            scoreTable = new Score(new Vec2F(0.1f, 0.62f),
                new Vec2F(0.35f, 0.35f));
            // Enemy movement Strategies
            
            strideList = new List<List<Image>> {
                ImageStride.CreateStrides(4,
                    Path.Combine("Assets", "Images", "BlueMonster.png")),
                ImageStride.CreateStrides(2,
                    Path.Combine("Assets", "Images", "RedMonster.png")),
                ImageStride.CreateStrides(2,
                    Path.Combine("Assets", "Images", "GreenMonster.png"))
            };

            // Enemy Explosion
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(28);
            
            playerShots = new List<PlayerShot>();
        }
        public void GameLoop() {
            player.Move();
            player.AddBoost();
            IterateShots();
            SpawnEnemies();
        }

        public void InitializeGameState() {
            throw new System.NotImplementedException();
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }

        public void RenderState() {
            backGroundImage = new Entity(new StationaryShape(new Vec2F(),
                new Vec2F() ),new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))); 
            backGroundImage.RenderEntity();
            player.Entity.RenderEntity();
            player.booster.RenderEntity();
            scoreTable.RenderScore();
            explosions.RenderAnimations();
            foreach (ISquadron squadron in monsterList) {
                squadron.Enemies.RenderEntities();
                   
            }
            foreach (var shot in playerShots) {
                shot.RenderEntity();
            }
        }
        public void HandleKeyEvent(string keyValue, string keyAction) {
            throw new System.NotImplementedException();
        }
    }
}