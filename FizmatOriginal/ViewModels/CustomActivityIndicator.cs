﻿using System.Threading.Tasks;
using Xamarin.Forms;

namespace FizmatOriginal.ViewModels
{
    public class CustomActivityIndicator : ContentView
    {
        private const string AnimationName = "ActivityIndicatorRotation";

        public static readonly BindableProperty IsRunningProperty =
            BindableProperty.Create(nameof(IsRunning), typeof(bool),
                typeof(CustomActivityIndicator), default(bool));

        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(ImageSource),
                typeof(CustomActivityIndicator),
                default(ImageSource));

        private readonly Animation _animation;

        private Image _image;

        public CustomActivityIndicator()
        {
            InitView();

            _animation = new Animation(v => Rotation
                = v, 0, 360);
        }

        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            set => SetValue(IsRunningProperty, value);
        }

        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(IsRunning) && IsEnabled)
            {
                if (IsRunning)
                {
                    StartAnimation();
                }
                else
                {
                    _ = StopAnimationAsync();
                }
            }

            if (propertyName == nameof(IsEnabled) && !IsEnabled && IsRunning)
            {
                _ = StopAnimationAsync();
            }

            if (propertyName == nameof(Source))
            {
                _image.Source = Source;
            }
        }

        private void InitView()
        {
            _image = new Image();
            Content = _image;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            WidthRequest = 50;
            HeightRequest = 50;
            Source = "rotate.png";
            Scale = 0;
        }

        private void StartAnimation()
        {
            this.ScaleTo(1, 500);
            _animation.Commit(this, AnimationName, 16, 1000,
                Easing.Linear,
                (v, c) => Rotation = 0, () => true);
        }

        private async Task StopAnimationAsync()
        {
            _ = await this.ScaleTo(0, 500);
            this.AbortAnimation(AnimationName);
        }
    }
}
