using System.Collections.Generic;
using Architecture.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Architecture
{
    public class GameManager :
        IInitializable, 
        ITickable, 
        IFixedTickable,
        ILateTickable
    {
        private readonly IObjectResolver _container;
        private readonly List<IGameListener> _listeners = new();
        private readonly List<IUpdateListener> _updateListeners = new();
        private readonly List<IFixedUpdateListener> _fixedUpdateListeners = new();
        private readonly List<ILateUpdateListener> _lateUpdateListeners = new();
        private GameState _state = GameState.None;
        
        public GameManager(IObjectResolver container)
        {
            _container = container;
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
        
        void IInitializable.Initialize()
        {
            AddListeners();
            InitGame();
        }

        void ITickable.Tick()
        {
            float time = Time.deltaTime;
            for (var i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(time);
            }
        }

        void IFixedTickable.FixedTick()
        {
            float time = Time.fixedDeltaTime;
            for (var i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(time);
            }
        }

        void ILateTickable.LateTick()
        {
            float time = Time.deltaTime;
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
        
        private void AddListeners()
        {
            var listeners = _container.Resolve<IEnumerable<IGameListener>>();
            _listeners.AddRange(listeners);

            var updateListeners = _container.Resolve<IEnumerable<IUpdateListener>>();
            _updateListeners.AddRange(updateListeners);

            var fixedUpdateListeners = _container.Resolve<IEnumerable<IFixedUpdateListener>>();
            _fixedUpdateListeners.AddRange(fixedUpdateListeners);
            
            var lateUpdateListeners = _container.Resolve<IEnumerable<ILateUpdateListener>>();
            _lateUpdateListeners.AddRange(lateUpdateListeners);
        }
    }
}