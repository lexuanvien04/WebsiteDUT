using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteDUT.Common
{
    public class Common
    {
        ///<summary>
        ///Ghi log lỗi và quy trình
        ///Input: sPage(Tên của trang cần Log), string sFunction(Tên của hàm cần Log), string sMessage(Nội dung Log)
        ///</summary>

        public static void WriteLog(string sPage, string sFunction, string sMessage)
        {
            try
            {
                string LogPath = HttpContext.Current.Server.MapPath("~/LogInfo");
                if (!System.IO.Directory.Exists(LogPath))
                {
                    System.IO.Directory.CreateDirectory(LogPath);
                }
                CLogger TestLogger = new CLogger();
                TestLogger.Initialize(LogPath, "Web.log", 2);
                TestLogger.LogInformation(sPage, sFunction, sMessage);
                TestLogger.Terminate();

            }
            catch (Exception)
            {

            }
        }
    }
}