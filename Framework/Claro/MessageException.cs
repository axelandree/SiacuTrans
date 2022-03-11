using System;

namespace Claro
{
    [Serializable]
    public class MessageException : Exception
    {
        public MessageException()
        {
            throw new ArgumentException();
        }

        public MessageException(string message)
        {
            throw new ArgumentException(message);
        }

        public static string GetOriginalExceptionMessage(Exception ex)
        {
            while (ex.InnerException != null) ex = ex.InnerException;
            return ex.Message;
        }
    }
}