using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;

public class Game : IGameEventProcessor <object> {
    
    private Window win;
    private GameTimer gameTimer;
    public Game() {
        win = new Window("Galaga", 500, 500);
        gameTimer = new GameTimer(10 , 60);
    }
    
    public void GameLoop() {

        while (win.IsRunning()) {

            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {

                win.PollEvents();
                // Update game logic here
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                // Render gameplay entities here
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