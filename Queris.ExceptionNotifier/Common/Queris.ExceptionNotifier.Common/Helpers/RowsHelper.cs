namespace Queris.ExceptionNotifier.Common.Helpers
{
    public static class RowsHelper
    {
        public static int MaxRowsToGet(int rowsReaded, ref int initialCounter)
        {
            int res;
            if (rowsReaded.Equals(0) || rowsReaded < initialCounter || rowsReaded == initialCounter)
                { res = 0; }
            else
                { res = rowsReaded - initialCounter;}
            
            initialCounter = rowsReaded;
            return res;
        }
    }
}