using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace platformer;

static class Program
{
    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;
    
    
    public static void Main(string[] args)
    {
        using var window = new RenderWindow(new VideoMode(ScreenWidth, ScreenHeight), "breakout");
        window.Closed += (o, e) => window.Close();

        Clock clock = new();
        Scene scene = new();
        Background background = new();
        Hero hero = new();
        hero.Position = new Vector2f(36, 288 - 9 - 12);
        
        
        scene.Spawn(background);
        scene.Spawn(hero);
        
        for (int i = 0; i < 10; i++)
        {
            scene.Spawn(new Platform
            {
                Position = new Vector2f(18 + i * 18, 288)
            });
        }
        
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

