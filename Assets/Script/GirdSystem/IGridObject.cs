using Character;
using UnityEngine;

namespace GirdSystem
{
    public interface IGridObject
    {
        BaseGrid<IGridObject> Grid { get; set; }
        Vector2Int GridPosition { get; set; }
        void Move(Vector2Int newPosition);
        void Interact();
    }
}