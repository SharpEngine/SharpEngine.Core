using SharpEngine.Core.Utils;
using SharpEngine.Core.Utils.Tween;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpEngine.Core.Manager
{
    /// <summary>
    /// Class which manage tweens
    /// </summary>
    public class TweenManager
    {
        /// <summary>
        /// Gets or sets the collection of active tweens managed by this instance.
        /// </summary>
        /// <remarks>The list contains all currently tracked tween animations. Modifying this collection
        /// directly may affect the behavior of the tween manager.</remarks>
        public List<Tween> Tweens { get; set; } = [];

        internal void Update(float delta)
        {
            for (var i = Tweens.Count - 1; i > -1; i--)
            {
                if (Tweens[i].Update(delta))
                    Tweens.RemoveAt(i);
            }
        }
    }
}
