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
        private GameState _state = GameState.None;

        public void AddListener(IGameListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IGameListener listener)
        {
            _listeners.Remove(listener);
        }
        
        public void PrepareGame()
        {
            if (_state != GameState.Initialized && _state != GameState.IsFinished)
            {
                Debug.LogWarning($"Can't Prepare game from {_state} state!");
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
            _state = GameState.Ready;
        }
        
        public void StartGame()
        {
            if (_state != GameState.Ready)
            {
                Debug.LogWarning($"Can't Start game from {_state} state!");
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

            _state = GameState.IsRunning;
        }
        
        public void FinishGame()
        {
            if (_state != GameState.IsRunning)
            {
                Debug.LogWarning($"Can't Finish game from {_state} state!");
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

            _state = GameState.IsFinished;
        }
        
        public void PauseGame()
        {
            if (_state != GameState.IsRunning)
            {
                Debug.LogWarning($"Can't Pause game from {_state} state!");
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

            _state = GameState.Pause;
        }
        
        public void ResumeGame()
        {
            if (_state != GameState.Pause)
            {
                Debug.LogWarning($"Can't Resume game from {_state} state!");
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

            _state = GameState.IsRunning;
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

            _state = GameState.None;
            QuitGame();
        }

        private void InitGame()
        {
            if (_state != GameState.None)
            {
                Debug.LogWarning($"Can't Init game from {_state} state!");
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
            _state = GameState.Initialized;
        }

        public void Initialize(
            IEnumerable<IGameListener> listeners)
        {
            AddListeners(listeners);
            InitGame();
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