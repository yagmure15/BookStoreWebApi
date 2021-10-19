using System;

namespace BookStoreWebApi.Services
{
    public class DbLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DB LOGGER: ] - " +message );
        }
    }
}