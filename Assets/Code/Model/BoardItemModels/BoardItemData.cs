using Model.Interfaces;

namespace Model.BoardItemModels
{
    public abstract class BoardItemData: BoardItemSerializable
    {
        public override string Name => name;

        public virtual void Update(){}
    }
}
