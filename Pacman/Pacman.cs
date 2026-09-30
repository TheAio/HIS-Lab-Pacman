using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Pacman;

public class Pacman : Actor
{
    private Dictionary<int, IntRect> pacmanStateAIntRects = new();
    private Dictionary<int, IntRect> pacmanStateBIntRects = new();
    
    private float animationTimer = 0f;
    private bool pacmanState = false;
    private int dir = 1;
    
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
        if (dir >= 0 && dir <= 3)
        {
            sprite.TextureRect = currentStateIntRects[dir];
        }
        else
        {
            Console.WriteLine($"Illegal pacman direction {dir} in Pacman.cs");
            sprite.TextureRect = currentStateIntRects[0];
        }
    }
    
    public override void Create(Scene scene)
    {
        speed = 100f;
        base.Create(scene);
        //Because the tutorial guided us to use 3 as up and 1 as down, we have to use this solution,
        //but ideally we could have used a for loop, if 1 was up and 3 was down
        pacmanStateAIntRects.Add(0, new IntRect(0,0,18,18)); // Right
        pacmanStateBIntRects.Add(0, new IntRect(18,0,18,18));
        pacmanStateAIntRects.Add(1, new IntRect(0,54,18,18)); // Down
        pacmanStateBIntRects.Add(1, new IntRect(18,54,18,18));
        pacmanStateAIntRects.Add(2, new IntRect(0,36,18,18)); // Left
        pacmanStateBIntRects.Add(2, new IntRect(18,36,18,18));
        pacmanStateAIntRects.Add(3, new IntRect(0,18,18,18)); // Up
        pacmanStateBIntRects.Add(3, new IntRect(18,18,18,18));

        scene.LoseHealth += OnLoseHealth;
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

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.LoseHealth -= OnLoseHealth;
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        Reset();
    }
}

