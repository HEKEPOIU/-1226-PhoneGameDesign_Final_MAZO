using CodeMonkey.Utils;
using UnityEngine;

namespace GirdSystem
{
    public class BaseGrid<T>
    {
        public int Width {get; private set; }
        public int Height {get; private set; }
        private float _cellSize;
        private T[,] _gridArray;
        private Vector3 _originPosition;
    
        public BaseGrid(int width, int height, float cellSize, Vector3 originPosition)
        {
            this.Width = width;
            this.Height = height;
            this._cellSize = cellSize;
            this._originPosition = originPosition;
        
            _gridArray = new T[width, height];

        }
    
        public Vector3 GetWorldPosition(int x, int y)
        {
            if (IsInGrid(x, y))
            {
                return new Vector3(x, y) * _cellSize + _originPosition;
            }

            return Vector3.zero;
        }
    
        public void GetGridXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
            y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return;
            }
            else
            {
                Debug.Log("Out of range, set to MaxValue");
                x = Mathf.Clamp(x, 0, Width - 1);
                y = Mathf.Clamp(y, 0, Height - 1);
            }
        }
    
        public void SetValue(int x, int y, T value)
        {
            if (IsInGrid(x, y))
            {
                _gridArray[x, y] = value;
            }
            else
            {
                Debug.Log("Out of range, set to MaxValue");
                int xMax = Mathf.Clamp(x, 0, Width - 1);
                int yMax = Mathf.Clamp(y, 0, Height - 1);
                _gridArray[xMax, yMax] = value;
                
            }
        }
    
        public void SetValue(Vector3 worldPosition, T value)
        {
            GetGridXY(worldPosition, out int x, out int y);
            SetValue(x, y, value);
        }
    
        public T GetValue(int x, int y)
        {
            if (IsInGrid(x, y))
            {
                return _gridArray[x, y];
            }
            else
            {
                return default(T);
            }
        }
    
        public T GetValue(Vector3 worldPosition)
        {
            GetGridXY(worldPosition, out int x, out int y);
            return GetValue(x, y);
        }
        
        
        /// <summary>
        /// Get the center position of the cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns> if X,Y Not In range, return Vector3.negativeInfinity</returns>
        public Vector3 GetCellCenterPosition(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * .5f;
            }
            else
            {
                return Vector3.negativeInfinity;
            }
            
        }
        
        public bool IsInGrid(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }
    }
}
