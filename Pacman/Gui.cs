using SFML.Graphics;
using SFML.System;

namespace Pacman;

public class Gui : Entity
{

    private Text scoreText = new();
    private int maxHealth = 3;
    private int currentHealth = 3;
    private int currentScore;
    
    public Gui() : base("pacman")
    {

    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        scoreText.Font = new Font(scene.Assets.LoadFont("pixel-font"));
        sprite.TextureRect = new IntRect(90, 54, 18, 18);
        scoreText.DisplayedString = "Score";
        currentHealth = maxHealth;
    }

    public override void Render(RenderTarget target)
    {
        
        sprite.Position = new Vector2f(36, 396);
        for (int i = 0; i < maxHealth; i++)
        {
            sprite.TextureRect = i < currentHealth
                ? new IntRect(72,36,18,18) // Full heart
                : new IntRect(72, 0, 18, 18); // Empty heart
            
            base.Render(target);
            sprite.Position += new Vector2f(18,0);
        }
        scoreText.DisplayedString = $"Score: {currentScore}";
        scoreText.Position = new Vector2f(414 - scoreText.GetGlobalBounds().Width, 396);
        target.Draw(sprite);
        target.Draw(scoreText);
    }
}