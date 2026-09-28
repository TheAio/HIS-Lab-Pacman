using SFML.Graphics;

namespace Pacman;

public class AssetManager
{
    public static readonly string AssetPath = "assets";
    private readonly Dictionary<string, Texture> textures;
    private readonly Dictionary<string, Font> fonts;
    
    public AssetManager()
    {
        textures = new Dictionary<string, Texture>();
        fonts = new Dictionary<string, Font>();
    }

    public Texture LoadTexture(string name)
    {
        string path = $"{AssetPath}/{name}.png";
        if (textures.ContainsKey(name))
        {
            return textures[name];
        }
        if (File.Exists(path))
        {
            Texture texture = new Texture(path);
            textures.Add(name, texture);
            return texture;
        }
        Console.WriteLine($"Texture {name} not found");
        return null;
    }
    public Font LoadFont(string name)
    {
        string path = $"{AssetPath}/{name}.ttf";
        if (fonts.ContainsKey(name))
        {
            return fonts[name];
        }
        if (File.Exists(path))
        {
            Font font = new Font(path);
            fonts.Add(name, font);
            return font;
        }
        Console.WriteLine($"Font {name} not found");
        return null;
    }
}