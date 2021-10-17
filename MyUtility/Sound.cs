namespace MyUtility
{
    public class Sound
    {
        private System.Media.SoundPlayer Player { get; }

        public Sound(string wavFilePath)
        {
            CheckExistFile(wavFilePath);
            Player = new System.Media.SoundPlayer(wavFilePath);

            // 初期化に時間かかるので前もって完了させておく
            Player.Play();
            Player.Stop();
        }

        public void CheckExistFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new System.IO.FileNotFoundException(filePath);
            }
        }

        public void Play()
        {
            Player.Stop();    // 重複して鳴らさないようにする
            Player.Play();
        }
    }
}
