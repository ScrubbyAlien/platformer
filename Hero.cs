using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace platformer;

public class Hero : Entity
{
    public const float SPEED = 100f;
    public const float JUMP_FORCE = 250f;
    public const float GRAVITY_FORCE = 400f;

    private float verticalSpeed;
    private bool isGrounded;
    private bool isUpPressed;
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
            scene.TryMove(this, new Vector2f(-SPEED * deltaTime, 0));
            _faceRight = false;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            scene.TryMove(this, new Vector2f(SPEED * deltaTime, 0));
            _faceRight = true;
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
        {
            if (isGrounded && !isUpPressed)
            {
                verticalSpeed = -JUMP_FORCE;
                isUpPressed = true;
            }
        }
        else
        {
            isUpPressed = false;
        }

        verticalSpeed += GRAVITY_FORCE * deltaTime;
        if (verticalSpeed > 500f) verticalSpeed = 500f;
        isGrounded = false;
        Vector2f velocity = new Vector2f(0, verticalSpeed * deltaTime);
        if (scene.TryMove(this, velocity))
        {
            if (verticalSpeed > 0.0f)
            {
                isGrounded = true;
            }
            verticalSpeed = 0.0f;
        }
        
        if (Position.Y > Program.ScreenHeight + Bounds.Height / 2) scene.Reload();
        if (Position.X > Program.ScreenWidth + Bounds.Width / 2) scene.Reload();
        if (Position.Y < 0 - Bounds.Height / 2) scene.Reload();
        if (Position.X < 0 - Bounds.Width / 2) scene.Reload();

        

    }
    
    public override void Render(RenderTarget target)
    {
        _sprite.Scale = new Vector2f(_faceRight ? -1 : 1, 1);
        base.Render(target);
    }
}