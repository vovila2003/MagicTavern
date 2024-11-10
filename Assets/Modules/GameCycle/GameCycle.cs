using System.Collections.Generic;
using Tavern.Architecture.GameManager;
using Tavern.Architecture.GameManager.Interfaces;
using UnityEngine;

namespace Modules.GameCycle
{
    public class GameCycle
    {
        private readonly List<IGameListener> _listeners = new();
        private readonly List<IUpdateListener> _updateListeners = new();
        private readonly List<IFixedUpdateListener> _fixedUpdateListeners = new();
        private readonly List<ILateUpdateListener> _lateUpdateListeners = new();
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
            IEnumerable<IGameListener> listeners, 
            IEnumerable<IUpdateListener> updateListeners, 
            IEnumerable<IFixedUpdateListener> fixedUpdateListeners, 
            IEnumerable<ILateUpdateListener> lateUpdateListeners)
        {
            AddListeners(listeners, updateListeners, fixedUpdateListeners, lateUpdateListeners);
            InitGame();
        }

        public void Tick(float time)
        {
            for (var i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(time);
            }
        }

        public void FixedTick(float time)
        {
            for (var i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(time);
            }
        }

        public void LateTick(float time)
        {
            for (var i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnLateUpdate(time);
            }
        }

        private static void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void AddListeners(
            IEnumerable<IGameListener> listeners, 
            IEnumerable<IUpdateListener> updateListeners,
            IEnumerable<IFixedUpdateListener> fixedUpdateListeners,
            IEnumerable<ILateUpdateListener> lateUpdateListeners)
        {
            _listeners.AddRange(listeners);
            _updateListeners.AddRange(updateListeners);
            _fixedUpdateListeners.AddRange(fixedUpdateListeners);
            _lateUpdateListeners.AddRange(lateUpdateListeners);
        }
    }
}