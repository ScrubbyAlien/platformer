using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Platform : Entity
{
    private readonly bool breakable;
    
    public Platform() : base("tileset")
    {
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }

    public bool Breakable
    {
        get => breakable;
        init
        {
            breakable = value;
            if (value) sprite.TextureRect = new IntRect(108, 0, 18, 18);
        }
    }

    public override bool Solid => true;
}