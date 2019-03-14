using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using Galaga_Exercise_2;

public class Game : IGameEventProcessor<object> {
    public List<Enemy> enemies;
    private Enemy enemy;
    public List<Image> enemyStrides;
    
    private List<Image> explosionStrides;
    private AnimationContainer explosions;
    
    private GameEventBus<object> eventBus;
    private GameTimer gameTimer;
    private Player player;
    private Window win;
    private int explosionLength = 500;
    private Score scoreTable;
    public List<PlayerShot> playerShots { get; private set; }

    public Game() {
        win = new Window("Galaga", 500, 500);
        gameTimer = new GameTimer(60, 60);
        playerShots = new List<PlayerShot>();

        // Player Sprite
        player = new Player(this,
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(
                0.1f, 0.1f)), new Image(Path.Combine("Assets", "Images", "Player.png"))); 

        // Enemy Image List
        enemyStrides = ImageStride.CreateStrides(4,
            Path.Combine("Assets", "Images", "BlueMonster.png"));
        enemies = new List<Enemy>();
        
        // Enemy Explosion
        explosionStrides = ImageStride.CreateStrides(8,
            Path.Combine("Assets", "Images", "Explosion.png"));
        explosions = new AnimationContainer(28);
        
        // ScoreTable
        scoreTable = new Score(new Vec2F(0.1f,0.62f), 
            new Vec2F(0.35f,0.35f));

        // EventHandling
        eventBus = new GameEventBus<object>();
        eventBus.InitializeEventBus(new List<GameEventType> {
            GameEventType.InputEvent, // key press / key release
            GameEventType.WindowEvent // messages to the window });
        });
        win.RegisterEventBus(eventBus);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
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
                player.KeyPress(gameEvent.Message);
                break;
            case "KEY_RELEASE":
                KeyRelease(gameEvent.Message);
                   break;
            }
        }
    }
    /// <summary>
    /// Adds an enemy at given position.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void AddEnemies() {
        for (float i = 0.15f; i <= 0.85f; i += .1f) {
            enemy = new Enemy(this, new DynamicShape(new Vec2F(i,
                .7f), new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.0f)),
                new ImageStride(80, enemyStrides));
            enemies.Add(enemy);
            enemy = new Enemy(this, new DynamicShape(new Vec2F(i,
                    .6f), new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.0f)),
                new ImageStride(80, enemyStrides));
            enemies.Add(enemy);
        }
        
        
    }
    /// <summary>
    /// Adds an explosion animation at given position.
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
    /// Makes the shots move and deletes shots if outside window.
    /// </summary>
    public void IterateShots() {
        foreach (var shot in playerShots) {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            }

            foreach (var enemy in enemies) {
                if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(),
                    enemy.Shape.AsDynamicShape()).Collision) {
                    enemy.DeleteEntity();
                    AddExplosion(enemy.Shape.Position.X,enemy.Shape.Position.Y, 
                        enemy.Shape.Extent.X,enemy.Shape.Extent.Y);
                    shot.DeleteEntity();
                    scoreTable.AddPoint();
                    
                }
            }

            var newEnemies = new List<Enemy>();
            foreach (var enemy in enemies) {
                if (!enemy.IsDeleted()) {
                    newEnemies.Add(enemy);
                }
            }

            enemies = newEnemies;
            var newShots = new List<PlayerShot>();
            foreach (var playerShot in playerShots) {
                if (!playerShot.IsDeleted()) {
                    newShots.Add(playerShot);
                }
            }
            playerShots = newShots;
        }
    }

    public void GameLoop() {
        AddEnemies();
        while (win.IsRunning()) {
            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {
                win.PollEvents();
                player.Move();
                player.AddBoost();
                IterateShots();
                eventBus.ProcessEvents();
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                player.RenderEntity();
                player.booster.RenderEntity();
                scoreTable.RenderScore();
                
                foreach (var enemy in enemies) {
                    explosions.RenderAnimations();
                    enemy.RenderEntity();
                }

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

    public void KeyRelease(string key) {
        switch (key) {
        case "KEY_LEFT":
            player.Direction(new Vec2F(0.0f, 0.0f));
            break;
        case "KEY_RIGHT":
            player.Direction(new Vec2F(0.0f, 0.0f));
            break;
        }
        
    }
}