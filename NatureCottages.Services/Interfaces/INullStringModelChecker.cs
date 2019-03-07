using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Services.Interfaces
{
    public interface INullStringModelChecker
    {
        bool CheckStringsForNullOrEmpty<T>(T obj);
    }
}
