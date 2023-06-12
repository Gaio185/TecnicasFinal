using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace TrabalhoTécnicas2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        private List<Slime> slimes;
        private List<HeavySlime> heavyslime;
        private List<Ball> balls;
        private float attackRange = 200f;
        private double elapsedTime = 0;
        public const int Width= 1280;
        public const int Height= 720;
        private Slime slime;
        private HeavySlime heavySlime;
        private Ball ball;
        private double spawnTimer;
        private double spawnTimer2;
        private double spawnTimer3;
        private bool inGame = false;
        private double bestTime = 0;

        private int playerStartX;
        private int playerStartY;
        private Player player;
        private Texture2D background;

        private Song _song;
        private float _volume = 0.5f;
        private SoundEffect _hurtSound;
        private SoundEffect _attackSound;
        private SoundEffect _slimeHurtSound;
        private SoundEffect _heavySlimeHurtSound;
        private SoundEffect _ballHurtSound;
        private SoundEffectInstance _attackSoundInstance;
        private SoundEffectInstance _hurtSoundInstance;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = Width;
            _graphics.PreferredBackBufferHeight = Height;
            _graphics.ApplyChanges();
            playerStartX = Width / 2;
            playerStartY=Height / 2;
            player = new Player(this,new Vector2(playerStartX,playerStartY));
            slime = new Slime(this,player,new Vector2(0,0));
            slimes = new List<Slime>();
            heavySlime = new HeavySlime(this, player, new Vector2(0, 0));
            heavyslime = new List<HeavySlime>();
            ball = new Ball(this, player, new Vector2(0, 0));
            balls = new List<Ball>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("grass");
            font = Content.Load<SpriteFont>("font");

            _song = Content.Load<Song>("musica");
            MediaPlayer.Volume = 0.0f;
            MediaPlayer.Play(_song);

            _hurtSound = Content.Load<SoundEffect>("hurtSound");
            _hurtSoundInstance = _hurtSound.CreateInstance();
            _attackSound = Content.Load<SoundEffect>("attackSound");
            _attackSoundInstance = _attackSound.CreateInstance();
            _slimeHurtSound = Content.Load<SoundEffect>("slimeHurtSound");
            _heavySlimeHurtSound = Content.Load<SoundEffect>("heavySlimeHurtSound");
            _ballHurtSound = Content.Load<SoundEffect>("ballHurtSound");

            player.position = new Vector2(playerStartX, playerStartY);
            base.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                inGame = true;
            }

            if(inGame == false) { 
                if(elapsedTime > bestTime) { bestTime = elapsedTime; }
                elapsedTime = 0; 
            }

            if(inGame == true) {

                // TODO: Add your update logic here
                elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

                MediaPlayer.Volume = 0.25f;
                // Controla o volume da musica
                if (Keyboard.GetState().IsKeyDown(Keys.M))
                {
                    _volume += 0.01f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.N))
                {
                    _volume -= 0.01f;
                }
                // Garante que o volume fique entre 0 e 1
                _volume = (float)Math.Clamp(_volume, 0.0, 1.0);
                // Modifica o volume da música
                MediaPlayer.Volume = _volume;

                MouseState mouseState = Mouse.GetState();

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    _attackSoundInstance.Play();
                    
                }

                player.Update(gameTime);
                spawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                spawnTimer2 += gameTime.ElapsedGameTime.TotalMilliseconds;
                spawnTimer3 += gameTime.ElapsedGameTime.TotalMilliseconds;

                // Intervalo de tempo para spawnar um novo slime (em milissegundos)
                double spawnInterval = 2000;
                double spawnIntervalB = 5000;
                double spawnIntervalH = 10000;

                // Verifique se é hora de spawnar um novo slime
                if (spawnTimer >= spawnInterval)
                {
                    // Crie um novo slime e adicione à lista de slimes
                    Slime newSlime = new Slime(this, player, GetRandomSpawnPosition()); // Posição inicial do slime
                    slimes.Add(newSlime);

                    // Reinicie o temporizador
                    spawnTimer = 0;
                }

                //------------------------------------------------------------------------
                // Verifique se é hora de spawnar um novo Heavyslime
                if (spawnTimer2 >= spawnIntervalH)
                {
                    // Crie um novo slime e adicione à lista de slimes
                    HeavySlime newheavySlime = new HeavySlime(this, player, GetRandomSpawnPosition()); // Posição inicial do Heavyslime
                    heavyslime.Add(newheavySlime);

                    // Reinicie o temporizador
                    spawnTimer2 = 0;
                }
                //-------------------------------
                // Verifique se é hora de spawnar uma nova Ball
                if (spawnTimer3 >= spawnIntervalB)
                {
                    // Crie um novo slime e adicione à lista de slimes
                    Ball newball = new Ball(this, player, GetRandomSpawnPosition()); // Posição inicial do Heavyslime
                    balls.Add(newball);

                    // Reinicie o temporizador
                    spawnTimer3 = 0;
                }
                

                //-----------------------

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Rectangle mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                    foreach (Slime slime in slimes)
                    {
                        if (slime.Bounds.Intersects(mouseRect))
                        {
                            float distance = Vector2.Distance(player.position, slime.Position);

                            if (distance <= attackRange)
                            {
                                _slimeHurtSound.Play();
                                slimes.Remove(slime);
                                // Outra lógica adicional, como pontuação ou efeitos sonoros
                                break; // Encerra o loop, pois apenas uma slime pode ser atingida por vez
                            }
                        }
                    }
                }
                // Atualize cada slime na lista
                foreach (Slime slime in slimes)
                {
                    slime.Update(gameTime);
                }
                foreach (Slime slime in slimes)
                {
                    if (CheckCollision(player, slime))
                    {
                        TakeDamage(player,slime); // Aplica um dano fixo ao jogador
                        //slime.Destroy(); // Destroi a slime após atingir o jogador
                    }
                }

                //--------------------------------------------------------//-------------------


                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Rectangle mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                    foreach (Ball ball in balls)
                    {
                        if (ball.Bounds.Intersects(mouseRect))
                        {
                            float distance = Vector2.Distance(player.position, ball.Position);

                            if (distance <= attackRange)
                            {
                                _ballHurtSound.Play();
                                balls.Remove(ball);
                                // Outra lógica adicional, como pontuação ou efeitos sonoros
                                break; // Encerra o loop, pois apenas uma slime pode ser atingida por vez
                            }
                        }
                    }
                }
                if (balls.Count > 0)
                {
                    // Atualize cada slime na lista
                    foreach (Ball ball in balls)
                    {
                        ball.Update(gameTime);
                    }
                    foreach (Ball ball in balls)
                    {
                        if (CheckCollision3(player, ball))
                        {
                            TakeDamage3(player, ball); // Aplica um dano fixo ao jogador
                            //slime.Destroy(); // Destroi a slime após atingir o jogador
                        }
                    }
                }


                //--------------------------------------------------------------
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Rectangle mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                    foreach (HeavySlime heavySlime in heavyslime)
                    {
                        if (heavySlime.Bounds.Intersects(mouseRect))
                        {
                            float distance = Vector2.Distance(player.position, heavySlime.Position);

                            if (distance <= attackRange)
                            {
                                _heavySlimeHurtSound.Play();
                                heavyslime.Remove(heavySlime);
                                // Outra lógica adicional, como pontuação ou efeitos sonoros
                                break; // Encerra o loop, pois apenas uma slime pode ser atingida por vez
                            }
                        }
                    }
                }
                // Atualize cada slime na lista
                foreach (HeavySlime heavySlime in heavyslime)
                {
                    heavySlime.Update(gameTime);
                }
                foreach (HeavySlime heavySlime in heavyslime)
                {
                    if (CheckCollision2(player, heavySlime))
                    {
                        TakeDamage2(player, heavySlime); // Aplica um dano fixo ao jogador
                        //slime.Destroy(); // Destroi a slime após atingir o jogador
                    }
                }


                base.Update(gameTime);
           }
        }
        private bool CheckCollision(Player player, Slime slime)
        {
            Rectangle playerRect = new Rectangle((int)player.position.X - player.radius, (int)player.position.Y - player.radius, player.radius * 2, player.radius * 2);
            Rectangle slimeRect = new Rectangle((int)slime.Position.X - slime.Radius, (int)slime.Position.Y - slime.Radius, slime.Radius * 2, slime.Radius * 2);

            if (playerRect.Intersects(slimeRect))
            {
                return true; // Há colisão
            }

            return false; // Não há colisão
        }

        private bool CheckCollision2(Player player, HeavySlime heavySlime)
        {
            Rectangle playerRect = new Rectangle((int)player.position.X - player.radius, (int)player.position.Y - player.radius, player.radius * 2, player.radius * 2);
            Rectangle heavyslimeRect = new Rectangle((int)heavySlime.Position.X - heavySlime.Radius, (int)heavySlime.Position.Y - heavySlime.Radius, heavySlime.Radius * 2, heavySlime.Radius * 2);

            if (playerRect.Intersects(heavyslimeRect))
            {
                return true; // Há colisão
            }

            return false; // Não há colisão
        }

        private bool CheckCollision3(Player player, Ball ball)
        {
            Rectangle playerRect = new Rectangle((int)player.position.X - player.radius, (int)player.position.Y - player.radius, player.radius * 2, player.radius * 2);
            Rectangle ballsRect = new Rectangle((int)ball.Position.X - ball.Radius, (int)ball.Position.Y - ball.Radius, ball.Radius * 2, ball.Radius * 2);

            if (playerRect.Intersects(ballsRect))
            {
                return true; // Há colisão
            }

            return false; // Não há colisão
        }
       
        //----------------------------------------------------------------------
        private void TakeDamage(Player player, Slime slime)
        {
            _hurtSoundInstance.Play();
            player.currentHP -= slime.Damage;
            if (player.currentHP <= 0)
            {
                inGame = false; // O jogador morreu, implemente a lógica apropriada para o seu jogo
                player.position = Player.defaultPosition;
                player.currentHP = player.maxHP;


            }
        }

        private void TakeDamage2(Player player, HeavySlime heavySlime)
        {
            _hurtSoundInstance.Play();
            player.currentHP -= heavySlime.Damage;
            if (player.currentHP <= 0)
            {
                inGame = false;// O jogador morreu, implemente a lógica apropriada para o seu jogo
                player.position = Player.defaultPosition;
                player.currentHP = player.maxHP;
            }
        }

        private void TakeDamage3(Player player, Ball ball)
        {
            _hurtSoundInstance.Play();
            player.currentHP -= ball.Damage;
            if (player.currentHP <= 0)
            {
                inGame = false; // O jogador morreu, implemente a lógica apropriada para o seu jogo
                player.position = Player.defaultPosition;
                player.currentHP = player.maxHP;
            }
        }

        //------------------------------------------------
        private Vector2 GetRandomSpawnPosition()
        {
            Random random = new Random();

            // Gere coordenadas X e Y aleatórias dentro dos limites da tela
            int x = random.Next(0, Width);
            int y = random.Next(0, Height);

            return new Vector2(x, y);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            //string hpText = "HP: " + player.currentHP.ToString();
            //Vector2 hpPosition = new Vector2(20, 20); // Posição do texto no canto superior esquerdo
            //_spriteBatch.DrawString(font, hpText, hpPosition, Color.Red);
            int grassWidth = background.Width;
            int grassHeight = background.Height;
            int viewportWidth = GraphicsDevice.Viewport.Width;
            int viewportHeight = GraphicsDevice.Viewport.Height;
            for (int x = 0; x < viewportWidth; x += grassWidth)
            {
                for (int y = 0; y < viewportHeight; y += grassHeight)
                {
                    _spriteBatch.Draw(background, new Vector2(x, y), Color.White);
                }
            }

            Vector2 titlePos = new Vector2(Width / 4.5f, Height / 2.8f);
            float fontSize = 5; // Tamanho da fonte
            Color fontColor = Color.Red; // Cor da fonte
            Vector2 hpPosition = new Vector2(20, 0); // Posição do texto no canto superior esquerdo
            Vector2 timePosition = new Vector2(Width - 320, 0);
            Vector2 bestTimePosition = new Vector2(20, 625); 
            string hpText = "HP: " + ((int)player.currentHP).ToString();
            string timeText = "Time: " + Math.Floor(elapsedTime).ToString();
            string bestTimeText = "Best Time: " + Math.Floor(bestTime).ToString();
            _spriteBatch.DrawString(font, hpText, hpPosition, fontColor, 0, Vector2.Zero, fontSize, SpriteEffects.None, 0);
            _spriteBatch.DrawString(font, timeText, timePosition, fontColor, 0, Vector2.Zero, fontSize, SpriteEffects.None, 0);
            _spriteBatch.DrawString(font, bestTimeText, bestTimePosition, fontColor, 0, Vector2.Zero, fontSize, SpriteEffects.None, 0);

            if (inGame == false)
            {
                slimes.Clear();
                heavyslime.Clear();
                balls.Clear();
                string menuMessage = "Press Enter to Begin!";
                Vector2 sizeOfText = font.MeasureString(menuMessage);
                _spriteBatch.DrawString(font, menuMessage, titlePos, fontColor, 0, Vector2.Zero, fontSize, SpriteEffects.None, 0);
            }


            player.Draw(_spriteBatch);
            foreach (Slime slime in slimes)
            {
                slime.Draw(_spriteBatch);
            }
            foreach (HeavySlime heavySlime in heavyslime)
            {
                heavySlime.Draw(_spriteBatch);
            }
            foreach (Ball ball in balls)
            {
                ball.Draw(_spriteBatch);
            }
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}