using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace platformer;

public class Hero : Entity
{
    private const float SPEED = 100;
    private bool _faceRight;
    
    public Hero() : base("characters")
    {
        _sprite.TextureRect = new IntRect(0, 0, 24, 24);
        _sprite.Origin = new Vector2f(12, 12);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            Position -= new Vector2f(SPEED * deltaTime, 0);
            _faceRight = false;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            Position += new Vector2f(SPEED * deltaTime, 0);
            _faceRight = true;
        }
    }

    public override void Render(RenderTarget target)
    {
        _sprite.Scale = new Vector2f(_faceRight ? -1 : 1, 1);
        base.Render(target);
    }
}