using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Pacman;

public class Pacman : Actor
{
    public override void Create(Scene scene)
    {
        speed = 100f;
        base.Create(scene);
        sprite.TextureRect = new IntRect(18, 18, 18, 18);
    }

    protected override int PickDirection(Scene scene)
    {
        int dir = direction;
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
}

