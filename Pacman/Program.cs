using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Runtime.CompilerServices;

namespace Pacman {
    class Program {
        static void Main(string[] args) {
            using (var window = new RenderWindow(
            new VideoMode(828, 900), "Pacman")) 
            {
                window.Closed += (o, e) => window.Close();
                window.SetView(new View(new FloatRect(18, 0, 414, 450)));
                // TODO: Initialize
                Clock clock = new Clock();
                Scene scene = new Scene();
                float debugTimer = 0;
                scene.Loader.Load("maze");
                
                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();
                    deltaTime = MathF.Min(deltaTime, 0.01f);
                    // TODO: Updates
                    scene.UpdateAll(deltaTime);
                    
                    window.Clear(new Color(223, 246, 245));
                    // TODO: Drawing
                    scene.RenderAll(window);
                    
                    window.Display();
                    
                    //debug keybind to lose 1hp
                    debugTimer += deltaTime;
                    if (Keyboard.IsKeyPressed(Keyboard.Key.C))
                    {
                        if (debugTimer > 0.5f)
                        {
                            scene.Events.PublishLoseHealth(1);
                            debugTimer = 0;
                        }
                    }
                }
            }
        }
    }
}