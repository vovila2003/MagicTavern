using Architecture.Interfaces;
using UnityEngine;
using VContainer;

namespace GameCursor
{
    public sealed class GameCursor : 
        Cursor,
        IPauseGameListener,
        IResumeGameListener,
        IStartGameListener,
        IInitGameListener,
        IFinishGameListener
    {
        private GameCursorSettings _settings;
        private Vector2 _shootCursorOffset;
        
        [Inject]
        private void Construct(GameCursorSettings settings)
        {
            _settings = settings; 
        }
        
        void IPauseGameListener.OnPause() => SetNormalCursor();

        void IResumeGameListener.OnResume() => SetShootCursor();

        void IStartGameListener.OnStart() => SetShootCursor();

        void IInitGameListener.OnInit()
        {
            _shootCursorOffset = 
                new Vector2(_settings.ShootCursor.width, _settings.ShootCursor.height) / 2;
            SetNormalCursor();
        }

        void IFinishGameListener.OnFinish() => SetNormalCursor();

        private void SetShootCursor()
        {
            SetCursor(_settings.ShootCursor, _shootCursorOffset, CursorMode.Auto);
        }

        private void SetNormalCursor()
        {
            SetCursor(_settings.NormalCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}