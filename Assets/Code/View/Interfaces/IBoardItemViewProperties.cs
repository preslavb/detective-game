using UnityEngine;
using View.Scripts.Identifiers;

namespace View.Interfaces
{
    public interface IBoardItemViewProperties
    {
        ViewIdentifierScript ViewIdentifierScript { get; }
        Vector2? StartingPosition { get; } 
    }
}