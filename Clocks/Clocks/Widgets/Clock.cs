using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Clocks.Widgets
{
    class Clock : SKCanvasView
    {
        private SKColor headColor;
        private SKColor clockFaceColor;
        private TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;

        public static readonly BindableProperty HeadColorProperty =
          BindableProperty.Create(nameof(HeadColor), typeof(Color), typeof(Clock), Color.White,
          propertyChanging: (currentControl, oldValue, newValue) =>
          {
              var thisControl = currentControl as Clock;
              thisControl.HeadColor = (Color)newValue;
          });

        public static readonly BindableProperty ClockFaceColorProperty =
          BindableProperty.Create(nameof(HeadColor), typeof(Color), typeof(Clock), Color.LightGray,
          propertyChanging: (currentControl, oldValue, newValue) =>
          {
              var thisControl = currentControl as Clock;
              thisControl.ClockFaceColor = (Color)newValue;
          });

        public static readonly BindableProperty TimeZoneProperty =
          BindableProperty.Create(nameof(ClockTimeZoneInfo), typeof(TimeZoneInfo), typeof(Clock),
          propertyChanging: (currentControl, oldValue, newValue) =>
          {
              var thisControl = currentControl as Clock;
              thisControl.ClockTimeZoneInfo = (TimeZoneInfo)newValue;
          });


        public TimeZoneInfo ClockTimeZoneInfo
        {
            get { return (TimeZoneInfo)GetValue(TimeZoneProperty); }
            set
            {
                SetValue(TimeZoneProperty, value);
                timeZoneInfo = value;
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

        public Color ClockFaceColor
        {
            get { return (Color)GetValue(ClockFaceColorProperty); }
            set
            {
                SetValue(ClockFaceColorProperty, value);
                clockFaceColor = value.ToSKColor();
                InvalidateSurface();
            }
        }

        public Clock()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1f), () =>
            {
                InvalidateSurface();
                return true;
            });
        }


        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.Transparent);

            int width = e.Info.Width;
            int height = e.Info.Height;

            //Set transforms
            canvas.Translate(width / 2, height / 2);
            canvas.Scale(width / 210f, height / 210f);

            dateTime = TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);
            //timeZoneLabel.Text = tz[zone].Id;


            SKPaint backFillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = clockFaceColor
            };

            //Clock background
            canvas.DrawCircle(0, 0, 100, backFillPaint);

            DrawClockFace(canvas, width, dateTime);
            DrawHands(canvas, width, dateTime);
        }

        private void DrawClockFace(SKCanvas canvas, int width, DateTime dateTime)
        {
            SKPaint whiteFillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = headColor
            };

            //Hour and minute marks
            for (int angle = 0; angle < 360; angle += 6)
            {
                canvas.DrawCircle(0, -90, angle % 30 == 0 ? 4 : 2, whiteFillPaint);
                canvas.RotateDegrees(6);
            }
        }



        private void DrawHands(SKCanvas canvas, int width, DateTime dateTime)
        {
            SKPaint StrokePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = headColor,
                StrokeWidth = 2,
                StrokeCap = SKStrokeCap.Round,
                IsAntialias = true
            };

            //Hour hands
            canvas.Save();
            canvas.RotateDegrees(30 * dateTime.Hour + dateTime.Minute / 2f);
            StrokePaint.StrokeWidth = width / 70;
            canvas.DrawLine(0, 0, 0, -50, StrokePaint);
            canvas.Restore();

            //Minute Hands
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Minute + dateTime.Second / 10f);
            StrokePaint.StrokeWidth = width / 110;
            canvas.DrawLine(0, 0, 0, -70, StrokePaint);
            canvas.Restore();

            //Second Hands
            canvas.Save();
            float seconds = dateTime.Second + dateTime.Millisecond / 1000f;
            canvas.RotateDegrees(6 * seconds);
            StrokePaint.StrokeWidth = width / 150;
            canvas.DrawLine(0, 10, 0, -80, StrokePaint);
            canvas.Restore();
        }
    }
}