using GirdSystem;
using UnityEngine;

namespace Character
{
    public abstract class BaseGridCharacter : MonoBehaviour, IGridObject
    {
        public BaseGrid<IGridObject> Grid { get; set; }
        public Vector2Int GridPosition { get; set; }


        public void Move(Vector2Int newPosition)
        {
            IGridObject newPositionObj = Grid.GetValue(newPosition.x, newPosition.y);
            if (newPositionObj != null)
            {
                newPositionObj.Interact();
            }
            UpdatePosition(newPosition);
            
        }
        protected void Start()
        {
            InitGridObject();
        }

        public abstract void Interact();
        protected virtual void InitGridObject()
        {
            Grid = GridGenerator.Instance.Grid;
            Grid.GetGridXY(transform.position, out int x, out int y);
            UpdatePosition(new Vector2Int(x, y));
        }
        
        protected void UpdatePosition(Vector2Int newPosition)
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
        
        
    }
}