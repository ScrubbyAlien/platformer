using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Platformer;
using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Scene
{
    private readonly Dictionary<string, Texture> textures;
    private readonly List<Entity> entities;

    private string? nextScene;
    private string currentScene;

    public Scene()
    {
        textures = new Dictionary<string, Texture>();
        entities = new List<Entity>();
    }

    public bool TryMove(Entity entity, Vector2f movement)
    {
        entity.Position += movement;
        bool collided = false;

        for (int i = 0; i < entities.Count; i++)
        {
            Entity other = entities[i];
            if (!other.Solid) continue;
            if (ReferenceEquals(other, entity)) continue;

            FloatRect boundsA = entity.Bounds;
            FloatRect boundsB = other.Bounds;
            if (Collision.RectangleRectangle(boundsA, boundsB, out Collision.Hit hit))
            {
                entity.Position += hit.Normal * hit.Overlap;
                i--;
                collided = true;
            }

        }
        
        return collided;
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void Reload()
    {
        nextScene = currentScene;
    }

    public void Load(string scene)
    {
        nextScene = scene;
    }

    private void HandleSceneChange()
    {
        if (nextScene == null) return;
        entities.Clear();
        Spawn(new Background());

        string file = $"assets/{nextScene}.txt";
        Console.WriteLine($"Loading scene '{file}'");

        foreach (string line in File.ReadLines(file, Encoding.UTF8))
        {
            string parsed = line.Trim();
            
            int commentAt = parsed.IndexOf('#');
            if (commentAt >= 0) 
            {
                parsed = parsed.Substring(0, commentAt).Trim();
            }
            if (parsed.Length == 0) continue;
            string[] words = parsed.Split(" ");
            string type = words[0];
            float x = float.Parse(words[1]);
            float y = float.Parse(words[2]);

            switch (type)
            {
                case "w":
                    Spawn(new Platform { Position = new Vector2f(x, y) });
                    break;
                case "d":
                    Spawn(new Door
                    {
                        Position = new Vector2f(x, y),
                        NextRoom = words[3]
                    });
                    break;
                case "k":
                    Spawn(new Key { Position = new Vector2f(x, y) });
                    break;
                case "h":
                    Spawn(new Hero { Position = new Vector2f(x, y) });
                    break;
            }
            Console.WriteLine($"Loaded {type} at {x}, {y}");
        }
        
        currentScene = nextScene;
        nextScene = null;
    }
    
    public Texture LoadTexture(string name)
    {
        if (textures.TryGetValue(name, out Texture? found))
        {
            return found;
        }

        Texture texture = new Texture($"assets/{name}.png");
        textures.Add(name, texture);
        return texture;
    }

    public void UpdateAll(float deltaTime)
    {
        HandleSceneChange();
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            entities[i].Update(this, deltaTime);
        }

        for (int i = 0; i < entities.Count;)
        {
            if (entities[i].Dead) entities.RemoveAt(i);
            else i++;
        }
    }

    public void RenderAll(RenderTarget target)
    {
        foreach (Entity entity in entities)
        {
            entity.Render(target);
        }
    }
}