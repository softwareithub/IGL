using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IGL.UI.Helper
{
    public static class Message
    {
        public static IConfiguration configuration;
        static Message()
        {
            configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appMessages.json")
                   .Build();
        }
    }
}
