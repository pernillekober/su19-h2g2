using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Galaga_Exercise__1;

public class Game : IGameEventProcessor<object> {

    private Window win;
    private GameTimer gameTimer;
    private Player player;
    private Enemy enemy;
    private GameEventBus<object> eventBus;
    public List<Enemy> enemies;
    public List<Image> enemyStrides;
    public List<PlayerShot> playerShots { get; private set; }
    
   

    public Game() {
        win = new Window("Galaga", 500, 500);
        gameTimer = new GameTimer(60, 60);
        
        // Player Sprite
        player = new Player(this,
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(
                0.1f, 0.1f)), new Image(Path.Combine("Assets", "Images", "Player.png")));
        
        // Player Shots
        playerShots = new List<PlayerShot>();
        
        // Enemy Sprite
        // Look at the file and consider why we place the number '4' here.
        enemyStrides = ImageStride.CreateStrides(4,
            Path.Combine("Assets", "Images", "BlueMonster.png"));
        enemies = new List<Enemy>();

        // EventHandling
        eventBus = new GameEventBus<object>();
        eventBus.InitializeEventBus(new List<GameEventType>() {
            GameEventType.InputEvent, // key press / key release
            GameEventType.WindowEvent, // messages to the window });
        });
        win.RegisterEventBus(eventBus);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);   
    }

    public void AddEnemies(float x, float y) {
        enemy = new Enemy(this, new DynamicShape(new Vec2F(x,
                y), new Vec2F(0.1f, 0.1f), new Vec2F(0.0f, 0.0f)),
            new ImageStride(80, enemyStrides));
        enemies.Add(enemy);
    }
    public void IterateShots() {
        foreach (PlayerShot shot in playerShots) {
            shot.Shape.Move();
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            }

            foreach (Enemy enemy in enemies) {
                if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(),
                    enemy.Shape.AsDynamicShape()).Collision) {
                    enemy.DeleteEntity();
                    shot.DeleteEntity();
                }
            }
        List<Enemy> newEnemies = new List<Enemy>();
            foreach (Enemy enemy in enemies) {
                if (!enemy.IsDeleted()) {
                    newEnemies.Add(enemy);
                    
                } 
            }
            enemies = newEnemies;
            List<PlayerShot> newShots = new List<PlayerShot>();
            foreach (PlayerShot shots in playerShots) {
                if (!shot.IsDeleted()) {
                    newShots.Add(shots);
                }
                playerShots = newShots;
            }

        }
        
    }

    public void GameLoop() {
        AddEnemies(0.15f,0.7f);
        AddEnemies(0.25f,0.8f);
        AddEnemies(0.35f,0.7f);
        AddEnemies(0.45f,0.8f);
        AddEnemies(0.55f,0.7f);
        AddEnemies(0.65f,0.8f);
        AddEnemies(0.75f,0.7f);
        while (win.IsRunning()) {
            
            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {
                win.PollEvents();
                player.Move();
                eventBus.ProcessEvents();
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                player.RenderEntity();
                IterateShots();
                foreach (var enemy in enemies) {
                    enemy.RenderEntity();
                }
                foreach ( PlayerShot shot in playerShots) {
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

    private void KeyPress(string key) {
        switch (key) {
        case "KEY_ESCAPE":
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                    "", ""));
            break;
        case "KEY_RIGHT":
            player.Direction(new Vec2F(0.02f,0.0f));
            break;
        case "KEY_LEFT":
            player.Direction(new Vec2F(-0.02f,0.0f));
            break;
        case "KEY_SPACE":
            player.Shoot();
            Console.WriteLine(playerShots.Count);
            break;
        }
    }
    public void KeyRelease(string key) {
        switch (key) {
        case "KEY_LEFT":
            player.Direction(new Vec2F(0.0f,0.0f));;
            break;
        case "KEY_RIGHT":
            player.Direction(new Vec2F(0.0f,0.0f));
            break;
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
    }