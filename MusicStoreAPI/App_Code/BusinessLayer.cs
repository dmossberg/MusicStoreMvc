
/// <summary>
/// Summary description for BusinessLayer
/// </summary>
/// 
namespace MvcMusicStore.API
{
    public class BusinessLayer
    {
        public static object _syncRoot = new object();

        public BusinessLayer()
        {
        }

        public string PerformSomeBusinessOperation(int i)
        {
            lock (_syncRoot)
            {
                //do stuff
                System.Threading.Thread.Sleep(i);
                return System.DateTime.Now.ToShortDateString();
            }
        }

        public string GetSomeResults(int i)
        {
            string strResults = "";
            for (int j = 0; j < i; j++)
            {
                strResults += "<NewID>" + j + "</NewID>";
            }
            return strResults;
        }
    }
}