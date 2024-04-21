namespace Alter {

    public class IDRecordService {

        int cellEntityID;
        int blockEntityID;

        public IDRecordService() {
            cellEntityID = 0;
            blockEntityID = 0;
        }

        public int PickCellEntityID() {
            return ++cellEntityID;
        }

        public int PickBlockEntityID() {
            return ++blockEntityID;
        }

        public void Reset() {
            cellEntityID = 0;
            blockEntityID = 0;
        }
    }

}