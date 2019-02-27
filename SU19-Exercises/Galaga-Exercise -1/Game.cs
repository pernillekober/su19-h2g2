using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Galaga_Exercise__1;

public class Game : IGameEventProcessor<object> {

    private Window win;
    private GameTimer gameTimer;
    private Galaga_Exercise__1.Player player;
    private GameEventBus<object> eventBus;

    public Game() {
        win = new Window("Galaga", 500, 500);
        gameTimer = new GameTimer(10, 60);
        player = new Player(this,
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(
                0.1f, 0.1f)), new Image(Path.Combine("Assets", "Images", "Player.png")));
        // For animated Image: new ImageStride()
        
        eventBus = new GameEventBus<object>();
        eventBus.InitializeEventBus(new List<GameEventType>() {
            GameEventType.InputEvent, // key press / key release
            GameEventType.WindowEvent, // messages to the window });
        });
        win.RegisterEventBus(eventBus);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        
    }

    public void GameLoop() {

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
            player.Direction(new Vec2F(0.03f,0.0f));
            break;
        case "KEY_LEFT":
            player.Direction(new Vec2F(-0.03f,0.0f));
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