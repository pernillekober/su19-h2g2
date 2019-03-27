using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using GalagaGame.GalagaState;
using Galaga_Exercise_3;
using Galaga_Exercise_3.GalagaGame;
using Galaga_Exercise_3.GalagaStates;


public class Game : IGameEventProcessor<object> {
    
    private GameTimer gameTimer;
    
    private GameEventBus<object> eventBus;
    private Window win;
    private StateMachine stateMachine;
    

    public Game() {
        win = new Window("Galaga", 500, 500);
        gameTimer = new GameTimer(60, 60);
        stateMachine = new StateMachine();
        
        // EventHandling
        eventBus = GalagaBus.GetBus();
        eventBus.InitializeEventBus(new List<GameEventType> {
            GameEventType.InputEvent, // key press / key release
            GameEventType.WindowEvent, // messages to the window });
            GameEventType.PlayerEvent, // player event
            GameEventType.GameStateEvent
        });
        win.RegisterEventBus(eventBus);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent,this);
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
        Console.WriteLine("Game.ProcessEvent");
        if (eventType == GameEventType.InputEvent) {
            stateMachine.ActiveState.HandleKeyEvent(gameEvent.Message,
                gameEvent.Parameter1);
        }

        Console.WriteLine($"GameEventType {eventType}");
        Console.WriteLine($"GameEvent.Message {gameEvent.Message}");
        Console.WriteLine($"GameEvent.Para1 {gameEvent.Parameter1}");
        Console.WriteLine($"GameEvent.Para2 {gameEvent.Parameter2}");
        if (eventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
            case "CLOSE_WINDOW":
                win.CloseWindow();
                break;
            }
        }
    }
}
