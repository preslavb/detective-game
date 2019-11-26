using System.Collections.Generic;
using Model.BoardItemModels;
using Model.Interfaces;

namespace Model
{
    public class Board
    {
        public delegate void BoardItemInfoDelegate(BoardItemSerializable itemSerializable);

        public Board()
        {
            _boardItems = new List<BoardItemSerializable>();
        }
        
        private List<BoardItemSerializable> _boardItems;

        public void InsertItem(BoardItemSerializable item)
        {
            _boardItems.Add(item);
            DidInsertItem?.Invoke(item);
        }

        public void DeleteItem(BoardItemSerializable item)
        {
            _boardItems.Remove(item);
            DidDeleteItem?.Invoke(item);
        }

        public event BoardItemInfoDelegate DidInsertItem;
        public event BoardItemInfoDelegate DidDeleteItem;
    }
}