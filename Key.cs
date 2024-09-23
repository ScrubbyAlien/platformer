using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Key : Entity
{
    public Key() : base("tileset")
    {
        _sprite.TextureRect = new IntRect(126, 18, 18, 18);
        _sprite.Origin = new Vector2f(9, 9);
    }
}