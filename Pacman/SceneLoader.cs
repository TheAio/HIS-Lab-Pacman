using System.Text;
using SFML.System;

namespace Pacman;

public class SceneLoader
{
    private readonly Dictionary<char, Func<Entity>> loaders;
    private Dictionary<Vector2f, Entity> entityAtCoordinates = new();
    private string currentScene = "", nextScene = "";

    public SceneLoader()
    {
        loaders = new Dictionary<char, Func<Entity>>
        {
            {'#', () => new Wall()},
            {'c', () => new Candy()},
            {'.', () => new Coin()}
        };
    }

    private bool Create(char symbol, out Entity created)
    {
        if (loaders.TryGetValue(symbol, out Func<Entity> loader))
        {
            created = loader();
            return true;
        }
        
        created = null;
        return false;
    }

    public void HandleSceneLoad(Scene scene)
    {
        if (nextScene == "") return;
        scene.Clear();
        //TODO: Load scene file
        
        //TODO: Replace maze.txt with variable
        int posX = 0;
        int posY = 0;
        //TODO: decide if we should do charAtCoordinates.Clear();
        foreach (var line in File.ReadLines("assets/maze.txt", Encoding.UTF8))
        {
            posY++;
            string parsed = line.Trim();
            foreach (char c in parsed)
            {
                Vector2f coords = new Vector2f((posX*18), (posY*18));
                List<char> temporaryListPleaseRemove = new List<char>(); //Todo: seriously remove this!!!!
                temporaryListPleaseRemove.Add('#');
                temporaryListPleaseRemove.Add('c');
                temporaryListPleaseRemove.Add('.');
                if (temporaryListPleaseRemove.Contains(c))
                {
                    Create(c, out Entity created);
                    created.Position = coords;
                    scene.Spawn(created);
                }
                //entityAtCoordinates.Add(coords,created);
                posX++;
            }
            posX = 0;
        }
        
        currentScene = nextScene;
        nextScene = "";
    }
    
    public void Load(string scene) => nextScene = scene;
    public void Reload() => nextScene = currentScene;
    
}