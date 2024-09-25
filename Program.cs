using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace platformer;

static class Program
{
    public const int ScreenWidth = 800;
    public const int ScreenHeight = 600;
    
    
    public static void Main(string[] args)
    {
        using var window = new RenderWindow(new VideoMode(ScreenWidth, ScreenHeight), "breakout");
        window.Closed += (o, e) => window.Close();

        Clock clock = new();
        Scene scene = new();
        scene.Load("level0");
        
        
        window.SetView(new View(new Vector2f(200, 150), new Vector2f(400, 300)));

        
        while (window.IsOpen)
        {
            float deltaTime = clock.Restart().AsSeconds();
            window.DispatchEvents();
            scene.UpdateAll(deltaTime);
            
            window.Clear(new Color(150, 200, 255));
            scene.RenderAll(window);
            
            window.Display();
        }
    }

} 

