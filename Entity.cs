using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Entity 
{
    private readonly string _textureName;
    protected readonly Sprite _sprite;
    public bool Dead;

    protected Entity(string textureName)
    {
        _sprite = new Sprite();
        _textureName = textureName;
    }

    public Vector2f Position
    {
        get => _sprite.Position; 
        set => _sprite.Position = value;
    }
    
    public virtual bool Solid => false;

    public virtual FloatRect Bounds => _sprite.GetGlobalBounds();

    public virtual void Create(Scene scene)
    {
        _sprite.Texture = scene.LoadTexture(_textureName);
    }

    public virtual void Update(Scene scene, float deltaTime)
    {
        
    }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(_sprite);
    }
}