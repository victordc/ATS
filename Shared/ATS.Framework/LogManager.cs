using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ATS.Framework
{
    public class LogManager
    {
        public static log4net.ILog _ATSlogger;
        public static log4net.ILog ATSLogger
        {
            get
            {
                try
                {
                    if (_ATSlogger == null)
                    {
                        _ATSlogger = log4net.LogManager.GetLogger("ATS.Logger");
                        log4net.Config.XmlConfigurator.Configure();
                    }
                    return _ATSlogger;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static void LogError(Object message)
        {
            try
            {
                ATSLogger.Error(message);
            }
            catch (Exception) { }
        }


        public static void LogError(Object message, Exception ex)
        {
            try
            {
                ATSLogger.Error(message, ex);
            }
            catch (Exception) { }
        }

        public static void LogInfo(Object message)
        {
            try
            {
                ATSLogger.Info(message);
            }
            catch (Exception) { }
        }


        public static void LogObjectToXML(Object obj, String fileName)
        {
            try
            {
                XmlSerializer s = new XmlSerializer(obj.GetType());
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName);
                s.Serialize(sw, obj);
                sw.Close();
            }
            catch (Exception) { }
        }
    }
}
