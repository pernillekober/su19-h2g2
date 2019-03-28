
using System;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;
using Galaga_Exercise_3.GalagaStates;
using Galaga_Exercise_3.GalagaGame;



public class Game : IGameEventProcessor<object> {
    
    private GameTimer gameTimer;
    private GameEventBus<object> eventBus;
    private Window win;
    private StateMachine stateMachine;
    

    public Game() {
        win = new Window("Galaga", 500, 500);
        gameTimer = new GameTimer(60, 60);
        
        
        // EventHandling
        eventBus = GalagaBus.GetBus();
        eventBus.InitializeEventBus(new List<GameEventType> {
            GameEventType.InputEvent, // key press / key release
            GameEventType.WindowEvent, // messages to the window });
            GameEventType.PlayerEvent, // player event
            GameEventType.GameStateEvent
        });
        win.RegisterEventBus(eventBus);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        stateMachine = new StateMachine();
    }
    
    
    public void GameLoop() {

        while (win.IsRunning()) {
            gameTimer.MeasureTime();
            while (gameTimer.ShouldUpdate()) {
                win.PollEvents();
                GalagaBus.GetBus().ProcessEvents();
                stateMachine.ActiveState.UpdateGameLogic();
            }

            if (gameTimer.ShouldRender()) {
                win.Clear();
                stateMachine.ActiveState.RenderState();
                win.SwapBuffers();
            }

            if (gameTimer.ShouldReset()) {
                // 1 second has passed - display last captured ups and fps win.Title = "Galaga | UPS: "
                // + gameTimer.CapturedUpdates + ", FPS: " + gameTimer.CapturedFrames;
            }
        }
    }
    
    // Checker for a windowevent. The decision of placing it in Game.cs is because it is directly
    // related to win, and hence Game.  
    public void ProcessEvent(GameEventType eventType,
        GameEvent<object> gameEvent) {
        switch (eventType) {
        case GameEventType.WindowEvent:
            switch (gameEvent.Message) {
            case "CLOSE_WINDOW":
                win.CloseWindow();
                break;
            }

            break;
        }
    }
}
