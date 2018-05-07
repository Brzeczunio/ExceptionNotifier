using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queris.ExceptionNotifier.Common.Abstract
{
    public interface IConfig
    {
        string Read(string key);
    }
}
