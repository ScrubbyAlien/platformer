using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Door : Entity
{
    public string NextRoom = "";
    public bool Unlocked;
    
    public Door() : base("tileset")
    {
        _sprite.TextureRect = new IntRect(180, 103, 18, 23);
        _sprite.Origin = new Vector2f(9, 11.5f);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        
    }

    public override void Render(RenderTarget target)
    {
        
    }
}