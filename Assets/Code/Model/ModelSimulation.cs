namespace Model
{
    public class ModelSimulation
    {
        private Board _board;

        public ModelSimulation(BoardItemSerializable[] boardItemsToStartWith)
        {
            _board = new Board();

            foreach (var boardItemSerializable in boardItemsToStartWith)
            {
                _board.InsertItem(boardItemSerializable);
            }
        }
    }
}