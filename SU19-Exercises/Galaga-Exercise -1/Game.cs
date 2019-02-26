using System;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Galaga_Exercise__1;

public class Game : IGameEventProcessor <object> {
    
    private Window win;
    private GameTimer gameTimer;
    private Galaga_Exercise__1.Player player;
    public Game() {
        win = new Window("Galaga", 500, 500);
        gameTimer = new GameTimer(10 , 60);
        player = new Player(this,
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
            // For animated Image: new ImageStride()
    }
    
    public void GameLoop() {

        while (win.IsRunning()) {

            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {

                win.PollEvents();
                player.Move();
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
    public void KeyPress(string key) {
        throw new NotSupportedException();
    }
    public void KeyRelease(string key) {
        throw new NotSupportedException();
    }
    public void ProcessEvent(GameEventType eventType, GameEvent <object> gameEvent) {
        throw new NotSupportedException();
    }
}