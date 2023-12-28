using Singleton;
using UnityEngine;

namespace GirdSystem
{
    public class GridGenerator : Singleton<GridGenerator>
    {
        [SerializeField] private Vector3 _originPosition;
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private float _cellSize = 10f;
        public BaseGrid<IGridObject> Grid { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Grid = new BaseGrid<IGridObject>(_gridSize.x, _gridSize.y, _cellSize, _originPosition);
        
        }
    }
}