using System;
using System.Windows.Threading;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Class for handling timer in game
    /// </summary>
    public class TimerViewModel:ViewModelBase
    {
        //Timer to keep track of length of playing
        private DispatcherTimer _playedTimer;        
        private const int _playSeconds = 1;

        #region Constructor
        public TimerViewModel(TimeSpan time)
        {
            _playedTimer = new DispatcherTimer();
            _playedTimer.Interval = time;
            _playedTimer.Tick += PlayedTimer_Tick;
            _timePlayed = new TimeSpan();
        }
        #endregion        

        /// <summary>
        /// Time spent in one game
        /// </summary>
        private TimeSpan _timePlayed;
        public TimeSpan Time
        {
            get
            {
                return _timePlayed;
            }
            set
            {
                _timePlayed = value;
                OnPropertyChanged("Time");
            }
        }       

        /// <summary>
        /// Starting timer method
        /// </summary>
        public void Start()
        {
            _playedTimer.Start();
        }

        /// <summary>
        /// Stoping timer method
        /// </summary>
        public void Stop()
        {
            _playedTimer.Stop();
        }

        /// <summary>
        /// Event of ticking timer added to time property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayedTimer_Tick(object sender, EventArgs e)
        {
            Time = _timePlayed.Add(new TimeSpan(0, 0, 1));
        }
    }
}
