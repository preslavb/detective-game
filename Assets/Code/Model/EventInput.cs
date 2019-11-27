using System;
using System.Linq;
using Model.BoardItemModels;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class EventInput: IEquatable<BoardItemSerializable[]>
    {
        [SerializeField]
        private BoardItemSerializable[] _boardItemInputsRequired;

        public bool Equals(BoardItemSerializable[] other)
        {
            if (other == null || _boardItemInputsRequired.Length != other.Length)
            {
                return false;
            }

            foreach (var boardItem in _boardItemInputsRequired)
            {
                var areEqual = other.Contains(boardItem);

                if (!areEqual) return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EventInput) obj);
        }

        public override int GetHashCode()
        {
            return (_boardItemInputsRequired != null ? _boardItemInputsRequired.GetHashCode() : 0);
        }
    }
}