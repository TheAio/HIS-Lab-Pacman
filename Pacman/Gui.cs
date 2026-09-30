using SFML.Graphics;
using SFML.System;

namespace Pacman;

public class Gui : Entity
{

    private Text scoreText = new();
    private int maxHealth = 3;
    private int currentHealth;
    private int currentScore;
    
    public Gui() : base("pacman")
    {

    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        scoreText.Font = new Font(scene.Assets.LoadFont("pixel-font"));
        scoreText.Scale = new Vector2f(0.5f, 0.5f);
        scoreText.CharacterSize = 50;
        scoreText.Color = Color.Black;
        
        sprite.TextureRect = new IntRect(90, 54, 18, 18);
        scoreText.DisplayedString = "Score";
        currentHealth = maxHealth;
        
        scene.LoseHealth += OnLoseHealth;
        scene.GainScore += OnGainScore;
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
        target.Draw(scoreText);
    }
    
    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.LoseHealth -= OnLoseHealth;
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            DontDestroyOnLoad = false;
            scene.Loader.Reload();
        }
    }

    private void OnGainScore(Scene scene, int amount)
    {
        currentScore += amount;
        if (!scene.FindByType<Coin>(out _))
        {
            DontDestroyOnLoad = true;
            scene.Loader.Reload();
        }
    }
}