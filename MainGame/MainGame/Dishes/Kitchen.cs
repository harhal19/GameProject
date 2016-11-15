﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnicornEngine;

namespace MainGame
{
    class Kitchen : Dishes
    {
        int set = 0;

        List<Dish> dishes;

        Effect shader;

        void ColorOn(int i)
        {
            if (!(bool)dishes[i].value)
            {
                dishes[i].pos -= dishes[i].size * 0.2f;
                dishes[i].scale *= 1.4f;
                dishes[i].value = true;
            }
        }

        void ColorOff(int i)
        {
            if ((bool)dishes[i].value)
            {
                dishes[i].pos = dishes[i].posOld;
                dishes[i].scale = dishes[i].scaleOld;
                dishes[i].value = false;
            }
        }

        public Kitchen(int set, Action<int> exit) : base(set, exit)
        {
            this.set = set;
            Canvas.elements.Add(
                new Sprite(Canvas, "Dishes/Kitchen/BG", 100, Vector2.Zero));
            Canvas.elements.Add(
                new Sprite(Canvas, "Dishes/Kitchen/Panel", 68, new Vector2(27, 0)));
            dishes = new List<Dish>();
            dishes.Add(new Dish(Canvas, "Dishes/Kitchen/0", 21, new Vector2(-3.5f, 21)));
            dishes.Last().SetCollisionMap("Dishes/Kitchen/Masks/0");
            dishes.Add(new Dish(Canvas, "Dishes/Kitchen/1", 19, new Vector2(18, 13)));
            dishes.Last().SetCollisionMap("Dishes/Kitchen/Masks/1");
            dishes.Add(new Dish(Canvas, "Dishes/Kitchen/2", 23, new Vector2(35, -3)));
            dishes.Last().SetCollisionMap("Dishes/Kitchen/Masks/2");
            dishes.Add(new Dish(Canvas, "Dishes/Kitchen/3", 23, new Vector2(40, -1.5f)));
            dishes.Last().SetCollisionMap("Dishes/Kitchen/Masks/3");
            dishes.Add(new Dish(Canvas, "Dishes/Kitchen/4", 23, new Vector2(48, -0.5f)));
            dishes.Last().SetCollisionMap("Dishes/Kitchen/Masks/4");
            dishes.Add(new Dish(Canvas, "Dishes/Kitchen/5", 21, new Vector2(58, 19)));
            dishes.Last().SetCollisionMap("Dishes/Kitchen/Masks/5");
            dishes.Add(new Dish(Canvas, "Dishes/Kitchen/6", 15, new Vector2(86, 16)));
            dishes.Last().SetCollisionMap("Dishes/Kitchen/Masks/6");
            dishes.Add(new Dish(Canvas, "Dishes/Kitchen/7", 21, new Vector2(82, 27)));
            dishes.Last().SetCollisionMap("Dishes/Kitchen/Masks/7");
            for (int i = 0; i < dishes.Count; i++)
                dishes[i].value = i < set - 2;
            /*effect =*/ shader = EngineCore.content.Load<Effect>("Shaders/BlackWhite");
        }

        public override void Update()
        {
            base.Update();
            for (int i = 0; i < dishes.Count; i++)
                if (dishes[i].UnderMouse())
                {
                    ColorOn(i);
                    if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                        EngineCore.oldMouseState.LeftButton == ButtonState.Released)
                        if (i == set - 2)
                            AddEvent(0.5f, delegate
                            {
                                exit(++set);
                            });
                }
                else
                    ColorOff(i);
        }

        public override void Draw()
        {
            base.Draw();
            Rectangle ORR = EngineCore.graphics.GraphicsDevice.ScissorRectangle;
            RasterizerState RS = new RasterizerState();
            RS.ScissorTestEnable = true;
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            for (int i = 0; i < dishes.Count; i++)
                if ((bool)dishes[i].value)
                    dishes[i].Draw();
            EngineCore.spriteBatch.End();
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS, shader);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            for (int i = 0; i < dishes.Count; i++)
                if (!(bool)dishes[i].value)
                    dishes[i].Draw();
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
        }
    }
}
