using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace platformer;

public class Hero : Entity
{
    private const float SPEED = 100f;
    private const float JUMP_FORCE = 250f;
    private const float GRAVITY_FORCE = 400f;
    private readonly IntRect STANDING = new IntRect(0, 0, 24, 24);
    private readonly IntRect WALKING = new IntRect(24, 0, 24, 24);
    private bool walking;
    private const float TOGGLE_TIME = 0.1f;
    private float timeSinceToggle = 0;

    private float verticalSpeed;
    private bool isGrounded;
    private bool isUpPressed;
    private bool _faceRight;

    public Hero() : base("characters")
    {
        sprite.TextureRect = STANDING;
        sprite.Origin = new Vector2f(12, 12);
    }

    public override FloatRect Bounds
    {
        get
        {
            FloatRect bounds = base.Bounds;
            bounds.Left += 3;
            bounds.Width -= 6;
            bounds.Top += 3;
            bounds.Height -= 3;
            return bounds;
        }
    }

    public float VerticalSpeed => verticalSpeed;

    private void ToggleAnimation(float deltaTime)
    {
        timeSinceToggle += deltaTime;

        if (walking && isGrounded && timeSinceToggle >= TOGGLE_TIME)
        {
            if (sprite.TextureRect.Left == STANDING.Left) sprite.TextureRect = WALKING;
            else if (sprite.TextureRect.Left == WALKING.Left) sprite.TextureRect = STANDING;
            timeSinceToggle = 0;
        }
        else if (!isGrounded)
        {
            sprite.TextureRect = WALKING;
        }
        else if (!walking && isGrounded)
        {
            sprite.TextureRect = STANDING;
        }
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            scene.TryMove(this, new Vector2f(-SPEED * deltaTime, 0));
            _faceRight = false;
            walking = true;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            scene.TryMove(this, new Vector2f(SPEED * deltaTime, 0));
            _faceRight = true;
            walking = true;
        }
        else
        {
            walking = false;
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
            // let the player choose how high to jump by holding up
            if (isUpPressed && verticalSpeed < 0) verticalSpeed *= 0.05f;

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
                verticalSpeed = 0.0f;
            }
            else
            {
                verticalSpeed = -0.5f * verticalSpeed;
            }
           
        }
        // stabilize isGrounded when on the floor, 1f is just margin when standing still because of collision shenanigans
        else if (verticalSpeed is > -float.Epsilon and < 1f ) 
        {
            isGrounded = true;
        }
        
        
        
        if (Position.Y > Program.ScreenHeight + Bounds.Height / 2) scene.Reload();
        if (Position.X > Program.ScreenWidth + Bounds.Width / 2) scene.Reload();
        if (Position.Y < 0 - Bounds.Height / 2) scene.Reload();
        if (Position.X < 0 - Bounds.Width / 2) scene.Reload();

        ToggleAnimation(deltaTime);
    }
    
    public override void Render(RenderTarget target)
    {
        sprite.Scale = new Vector2f(_faceRight ? -1 : 1, 1);
        base.Render(target);
    }
}