using System;
using Character;
using Singleton;
using UnityEditor;
using UnityEngine;

namespace GirdSystem
{
    public class GridGenerator : Singleton<GridGenerator>
    {
        [SerializeField] private Vector3 _originPosition;
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private float _cellSize = 10f;
        [SerializeField] private bool _isGridVisualize = true;
        [SerializeField] private bool _isGridLabelsVisualize = true;
        public BaseGrid<IGridObject> Grid { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Grid = new BaseGrid<IGridObject>(_gridSize.x, _gridSize.y, _cellSize, _originPosition);
        
        }
        
        public BaseGridCharacter SpawnGridCharacter(BaseGridCharacter prefab, Vector2Int spawnPosition)
        {
            BaseGridCharacter newCharacter = 
                Instantiate(prefab, Grid.GetCellCenterPosition(spawnPosition.x, spawnPosition.y), Quaternion.identity);
            newCharacter.InitGridObject();
            return newCharacter;
        }

        private void OnDrawGizmos()
        {
            if (_isGridVisualize == false) return;

#if UNITY_EDITOR
            Handles.color = Color.red;

            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    Handles.DrawLine(new Vector3(x, y) * _cellSize + _originPosition, 
                        new Vector3(x, y+1) * _cellSize + _originPosition);
                    Handles.DrawLine(new Vector3(x, y) * _cellSize + _originPosition, 
                        new Vector3(x+1, y) * _cellSize + _originPosition);
                    if (!_isGridLabelsVisualize) continue;

                    Handles.Label(new Vector3(x, y) * _cellSize + _originPosition + new Vector3(_cellSize, _cellSize) * .5f, x + "," + y);
                }
            }
            Handles.DrawLine(new Vector3(0, _gridSize.y) * _cellSize + _originPosition,
                new Vector3(_gridSize.x, _gridSize.y) * _cellSize + _originPosition);
            Handles.DrawLine(new Vector3(_gridSize.x, 0) * _cellSize + _originPosition, 
                new Vector3(_gridSize.x, _gridSize.y) * _cellSize + _originPosition);
#endif
        }
    }
}