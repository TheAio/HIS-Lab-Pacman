using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace Pacman;

public class Entity
{
    private string textureName;
    protected Sprite sprite;
    public bool Dead = false;

    /*protected Entity(string textureName)
    {
        
    }*/

    public Vector2f Position
    {
        get => sprite.Position;
        set => sprite.Position = value;
    }

    public virtual FloatRect Bounds
    {
        get => sprite.GetGlobalBounds(); 
    }
    
    public bool Solid { get; }

    public void Create(Scene scene)
    {
        // TODO fix this shit
    }

    public void Destroy(Scene scene)
    {
        
    }
    
    public virtual void Update(Scene scene, float deltaTime)
    {
        foreach (Entity found in scene.FindIntersects(Bounds))
        {
            CollideWith(scene, found);
        }
    }

    public void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }

    protected void CollideWith(Scene scene, Entity otherEntity)
    {
        
    }
}