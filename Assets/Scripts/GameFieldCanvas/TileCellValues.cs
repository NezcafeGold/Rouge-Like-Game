using DefaultNamespace;

    public class TileCellValues
    {
        private int xCord, yCord;
        private bool playerIsHere, _isStartPoint, _isVisited;
        private bool isEnableToMove = true;
        private TypeTileCardData typeTileCardData;

        public TileCellValues(int xCord, int yCord)
        {
            this.xCord = xCord;
            this.yCord = yCord;
        }
        
        public int XCord
        {
            get { return xCord; }
            set { xCord = value; }
        }

        public int YCord
        {
            get { return yCord; }
            set { yCord = value; }
        }

        public bool PlayerIsHere
        {
            get { return playerIsHere; }
            set { playerIsHere = value; }
        }

        public bool IsStartPoint
        {
            get { return _isStartPoint; }
            set { _isStartPoint = value; }
        }

        public bool IsVisited
        {
            get { return _isVisited; }
            set { _isVisited = value; }
        }

        public bool IsEnableToMove
        {
            get { return isEnableToMove; }
            set { isEnableToMove = value; }
        }

        public TypeTileCardData TypeTileCardData
        {
            get { return typeTileCardData; }
            set { typeTileCardData = value; }
        }
    }