using System.Runtime.CompilerServices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman;

public class Scene
{
    private AssetManager assets = new();
    private SceneLoader sceneLoader = new();
    private EventManager events = new();
    private Text gameOverText = new();
    
    private List<Entity> entities = new();
    public SceneLoader Loader { get => sceneLoader; }
    public AssetManager Assets { get => assets; }
    public EventManager Events { get => events; }

    private float frozenResetTimer = 1f;
    public bool isGameStarted = false;
    public bool showingGameOverScreen = false;


    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (!entity.DontDestroyOnLoad)
            {
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
    }

    public void UpdateAll(float deltaTime)
    {
        if (!showingGameOverScreen)
        {
            Loader.HandleSceneLoad(this);
            events.UpdateEvents(this);
        }

        if (!isGameStarted)
        {
            Events.PublishResetEvent(1);
            isGameStarted = true;
        }
        
        frozenResetTimer = MathF.Max(frozenResetTimer - deltaTime, 0.0f);
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            entity.Update(this, deltaTime);
        }
    }

    public void RenderAll(RenderTarget target)
    {
        for (int i = 0; i < entities.Count;)
        {
            Entity entity = entities[i];
            entity.Render(target);
            if (entity.Dead) entities.RemoveAt(i);
            else i++;
        }
        if (showingGameOverScreen)
        {
            ShowGameOverScreen(target);
        }

    }

    public bool FindByType<T>(out T found) where T : Entity
    {
        foreach (Entity entity in entities)
        {
            if (!entity.Dead && entity is T typed)
            {
                found = typed;
                return true;
            }
        }
        found = default(T);
        return false;
    }
    
    private void ShowGameOverScreen(RenderTarget renderTarget)
    {
        showingGameOverScreen = true;
        renderTarget.Clear(new Color(223, 246, 245));
        FindByType(out Gui gui);
        gameOverText.DisplayedString = $"Game Over\nHigh Score:{gui.GetHighScore()}";
        gameOverText.Position = new Vector2f((renderTarget.GetView().Viewport.Width*100)+(gameOverText.GetGlobalBounds().Width/2),100);
        gameOverText.Scale = new Vector2f(0.5f, 0.5f);
        gameOverText.Font = new Font(Assets.LoadFont("pixel-font"));
        gameOverText.Color = Color.Black;
        renderTarget.Draw(gameOverText);
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
        {
            if (showingGameOverScreen)
            {
                showingGameOverScreen = false;
                isGameStarted = false;
                Clear();
                Loader.Reload();
            }
        }
    }
    
    public IEnumerable<Entity> FindIntersects(FloatRect bounds)
    {
        int lastEntity = entities.Count - 1;

        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.Dead) continue;
            if (entity.Bounds.Intersects(bounds))
            {
                yield return entity;
            }
        }
    }
}