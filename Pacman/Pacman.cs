using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Pacman;

public class Pacman : Actor
{
    private Dictionary<int, IntRect> pacmanStateAIntRects = new();
    private Dictionary<int, IntRect> pacmanStateBIntRects = new();
    
    private float animationTimer = 0f;
    private float freezeTimer = 0f;
    private bool pacmanState = false;
    private int dir = 1;

    public override FloatRect Bounds
    {
        get
        {
            var bounds = base.Bounds;
            //bounds.Left += 3;
            bounds.Width = 5;
            //bounds.Top += 3;
            bounds.Height = 5;
            return bounds;
        }  
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        
        if (freezeTimer > 0f)
        {
            freezeTimer -= deltaTime;
            moving = false;
            if (freezeTimer <= 0.05f)
            {
                freezeTimer = 0f;
                moving = true;
            }
        }
    }
    protected override void Animate (float deltaTime)
    {
        Dictionary<int, IntRect> currentStateIntRects;
        animationTimer += deltaTime;
        if (animationTimer > 0.2f)
        {
            animationTimer = 0;
            pacmanState = !pacmanState;
        }

        if (pacmanState)
        {
            currentStateIntRects = pacmanStateBIntRects;
        }
        else
        {
            currentStateIntRects = pacmanStateAIntRects;
        }
        if (direction >= 0 && direction <= 3)
        {
            sprite.TextureRect = currentStateIntRects[direction];
        }
        else
        {
            Console.WriteLine($"Illegal pacman direction {direction} in Pacman.cs");
            sprite.TextureRect = currentStateIntRects[0];
        }
    }
    
    public override void Create(Scene scene)
    {
        speed = 100f;
        base.Create(scene);
        const int textureSize = 18;
        pacmanStateAIntRects.Add(0, new IntRect(0,0,textureSize,textureSize)); // Right
        pacmanStateBIntRects.Add(0, new IntRect(18,0,textureSize,textureSize));
        pacmanStateAIntRects.Add(1, new IntRect(0,54,textureSize,textureSize)); // Down
        pacmanStateBIntRects.Add(1, new IntRect(18,54,textureSize,textureSize));
        pacmanStateAIntRects.Add(2, new IntRect(0,36,textureSize,textureSize)); // Left
        pacmanStateBIntRects.Add(2, new IntRect(18,36,textureSize,textureSize));
        pacmanStateAIntRects.Add(3, new IntRect(0,18,textureSize,textureSize)); // Up
        pacmanStateBIntRects.Add(3, new IntRect(18,18,textureSize,textureSize));

        scene.Events.LoseHealth += OnLoseHealth;
        scene.Events.ResetEvent += OnFreeze;
    }

    protected override int PickDirection(Scene scene)
    {
        dir = direction;
        if (Keyboard.IsKeyPressed(Right))
        {
            dir = 0;
            moving = true;
        }
        else if (Keyboard.IsKeyPressed(Up))
        {
            dir = 3;
            moving = true;
        }
        else if (Keyboard.IsKeyPressed(Down))
        {
            dir = 1;
            moving = true;
        }
        else if (Keyboard.IsKeyPressed(Left))
        {
            dir = 2;
            moving = true;
        }
        if (IsFree(scene, dir))
        {
            return dir;
        }

        if (!IsFree(scene, direction))
        {
            moving = false;
        }
        return direction;
    }

    private void OnFreeze(Scene scene, int amount)
    {
        freezeTimer += amount;
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.Events.LoseHealth -= OnLoseHealth;
        scene.Events.ResetEvent -= OnFreeze;
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        Reset();
        scene.Events.PublishResetEvent(1);
    }
}

