using System;
using System.Collections.Generic;
using System.Text;

namespace Clocks.Services
{
    public interface ISQLite
    {      
        string GetDatabasePath(string filename);       
    }
}
