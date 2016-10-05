using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Solitare
{
    /// <summary>
    /// Abstract class for objects that intend on being drawn.
    /// </summary>
    abstract class Drawable
    {
        protected Rectangle location;

        /// <summary>
        /// Called for logical operations.
        /// </summary>
        /// <param name="gameTime">Time state of the game</param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Called to draw textures to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {

        }

        /// <summary>
        /// Called to draw the selected texture to the screen.
        /// </summary>
        /// <param name="spriteBatch">Used to draw textures to the screen</param>
        /// <param name="texture">Texture to draw from</param>
        public virtual void DrawSelected(SpriteBatch spriteBatch, Texture2D texture)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public virtual bool Contains(Vector2 position)
        {
            return position.X >= location.X && position.X <= location.X + location.Width &&
                position.Y >= location.Y && position.Y <= location.Y + location.Height;
        }
    }
}
