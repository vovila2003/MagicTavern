using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using UnityEngine;

namespace Modules.GameCycle
{
    [UsedImplicitly]
    public class GameCycle
    {
        private readonly List<IGameListener> _listeners = new();
        
        public GameState State { get; private set; } = GameState.None;

        public void AddListener(IGameListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IGameListener listener)
        {
            _listeners.Remove(listener);
        }

        public void Initialize(
            IEnumerable<IGameListener> listeners)
        {
            AddListeners(listeners);
            InitGame();
        }

        public void PrepareGame()
        {
            if (State != GameState.Initialized && State != GameState.IsFinished)
            {
                Debug.LogWarning($"Can't Prepare game from {State} state!");
                return;
            }
            
            for (var i = 0; i < _listeners.Count; i++)
            {
                IGameListener listener = _listeners[i];
                if (listener is IPrepareGameListener prepareGameListener)
                {
                    prepareGameListener.OnPrepare();
                }
            }
            State = GameState.Ready;
        }

        public void StartGame()
        {
            if (State != GameState.Ready)
            {
                Debug.LogWarning($"Can't Start game from {State} state!");
                return;
            }

            for (var i = 0; i < _listeners.Count; i++)
            {
                IGameListener listener = _listeners[i];
                if (listener is IStartGameListener startGameListener)
                {
                    startGameListener.OnStart();
                }
            }

            State = GameState.IsRunning;
        }

        public void FinishGame()
        {
            if (State != GameState.IsRunning)
            {
                Debug.LogWarning($"Can't Finish game from {State} state!");
                return;
            }
            
            for (var i = 0; i < _listeners.Count; i++)
            {
                IGameListener listener = _listeners[i];
                if (listener is IFinishGameListener finishGameListener)
                {
                    finishGameListener.OnFinish();
                }
            }

            State = GameState.IsFinished;
        }

        public void PauseGame()
        {
            if (State != GameState.IsRunning)
            {
                Debug.LogWarning($"Can't Pause game from {State} state!");
                return;
            }
            
            for (var i = 0; i < _listeners.Count; i++)
            {
                IGameListener listener = _listeners[i];
                if (listener is IPauseGameListener pauseGameListener)
                {
                    pauseGameListener.OnPause();
                }
            }

            State = GameState.Pause;
        }

        public void ResumeGame()
        {
            if (State != GameState.Pause)
            {
                Debug.LogWarning($"Can't Resume game from {State} state!");
                return;
            }
            
            for (var i = 0; i < _listeners.Count; i++)
            {
                IGameListener listener = _listeners[i];
                if (listener is IResumeGameListener resumeGameListener)
                {
                    resumeGameListener.OnResume();
                }
            }

            State = GameState.IsRunning;
        }

        public void ExitGame()
        {
            for (var i = 0; i < _listeners.Count; i++)
            {
                IGameListener listener = _listeners[i];
                if (listener is IExitGameListener exitGameListener)
                {
                    exitGameListener.OnExit();
                }
            }

            State = GameState.None;
            QuitGame();
        }

        private void InitGame()
        {
            if (State != GameState.None)
            {
                Debug.LogWarning($"Can't Init game from {State} state!");
                return;
            }
            
            for (var i = 0; i < _listeners.Count; i++)
            {
                IGameListener listener = _listeners[i];
                if (listener is IInitGameListener initGameListener)
                {
                    initGameListener.OnInit();
                }
            }
            State = GameState.Initialized;
        }

        private static void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void AddListeners(IEnumerable<IGameListener> listeners)
        {
            _listeners.AddRange(listeners);
        }
    }
}