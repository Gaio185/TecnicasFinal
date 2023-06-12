using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TrabalhoTécnicas2
{
    public class HeavySlime
    {
        public Texture2D Texture { get; set; }
        private Vector2 position;
        private Vector2 direction;

        private int radius = 30;
        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        public Rectangle Bounds
        {
            get
            {
                int diameter = radius * 3;
                return new Rectangle((int)(position.X - radius), (int)(position.Y - radius), diameter, diameter);
            }
        }
        private Game1 game;
        private Texture2D[][] heavyslimeTextures;
        private float speed;
        private float damage;
        private Player player;
        private List<Texture2D> animationFrames;
        private int currentFrame;
        private float frameTime;
        private float elapsedTime;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Damage
        {
            get { return damage; }
        }

        public HeavySlime(Game1 game1, Player targetPlayer, Vector2 positionX)
        {
            Random random = new Random();
            position = positionX;
            player = targetPlayer;
            speed = 50f; // Velocidade do slime
            damage = 0.03f; // Dano causado pelo slime
            game = game1;
            animationFrames = new List<Texture2D>();
            // Adicione as texturas dos quadros da animação à lista
            animationFrames.Add(game.Content.Load<Texture2D>("heavyslime"));
            animationFrames.Add(game.Content.Load<Texture2D>("heavyslime2"));
            animationFrames.Add(game.Content.Load<Texture2D>("heavyslime3"));
            animationFrames.Add(game.Content.Load<Texture2D>("heavyslime4"));
            currentFrame = 0;
            frameTime = 0.2f; // Tempo entre cada quadro da animação
            elapsedTime = 0f;
        }
        public void Update(GameTime gameTime)
        {
            Vector2 targetDirection = Vector2.Normalize(player.position - position);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += targetDirection * speed * dt;

            // Verifica se o Heavyslime ultrapassou as bordas da janela e ajusta a posição
            if (position.X < radius)
                position.X = radius;
            else if (position.X > (Game1.Width - radius))
                position.X = Game1.Width - radius;

            if (position.Y < radius)
                position.Y = radius;
            else if (position.Y > (Game1.Height - radius))
                position.Y = Game1.Height - radius;


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
