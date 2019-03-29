using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Math;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;
using Galaga_Exercise_3.GalagaEntities.Enemy;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;
using Galaga_Exercise_3.GalagaGame;


namespace Galaga_Exercise_3.GalagaStates {
    public class GameRunning : IGameState {
        
        private static GameRunning instance = null;
        private Entity backGroundImage;
        private int explosionLength = 500;
        private AnimationContainer explosions;
        private Player player;
        private List<Image> explosionStrides;
        private Score scoreTable;
        public List<PlayerShot> playerShots { get; private set; }
        private List<Image> playerShotImage;
        private int i;
        // Enemy Image List
        private List<List<Image>> strideList;

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
        
        public GameRunning() {
            //Instanciate background image
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f),
                new Vec2F(1.0f,1.0f) ),new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))); 
            
            // Player Sprite
            player = new Player(this,
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(
                    0.05f, 0.05f)), new Image(Path.Combine("Assets", "Images", "Player.png")));
           
        
            //PlayerShot List
            playerShotImage = new List<Image>();
            playerShotImage.Add(new Image(Path.Combine("Assets", "Images", "BulletRed2.png")));

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
            
            // instatiate playerhshot
            playerShots = new List<PlayerShot>();
        }

        /// <summary>
        ///     A method which instantiates a projectile for the player.
        /// </summary>
        public void Shoot() {
            playerShots.Add(new PlayerShot(new DynamicShape(
                    new Vec2F(player.Entity.Shape.Position.X + .022f, 0.15f),
                    new Vec2F(0.005f, 0.027f)),
                playerShotImage[0]));
        }

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
       
        // Here we check if the enemies are hit by a shot, and if they are create an explosion, 
        // delete the entities involved, and add points to the scoreTable.
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
        
        // Spawns enemy formations(squadrons) and applies moveStrategies on the currently
        // instantiated squadron.
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
        
        //Singleton pattern to ensure only one instance of GameRunning object. If instance does not
        //exist the create a new instance.
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void GameLoop() { 
        }
        
        //Resets/deletes the instance. This is called when 'New Game'-button is selected in MainMenu
        //before registering the events, in order to create new instantiation of GameRunning. 
        public void InitializeGameState() {
            GameRunning.instance = null;
        }
        
        // Updates the variables used by GameRunning. 
        public void UpdateGameLogic() {
            IterateShots();
            SpawnEnemies();
            player.Move();
        }

        public void RenderState() {
            //Renders all instantiations related to GameRunning. Lists rendered with foreach-loops.
            backGroundImage.RenderEntity();
            player.Entity.RenderEntity();
            scoreTable.RenderScore();
            explosions.RenderAnimations();
            foreach (ISquadron squadron in monsterList) {
                squadron.Enemies.RenderEntities();
                   
            }
            foreach (var shot in playerShots) {
                shot.RenderEntity();
            }
        }
        /// <summary>
        /// HandleKeyEvent evaluates KeyAction to be either "KEY_PRESS" or "KEY_RELEASE" and calls
        /// corresponding method KeyPress or KeyRelease with KeyValue.
        /// If HandleKeyEvent evaluates KeyAction to be "KEY_PRESS" it calls KeyPress with KeyValue.
        /// </summary>
        /// <param name="keyValue">string with value of key that triggered event</param>
        /// <param name="keyAction">type of InputEvent. Either KEY_PRESS or KEY_RELEASE</param>        
        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                KeyPress(keyValue);
            } else if (keyAction == "KEY_RELEASE") {
                KeyRelease(keyValue);
            }
        }
        /// <summary>
        /// Performs the actions in the menu according to the KeyValue. Either navigating the
        /// menubuttons or selecting the activeMenuButton and register GameStateEvent to eventBus
        /// with relevant gameEvent.message. 
        /// </summary>
        /// <param name="KeyValue">string with value of key that triggered event</param>
        public void KeyPress(string KeyValue) {
            switch (KeyValue) {
            case "KEY_ESCAPE":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                        "", ""));
                break;
            case "KEY_P":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent, this, "CHANGE_STATE",
                        "GAME_PAUSED", ""));
                break;
            case "KEY_RIGHT":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_RIGHT",
                        "", ""));
                break;
            case "KEY_LEFT":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "KEY_LEFT",
                        "", ""));
                break;
            case "KEY_SPACE":
                Shoot();
                break;
            }
        }
        
        /// <summary>
        /// Performs the actions in the menu according to the KeyValue. Either navigating the
        /// menubuttons or selecting the activeMenuButton and register GameStateEvent to eventBus
        /// with relevant gameEvent.message. 
        /// </summary>
        /// <param name="KeyValue">string with value of key that triggered event</param>
        public void KeyRelease(string KeyValue) {
            switch (KeyValue) {
            case "KEY_LEFT":
                GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.PlayerEvent, this, "STOP",
                    "", ""));
                    break;
            case "KEY_RIGHT":
                GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                GameEventType.PlayerEvent, this, "STOP",
                "", ""));
                break;
            }
        }
    }
}