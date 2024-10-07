using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Background : Entity
{
    private const float HEIGHT = 24;
    private const float WIDTH = 24;
    
    public Background() : base("background")
    {
        sprite.TextureRect = new IntRect(0, 0, (int) WIDTH, (int) HEIGHT);
        sprite.Origin = new Vector2f(WIDTH / 2, HEIGHT / 2);
    }

    public override void Render(RenderTarget target)
    {
        View view = target.GetView();

        Vector2f topLeft = view.Center - 0.5f * view.Size;
        int tilesX = (int)MathF.Ceiling(view.Size.X / WIDTH);
        int tilesY = (int)MathF.Ceiling(view.Size.Y / HEIGHT);

        for (int row = 0; row <= tilesY; row++)
        {
            for (int col = 0; col <= tilesX; col++)
            {
                sprite.TextureRect = row switch
                {
                    < 5 => new IntRect(0, 0, (int)WIDTH, (int)HEIGHT),
                    5 => new IntRect(24, 0, (int)WIDTH, (int)HEIGHT),
                    > 5 => new IntRect(48, 0, (int)WIDTH, (int)HEIGHT)
                };

                sprite.Origin = new Vector2f();
                sprite.Position = topLeft + 24 * new Vector2f(col, row);
                target.Draw(sprite);
            }
        }
    }
}