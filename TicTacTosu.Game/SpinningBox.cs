using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK.Graphics;

namespace TicTacTosu.Game
{
    public class SpinningBox : CompositeDrawable
    {
        private Container box;
        public double RotationDuration { get; set; }
        public double HoverAnimDuration { get; set; }
        private Color4 unhoveredColor, hoveredColor;
        public DrawableSample HoverSample { get; set; }

        public SpinningBox()
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            RotationDuration = 10000;
            HoverAnimDuration = 300;
            hoveredColor = new Color4(0xff, 0x64, 0x32, 0xff);
            unhoveredColor = new Color4(0x16, 0x18, 0x20, 0xff);
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (HoverSample != null)
                HoverSample.Play();
            box.TransformTo(nameof(BorderThickness), 15f, HoverAnimDuration, Easing.OutCubic);
            box.TransformTo(nameof(BorderColour), (SRGBColour)hoveredColor, HoverAnimDuration, Easing.OutCubic);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (HoverSample != null)
                HoverSample.Play();
            box.TransformTo(nameof(BorderThickness), 5f, HoverAnimDuration, Easing.OutCubic);
            box.TransformTo(nameof(BorderColour), (SRGBColour)unhoveredColor, HoverAnimDuration, Easing.OutCubic);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = box = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Masking = true,
                BorderColour = unhoveredColor,
                BorderThickness = 5,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                    new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Texture = textures.Get("logo")
                    },
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            box.Loop(b => b.RotateTo(0).RotateTo(360, RotationDuration));
        }
    }
}
