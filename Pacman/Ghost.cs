using SFML.Graphics;

namespace Pacman;

public class Ghost : Actor
{
    private float animationTimer = 0f;
    private bool ghostAnimationState = false;
    Dictionary<bool, IntRect> ghostAnimationIntRects = new();
    Dictionary<bool, IntRect> blueGhostAnimationIntRects = new();
    
    private float candyTimer;

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (candyTimer > 0)
        {
            candyTimer -= deltaTime;
            if (candyTimer <= 0.01f)
            {
                candyTimer = 0f;
            }
        }
        // är inte våran implementation bättre optimerad? kolla en if vs att göra beräkning varje gång??
        //frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
    }

    public override void Create(Scene scene)
    {
        direction = -1;
        speed = 100.0f;
        moving = true;
        base.Create(scene);
        
        ghostAnimationIntRects.Add(false,new IntRect(36, 0, 18, 18));
        ghostAnimationIntRects.Add(true,new IntRect(54, 0, 18, 18));

        blueGhostAnimationIntRects.Add(false,new IntRect(36, 18, 18, 18));
        blueGhostAnimationIntRects.Add(true,new IntRect(54, 18, 18, 18));
        
        scene.Events.CandyEaten += OnCandyEaten;
    }

    protected override int PickDirection(Scene scene)
    {
        List<int> validMoves = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            //Can't turn around 180 degrees
            if ((i+2) % 4 == direction) continue;
            if (IsFree(scene, i)) validMoves.Add(i);
        }
        int r = new Random().Next(0, validMoves.Count);
        return validMoves[r];
    }

    protected override void Animate(float deltaTime)
    {
        if (candyTimer > 0f)
        {
            animationTimer += deltaTime;
            if (animationTimer > 0.1f)
            {
                animationTimer = 0f;
                ghostAnimationState = !ghostAnimationState;
            }
            sprite.TextureRect = blueGhostAnimationIntRects[ghostAnimationState];
        }
        else
        {
            animationTimer += deltaTime;
            if (animationTimer > 0.1f)
            {
                animationTimer = 0f;
                ghostAnimationState = !ghostAnimationState;
            }
            sprite.TextureRect = ghostAnimationIntRects[ghostAnimationState];
        }

    }

    protected override void CollideWith(Scene scene, Entity e)
    {
        if (e is Pacman)
        {
            if (candyTimer == 0f)
            {
                scene.Events.PublishLoseHealth(1);
            }
            Reset();
        }
    }

    private void OnCandyEaten(Scene scene, int amount)
    {
        candyTimer += amount;
    }
}