using SFML.Graphics;

namespace platformer;

public class Scene
{
    private readonly Dictionary<string, Texture> _textures;
    private readonly List<Entity> _entities;

    public Scene()
    {
        _textures = new Dictionary<string, Texture>();
        _entities = new List<Entity>();
    }

    public void Spawn(Entity entity)
    {
        _entities.Add(entity);
        entity.Create(this);
    }

    public Texture LoadTexture(string name)
    {
        if (_textures.TryGetValue(name, out Texture? found))
        {
            return found;
        }

        Texture texture = new Texture($"assets/{name}.png");
        _textures.Add(name, texture);
        return texture;
    }

    public void UpdateAll(float deltaTime)
    {
        for (int i = _entities.Count - 1; i >= 0; i--)
        {
            _entities[i].Update(this, deltaTime);
        }

        for (int i = 0; i < _entities.Count;)
        {
            if (_entities[i].Dead) _entities.RemoveAt(i);
            else i++;
        }
    }

    public void RenderAll(RenderTarget target)
    {
        foreach (Entity entity in _entities)
        {
            entity.Render(target);
        }
    }
}