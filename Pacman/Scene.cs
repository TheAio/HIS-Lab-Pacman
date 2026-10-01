using System.Runtime.CompilerServices;
using SFML.Graphics;

namespace Pacman;

public class Scene
{
    //private Entity entity = new Entity("pacman");
    private AssetManager assets = new();
    private SceneLoader sceneLoader = new();
    private EventManager events = new();
    
    private List<Entity> entities = new();
    public SceneLoader Loader { get => sceneLoader; }
    public AssetManager Assets { get => assets; }
    public EventManager Events { get => events; }

    private float frozenResetTimer = 1f;
    public bool isGameStarted = false;


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
        Loader.HandleSceneLoad(this);
        events.UpdateEvents(this);

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