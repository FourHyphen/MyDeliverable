using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfDeliverable
{
    public partial class MusicWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _MusicName;

        public string MusicNameStr
        {
            get => _MusicName;
            private set => SetProperty(ref _MusicName, value);
        }

        private double _MusicTime;

        public double MusicTime
        {
            get => _MusicTime;
            private set => SetProperty(ref _MusicTime, value);
        }

        private double _MusicPostion;

        public double MusicPosition
        {
            // メッセージ: Timer か何かで MusicMedia.Position を定期取得して、それを画面に反映させる必要あり
            get => _MusicPostion;
            set => SetProperty(ref _MusicPostion, value);
        }

        private void SetProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MusicWindow(string wavFilePath = "../../Resource/carenginestart1.wav")
        {
            InitializeComponent();
            DataContext = this;
            InitMusicName(wavFilePath);
            InitMediaElement(System.IO.Path.GetFullPath(wavFilePath));
            InitProgress();
        }

        private void InitMusicName(string filePath)
        {
            MusicNameStr = System.IO.Path.GetFileName(filePath);
        }

        private void InitMediaElement(string wavFilePath)
        {
            CheckExistDataFile(wavFilePath);

            MusicMedia.LoadedBehavior = MediaState.Manual;
            MusicMedia.Source = new Uri(wavFilePath);

            // 最初の処理に時間かかるので前もって準備完了させる
            MusicMedia.Play();
            MusicMedia.Stop();
        }

        private void CheckExistDataFile(string wavFilePath)
        {
            if (!System.IO.File.Exists(wavFilePath))
            {
                throw new System.IO.FileNotFoundException(wavFilePath);
            }
        }

        private void InitProgress()
        {
            // メッセージ: 曲の長さは Windows 系の処理でファイルプロパティにアクセスするしかなさそう
            // 現時点で MusicTime = 0 = 曲時間 0 なので ProgressBar.Value は 100% になる
            //MusicTime = MusicMedia.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            PlayAsync();
        }

        private void PlayAsync()
        {
            OperateMusicMedia(MusicMedia.Play);
        }

        private async void OperateMusicMedia(Action action)
        {
            // こうではない、スレッドアフィニティの解決方法
            //MusicMedia.Dispatcher.Invoke(new Action(() =>
            //{
            //    MusicMedia.Play();
            //}));

            SynchronizationContext uiContext = SynchronizationContext.Current;
            await Task.Run(() =>
            {
                uiContext.Post(_ => { action(); }, null);
            });
        }

        private void PauseButtonClick(object sender, RoutedEventArgs e)
        {
            Pause();
        }

        private void Pause()
        {
            OperateMusicMedia(MusicMedia.Pause);
        }

        private void StopButtonClick(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            OperateMusicMedia(MusicMedia.Stop);
        }

        private void MusicMediaEnded(object sender, RoutedEventArgs e)
        {
            Stop();
        }
    }
}
