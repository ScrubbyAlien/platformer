using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Entity 
{
    private readonly string textureName;
    protected readonly Sprite sprite;
    public bool Dead;

    protected Entity(string textureName)
    {
        sprite = new Sprite();
        this.textureName = textureName;
    }

    public Vector2f Position
    {
        get => sprite.Position; 
        set => sprite.Position = value;
    }
    
    public virtual bool Solid => false;

    public virtual FloatRect Bounds => sprite.GetGlobalBounds();

    public virtual void Create(Scene scene)
    {
        sprite.Texture = scene.LoadTexture(textureName);
    }

    public virtual void Update(Scene scene, float deltaTime) { }

    public virtual void Render(RenderTarget target)
    {
        if (!Dead)
        {
            target.Draw(sprite);
        }
    }
}