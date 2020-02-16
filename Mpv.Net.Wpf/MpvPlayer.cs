using System;
using System.Windows;
using System.Windows.Controls;

namespace Mpv.Net.Wpf
{
    /// <summary>
    /// Mpv Player controller. Main Component
    /// </summary>
    public sealed class MpvPlayer : Control, IDisposable
    {
        private Mpv.NET.Player.MpvPlayer _player;
        private Locker _locker;

        public MpvDisplay Display
        {
            get { return (MpvDisplay)GetValue(DisplayProperty); }
            set { SetValue(DisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Display.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayProperty =
            DependencyProperty.Register("Display", typeof(MpvDisplay), typeof(MpvPlayer), new PropertyMetadata(null, DisplayChange));

        private static void DisplayChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MpvPlayer sender)
                sender.Init();
        }

        private void Init()
        {
            _player = new NET.Player.MpvPlayer(Display.DisplayHandle);
            _player.AutoPlay = false;
            _player.PositionChanged += _player_PositionChanged;
        }

        static MpvPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MpvPlayer), new FrameworkPropertyMetadata(typeof(MpvPlayer)));
        }

        public void Dispose()
        {
            if (_player != null)
            {
                _player.Dispose();
                _player = null;
            }
        }

        public override void OnApplyTemplate()
        {
            _locker = new Locker();

            var playpause = GetTemplateChild("PART_PlayPause") as Button;
            playpause.Click += Playpause_Click;

            var stop = GetTemplateChild("PART_Stop") as Button;
            stop.Click += Stop_Click;

            var volume = GetTemplateChild("PART_Volume") as Slider;
            volume.ValueChanged += Volume_ValueChanged;

            var seek = GetTemplateChild("PART_Seek") as Slider;
            seek.ValueChanged += Seek_ValueChanged;
        }

        private void Seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_locker.IsLocked && _player != null)
            {
                _player.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_player != null)
            {
                _player.Volume = Convert.ToInt32(e.NewValue);
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _player?.Stop();
        }

        private void Playpause_Click(object sender, RoutedEventArgs e)
        {
            if (_player != null)
            {
                if (_player.IsPlaying)
                    _player.Pause();
                else
                    _player.Resume();
            }
        }

        private void _player_PositionChanged(object sender, NET.Player.MpvPlayerPositionChangedEventArgs e)
        {
            _locker.PerformLockAction(() =>
            {
                if (GetTemplateChild("PART_Seek") is Slider seek)
                {
                    seek.Value = e.NewPosition.TotalSeconds;
                }
            });
        }
    }
}
