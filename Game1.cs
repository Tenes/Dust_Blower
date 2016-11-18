using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Particles
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Particles> particles;
        private Random rng;
        private MouseState mouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            this.IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            base.Initialize();
            this.rng = new Random();
            this.particles = new List<Particles>();
            for (int i = 0; i < 200000; i++)
            {
                this.particles.Add(new Particles(new Vector2(rng.Next(graphics.PreferredBackBufferWidth) + 1,rng.Next(graphics.PreferredBackBufferHeight) + 1)));
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadSprites(this.Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var delta = (float) gameTime.ElapsedGameTime.Seconds;
            this.mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(delta, new Vector2(mouseState.X, mouseState.Y));
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            this.spriteBatch.Begin();
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
