namespace App.Common.Web.External
{
    public class RequestIDGenerator
    {
        private long m_Id;
        
        public long GetNextID()
        {
            return ++m_Id;
        }
    }
}