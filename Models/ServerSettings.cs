using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Models
{
    public enum ServerStatus { Start, Stop };
    internal class ServerSettings
    {
        public int Port { get; set; } = 7700;
    }
}
