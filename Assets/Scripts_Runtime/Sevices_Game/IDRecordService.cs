namespace Alter {

    public class IDRecordService {

        int cellEntityID;

        public IDRecordService() {
            cellEntityID = 0;
        }

        public int PickCellEntityID() {
            return ++cellEntityID;
        }

        public void Reset() {
            cellEntityID = 0;
        }
    }

}