using Runtime.Commands.Level;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform levelHolder;
        [SerializeField] private byte totalLevelCount;
        
        private OnLevelLoaderCommand _levelLoaderCommand;
        private OnLevelDestroyerCommand _levelDestroyerCommand;
        
        private short _currentLevel;
        private LevelData _levelData;

        private void Awake()
        {
             _levelData = GetLevelData();
            _currentLevel = GetActiveLevel();
            
            Init();
        }

        private void Start()
        {
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Start, 0);
        }
        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();
        
        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[_currentLevel];
        }
        private void Init()
        {
            _levelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
            _levelDestroyerCommand = new OnLevelDestroyerCommand(levelHolder);
        }
        private byte GetActiveLevel()
        {
            return (byte)(_currentLevel % totalLevelCount);
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnLevelInitialize += _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.OnClearActiveLevel += _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.OnGetLevelValue += OnGetLevelValue;
            CoreGameSignals.Instance.OnNextLevel += OnNextLevel;
            CoreGameSignals.Instance.OnRestartLevel += OnRestartLevel;
        }
        private byte OnGetLevelValue()
        {
            return (byte)_currentLevel;
        }
        private void OnNextLevel()
        {
            _currentLevel++;
            CoreGameSignals.Instance.OnClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.OnReset?.Invoke();
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        }
        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.OnClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.OnReset?.Invoke();
            CoreGameSignals.Instance.OnLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.OnLevelInitialize -= _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.OnClearActiveLevel -= _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.OnGetLevelValue -= OnGetLevelValue;
            CoreGameSignals.Instance.OnNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.OnRestartLevel -= OnRestartLevel;
        }
    }
}