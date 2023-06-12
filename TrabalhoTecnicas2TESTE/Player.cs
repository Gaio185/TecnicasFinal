using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace TrabalhoTécnicas2
{
    public enum Direction
    {
        Up, Down, Left, Right
    }
    public class Player
    {
        
        static public Vector2 defaultPosition = new Vector2(640, 360);
        public Vector2 position = defaultPosition;
        Direction direction;
        public float maxHP;
        public float currentHP;
        public int speed = 180;
        public int radius = 30;
        Game1 game;
        private List<Texture2D> animationFrames;
        private int currentFrame;
        private float frameTime;
        public float elapsedTime;

        public Player(Game1 game1, Vector2 positionX)
        {
            maxHP = 100;
            currentHP = maxHP;
            position = positionX;
            game = game1;
            animationFrames = new List<Texture2D>();
            // Adicione as texturas dos quadros da animação à lista
            animationFrames.Add(game.Content.Load<Texture2D>("playerchar"));
            animationFrames.Add(game.Content.Load<Texture2D>("playerchar2"));
            animationFrames.Add(game.Content.Load<Texture2D>("playerchar3"));
            animationFrames.Add(game.Content.Load<Texture2D>("playerchar4"));
            animationFrames.Add(game.Content.Load<Texture2D>("playerchar5"));
            animationFrames.Add(game.Content.Load<Texture2D>("playerchar6"));

            currentFrame = 0;
            frameTime = 0.2f; // Tempo entre cada quadro da animação
            elapsedTime = 0f;
        }

        
        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D)) && position.X < (Game1.Width-radius))
            {
                position.X += speed * dt;
                direction = Direction.Right;
            }

            if ((kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A)) && position.X > radius)
            {
                position.X -= speed * dt;
                direction = Direction.Left;
            }

            if ((kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S)) && position.Y < (Game1.Height - radius))
            {
                position.Y += speed * dt;
                direction = Direction.Down;
            }

            if ((kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W)) && position.Y > radius)
            {
                position.Y -= speed * dt;
                direction = Direction.Up;
            }

            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime >= frameTime)
            {
                currentFrame = (currentFrame + 1) % animationFrames.Count;
                elapsedTime = 0f;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Outro código de desenho

            // Desenhe o quadro atual da animação
            spriteBatch.Draw(animationFrames[currentFrame], position, Color.White);
        }

    }

    }


