using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Clocks.Widgets
{
    class Clock2 : SKCanvasView
    {
        public static readonly BindableProperty CircleColorProperty = BindableProperty.Create(nameof(CircleColor),
            typeof(Color), typeof(Clock2), Color.Blue,
            propertyChanging: (currentControl, oldValue, newValue) =>
            {
                var thisControl = currentControl as Clock2;
                thisControl.CircleColor = (Color)newValue;
            });

        public static readonly BindableProperty HeadColorProperty = BindableProperty.Create(nameof(HeadColor),
            typeof(Color), typeof(Clock2), Color.LightGray,
            propertyChanging: (currentControl, oldValue, newValue) =>
            {
                var thisControl = currentControl as Clock2;
                thisControl.HeadColor = (Color)newValue;
            });

        public static readonly BindableProperty CircleStrokeWidthProperty = BindableProperty.Create(nameof(CircleStrokeWidth),
            typeof(float), typeof(Clock2), (float)4,
            propertyChanging: (currentControl, oldValue, newValue) =>
            {
                var thisControl = currentControl as Clock2;
                thisControl.CircleStrokeWidth = (float)newValue;
            });

        private SKColor myCircleColor;
        private SKColor headColor;
        private SKColor myBackgroundColor;
        private float myCircleStrokeWidth;

        public Clock2()
        {
            BackgroundColor = myBackgroundColor.ToFormsColor();
            HeadColor = Color.LightGray;
            CircleColor = Color.Blue;
            CircleStrokeWidth = 4;
        }

        public Color CircleColor
        {
            get { return (Color)GetValue(CircleColorProperty); }
            set
            {
                SetValue(CircleColorProperty, value);
                myCircleColor = value.ToSKColor();
                InvalidateSurface();
            }
        }

        public float CircleStrokeWidth
        {
            get { return (float)GetValue(CircleStrokeWidthProperty); }
            set
            {
                SetValue(CircleStrokeWidthProperty, value);
                myCircleStrokeWidth = value;
                InvalidateSurface();
            }
        }

        public Color HeadColor
        {
            get { return (Color)GetValue(HeadColorProperty); }
            set
            {
                SetValue(HeadColorProperty, value);
                headColor = value.ToSKColor();
                InvalidateSurface();
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(BackgroundColor))
            {
                myBackgroundColor = BackgroundColor.ToSKColor();
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(myBackgroundColor);

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                TextSize = 48,
                StrokeWidth = myCircleStrokeWidth,
                IsAntialias = true
            };

            var circleRadius = Math.Min(info.Width - myCircleStrokeWidth, info.Height - myCircleStrokeWidth) / 2;
            var circleMiddle = circleRadius + myCircleStrokeWidth / 2;
            paint.Color = headColor;
            canvas.DrawCircle(circleMiddle, circleMiddle, circleRadius, paint);
            paint.Style = SKPaintStyle.Stroke;
            paint.Color = myCircleColor;
            canvas.DrawCircle(circleMiddle, circleMiddle, circleRadius, paint);
        }
    }
}
