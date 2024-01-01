using System;
using GirdSystem;
using Player;
using UnityEngine;

namespace Character
{
    public abstract class BaseGridCharacter : MonoBehaviour, IGridObject
    {
        public BaseGrid<IGridObject> Grid { get; set; }
        public Vector2Int GridPosition { get; set; }
        public event Action OnDeath;
        protected event Action OnInteractOther;

        public virtual void Move(Vector2Int newPosition)
        {
            IGridObject newPositionObj = Grid.GetValue(newPosition.x, newPosition.y);
            if (newPositionObj != null)
            {
                newPositionObj.Interact();
                OnInteractOther?.Invoke();
            }
            UpdatePosition(newPosition);
            
        }
        protected virtual void Start()
        {
            InitGridObject();
        }

        public virtual void Interact(){}

        public virtual void InitGridObject()
        {
            Grid = GridGenerator.Instance.Grid;
            Grid.GetGridXY(transform.position, out int x, out int y);
            Move(new Vector2Int(x, y));
        }
        
        protected virtual void UpdatePosition(Vector2Int newPosition)
        {
            if (Grid.IsInGrid(newPosition.x, newPosition.y) == false)
            {
                print("Out of range");
                return;
            }
            Vector3 newTransformPosition = Grid.GetCellCenterPosition(newPosition.x, newPosition.y);
            transform.position = newTransformPosition;
            
            Grid.SetValue(GridPosition.x, GridPosition.y, null);
            GridPosition = newPosition;
            Grid.SetValue(newPosition.x, newPosition.y, this);
            
            
        }
        
        private void RemoveFromGrid()
        {
            Grid.SetValue(GridPosition.x, GridPosition.y, null);
        }

        protected void OnDestroy()
        {
            RemoveFromGrid();
            OnDeath?.Invoke();
            OnDeath = null;
            OnInteractOther = null;
        }
    }
}