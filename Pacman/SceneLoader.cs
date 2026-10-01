using System.Text;
using SFML.System;

namespace Pacman;

public class SceneLoader
{
    private readonly Dictionary<char, Func<Entity>> loaders;
    private HashSet<Gui> guis;
    private string currentScene = "", nextScene = "";

    public SceneLoader()
    {
        loaders = new Dictionary<char, Func<Entity>>
        {
            {'#', () => new Wall()},
            {'c', () => new Candy()},
            {'.', () => new Coin()},
            {'g', () => new Ghost()},
            {'p', () => new Pacman()}
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

    /*private void CreateGUI(out Entity created)
    {
        Func<Entity> createdGui = () => new Gui();
        created = createdGui();
    }*/

    public void HandleSceneLoad(Scene scene)
    {
        if (nextScene == "") return;
        scene.Clear();
        
        //TODO: Replace maze.txt with variable
        int posX = 0;
        int posY = 0;

        foreach (var line in File.ReadLines("assets/maze.txt", Encoding.UTF8))
        {
            string parsed = line.Trim();
            foreach (char c in parsed)
            {
                Vector2f coords = new Vector2f((posX*18), (posY*18));

                if (loaders.ContainsKey(c))
                {
                    Create(c, out Entity created);
                    created.Position = coords;
                    scene.Spawn(created);
                }
                posX++;
            }
            posX = 0;
            posY++;
        }
        
        currentScene = nextScene;
        nextScene = "";

        if (!scene.FindByType<Gui>(out _))
        {
            Gui gui = new Gui();
            scene.Spawn(gui);
        }
    }
    
    
    
    public void Load(string scene) => nextScene = scene;
    public void Reload() => nextScene = currentScene;
    
}