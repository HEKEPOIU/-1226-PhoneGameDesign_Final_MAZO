using System;
using GirdSystem;
using UnityEngine;

namespace Character
{
    public abstract class BaseGridCharacter : MonoBehaviour, IGridObject
    {
        public BaseGridCharacter Owner { get; set; }
        public BaseGrid<IGridObject> Grid { get; set; }
        public Vector2Int GridPosition { get; set; }
        public event Action<BaseGridCharacter> OnDeath;

        public virtual void Move(Vector2Int newPosition)
        {
            IGridObject newPositionObj = Grid.GetValue(newPosition.x, newPosition.y);
            if (newPositionObj != null)
            {
                newPositionObj.Interact(this);
                InteractOther(newPositionObj);
            }
            
            UpdatePosition(newPosition);
            
        }

        public virtual void Interact(IGridObject interacter){}
        
        public virtual void InteractOther(IGridObject target){}

        public virtual void InitGridObject()
        {
            Grid = GridGenerator.Instance.Grid;
            Grid.GetGridXY(transform.position, out int x, out int y);
            Move(new Vector2Int(x, y));
        }
        
        public virtual void InitGridObject(Vector2Int spawnPosition)
        {
            Grid = GridGenerator.Instance.Grid;
            Move(spawnPosition);
        }
        
        protected virtual void UpdatePosition(Vector2Int newPosition)
        {
            if (Grid.IsInGrid(newPosition.x, newPosition.y) == false || this.gameObject == null)
            {
                return;
            }
            Vector3 newTransformPosition = Grid.GetCellCenterPosition(newPosition.x, newPosition.y);
            transform.position = newTransformPosition;
            
            Grid.SetValue(GridPosition.x, GridPosition.y, null);
            GridPosition = newPosition;
            Grid.SetValue(newPosition.x, newPosition.y, this);
            
            
        }
        
        /// <summary>
        /// 為啥不用Destroy呢，因為Destroy會在這個Frame結束後才執行，所以會有一個Frame的延遲，
        /// 然後就會有可能導致在這個Frame結束前，新的Character上了Grid，然後就會導致下一個Frame新的Character所設定的位置被移除。
        /// </summary>
        public void DestroyGridCharacter()
        {

            if (this == null) return;
            RemoveFromGrid();
            OnDeath?.Invoke(this);
            OnDeath = null;
            Destroy(gameObject);
        }
        
        
        private void RemoveFromGrid()
        {
            Grid.SetValue(GridPosition.x, GridPosition.y, null);
        }
        
    }
}