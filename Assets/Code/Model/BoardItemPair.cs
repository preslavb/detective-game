using System;
using Model.BoardItemModels;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class BoardItemPair: IEquatable<BoardItemPair>
    {
        [SerializeField] private BoardItemSerializable _firstItem;
        [SerializeField] private BoardItemSerializable _secondItem;

        [SerializeField] private bool _isUnique;
        
        public bool HasBeenInstantiated { get; set; }

        public BoardItemPair(BoardItemSerializable firstItem, BoardItemSerializable secondItem)
        {
            _firstItem = firstItem;
            _secondItem = secondItem;
            _isUnique = true;

            HasBeenInstantiated = false;
        }

        public bool Equals(BoardItemPair other)
        {
            return (
                    (Equals(_firstItem, other._firstItem) && Equals(_secondItem, other._secondItem)) ||
                    (Equals(_firstItem, other._secondItem) && Equals(_secondItem, other._firstItem))
                    ) && 
                    !HasBeenInstantiated && !other.HasBeenInstantiated;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return Equals((BoardItemPair) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_firstItem != null ? _firstItem.GetHashCode() : 0)) ^ (_secondItem != null ? _secondItem.GetHashCode() : 0);
            }
        }
    }
}