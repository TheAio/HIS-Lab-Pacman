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

    public void Spawn(Entity entity)
    {
        
    }

    public void Clear()
    {
        
    }

    public void UpdateAll(float deltaTime)
    {
        Loader.HandleSceneLoad(this);
        
        foreach (Entity entity in entities)
        {
            entity.Update(this, deltaTime);
            //Todo: should we really use this here?
        }
    }

    public void RenderAll(RenderTarget target)
    {
        for (int i = 0; i < entities.Count;)
        {
            Entity entity = entities[i];
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