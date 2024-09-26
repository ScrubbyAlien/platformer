using Platformer;
using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Door : Entity
{
    public string NextRoom = "";
    private bool unlocked;
    
    public Door() : base("tileset")
    {
        sprite.TextureRect = new IntRect(180, 103, 18, 23);
        sprite.Origin = new Vector2f(9, 11.5f);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        if (scene.FindByType(out Hero hero))
        {
            if (Collision.RectangleRectangle(Bounds, hero.Bounds, out _) && unlocked)
            {
                scene.Load(NextRoom);
            }
        }
    }

    public void Unlock()
    {
        unlocked = true;
        sprite.Color = Color.Black;
    }
}