﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnicornEngine;

namespace MainGame
{
    public class VoidScene : Scene
    {
        Sprite sun;

        public override Scene GetReloadedScene()
        {
            return this;
        }

        public VoidScene(int set, Action<int> exit) : base(exit)
        {
            Canvas.elements.Add(new Sprite(Canvas, "Common/MainMenu/BGColor", 100, Vector2.Zero));
            Canvas.elements.Add(sun = new Sprite(Canvas, "Common/MainMenu/Sun", 200, Vector2.One * -50));
            Canvas.elements.Add(new Sprite(Canvas, "Common/MainMenu/BackGround", 100, Vector2.Zero));
        }

        public override void Draw()
        {
            sun.rotation += 0.1f;
            Vector2 offset = Vector2.One * sun.scale / 2;
            offset = Vector2.Transform(offset, Matrix.CreateRotationZ((float)Math.PI * (sun.rotation - 180) / 180));
            sun.pos = new Vector2(70, 18) + offset;
            base.Draw();
        }
    }
}
