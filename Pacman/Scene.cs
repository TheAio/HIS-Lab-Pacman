using SFML.Graphics;

namespace Pacman;

public class Scene
{
    //private Entity entity = new Entity("pacman");
    private AssetManager assets = new();
    private SceneLoader sceneLoader = new();

    private List<Entity> entities = new();
    public SceneLoader Loader { get => sceneLoader; }
    public AssetManager Assets { get => assets; }
    
    public event ValueChangedEvent GainScore;
    public event ValueChangedEvent LoseHealth;
    
    private int scoreGained;
    private int loseHealth;
    public void PublishGainScore(int amount) => scoreGained += amount;
    public void PublishLoseHealth(int amount) => loseHealth += amount;

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
            entities.RemoveAt(i);
            entity.Destroy(this);
        }
    }

    public void UpdateAll(float deltaTime)
    {
        Loader.HandleSceneLoad(this);
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            entity.Update(this, deltaTime);
            if (scoreGained != 0)
            {
                GainScore?.Invoke(this, scoreGained);
                scoreGained = 0;
            }

            if (loseHealth != 0)
            {
                LoseHealth?.Invoke(this, loseHealth);
                loseHealth = 0;
            }
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
        //TODO FIX THIS SHIT AND LOOK AT THIS SHIT
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

public delegate void ValueChangedEvent(Scene scene, int value);