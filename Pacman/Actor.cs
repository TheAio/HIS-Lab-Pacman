using SFML.Graphics;
using SFML.System;

namespace Pacman;

public class Actor : Entity
{
    private bool wasAligned = false;
    protected float speed = 0;
    protected int direction = 0;
    protected bool moving = false;
    protected Vector2f originalPosition = new(0, 0);
    protected float originalSpeed = 0;
    
    protected Actor() : base("pacman")
    {
        
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (IsAligned)
        {
            if (!wasAligned)
            {
                direction = PickDirection(scene);
            }
            
            if (moving)
            {
                wasAligned = true;
            }
        }
        else
        {
            wasAligned = false;
        }

        if (!moving) return;
        Position += To
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        Reset();
    }

    protected bool IsAligned => (int) MathF.Floor(Position.X) % 18 == 0 && (int) MathF.Floor(Position.Y) % 18 == 0;

    protected void Reset()
    {
        wasAligned = false;
        originalPosition = Position;
        originalSpeed = speed;
    }

    protected bool IsFree(Scene scene, int dir)
    {
        Vector2f at = Position + new Vector2f(9, 9);
        at += 18 * ToVector2f(dir);
        FloatRect controlRectangle = new FloatRect(at.X, at.Y, 1, 1);
        return !scene.FindIntersects(controlRectangle).Any(e => e.Solid);
    }

    protected static Vector2f ToVector2f(int dir)
    {
        switch (dir)
        {
            case 0:
                return new Vector2f(1, 0);
            case 1:
                return new Vector2f(0, 1);
            case 2:
                return new Vector2f(-1, 0);
            case 3:
                return new Vector2f(0, -1);
            default:
                Console.WriteLine($"Invalid direction {dir} in Actor.cs!");
                return new Vector2f(0, 0);
        }
    }

    protected virtual int PickDirection(Scene scene)
    {
        return 0;
    }
}