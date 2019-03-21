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
using Galaga_Exercise_3.GalagaGame;
using Galaga_Exercise_3.GalagaStateType;

public class Game : IGameEventProcessor<object> {
    

    private List<Image> enemyStrides;
    private List<Image> greenEnemies;
    private List<Image> redEnemies;
    private List<Image> explosionStrides;

    // Instantiate Squadrons
    private List<ISquadron> monsterList = new List<ISquadron>() {
        new ArrowSquadron(20),
        new ZigZagSquadron(15),
        new WallSquadron(30)
    };
    
    // Enemy Image List
    private List<List<Image>> strideList;
    
    // Adding movement
    private IMovementStrategy down;
    
    private int i = 0;
    private int explosionLength = 500;
    private AnimationContainer explosions;
    private Player player;


    private GameTimer gameTimer;
    private Score scoreTable;

    private Window win;
    public StateMachine StateMachine;
    
    public Game() {
        win = new Window("Galaga", 500, 500);
        gameTimer = new GameTimer(60, 60);
        playerShots = new List<PlayerShot>();

        // Player Sprite
        player = new Player(this,
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(
                0.05f, 0.05f)), new Image(Path.Combine("Assets", "Images", "Player.png")));

        strideList = new List<List<Image>> {
            ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png")),
            ImageStride.CreateStrides(2,
                Path.Combine("Assets", "Images", "RedMonster.png")),
            ImageStride.CreateStrides(2,
                Path.Combine("Assets", "Images", "GreenMonster.png"))
        };

        //Initializing Movement Staretegies
        down = new Down();
        
        // Initializing statemachine
        StateMachine = new StateMachine();
        
        
        // Enemy Explosion
        explosionStrides = ImageStride.CreateStrides(8,
            Path.Combine("Assets", "Images", "Explosion.png"));
        explosions = new AnimationContainer(28);

        // ScoreTable
        scoreTable = new Score(new Vec2F(0.1f, 0.62f),
            new Vec2F(0.35f, 0.35f));
        // Enemy movement Strategies

        
        // EventHandling
        //eventBus = new GameEventBus<object>();
        GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> {
            GameEventType.InputEvent, // key press / key release
            GameEventType.WindowEvent, // messages to the window });
            GameEventType.PlayerEvent // player event
        });
        win.RegisterEventBus(GalagaBus.GetBus());
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
    }

    public List<PlayerShot> playerShots { get; private set; }



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
            if (monsterList.Count-1 < i) {
                i = 0;
            }
            monsterList[i].CreateEnemies(strideList[i]);
        }
    }


    public void GameLoop() {
        
        while (win.IsRunning()) {
            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {
                win.PollEvents();
                player.Move();
                player.AddBoost();
                IterateShots();
                GalagaBus.GetBus().ProcessEvents();
                
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                player.Entity.RenderEntity();
                player.booster.RenderEntity();
                scoreTable.RenderScore();
                SpawnEnemies();
                
                foreach (ISquadron squadron in monsterList) {
                   squadron.Enemies.RenderEntities();
                   
                }
                
                explosions.RenderAnimations();

                foreach (var shot in playerShots) {
                    shot.RenderEntity();
                }

                win.SwapBuffers();
            }

            if (gameTimer.ShouldReset()) {
                // 1 second has passed - display last captured ups and fps win.Title = "Galaga | UPS: "
                // + gameTimer.CapturedUpdates + ", FPS: " + gameTimer.CapturedFrames;
            }
        }
    }

    public void ProcessEvent(GameEventType eventType,
        GameEvent<object> gameEvent) {
        if (eventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
            case "CLOSE_WINDOW":
                win.CloseWindow();
                break;
            }
        } else if (eventType == GameEventType.InputEvent) {
            switch (gameEvent.Parameter1) {
            case "KEY_PRESS":
                KeyPress(gameEvent.Message);
                break;
            case "KEY_RELEASE":
                KeyRelease(gameEvent.Message);
                break;
            }
        }
    }
    public void KeyPress(string key) {
        switch (key) {
        case "KEY_ESCAPE":
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                    "", ""));
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
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.PlayerEvent, this, "KEY_SPACE",
                    "", ""));
            break;
        }
    }

    public void KeyRelease(string key) {
        switch (key) {
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
