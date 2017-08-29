using System;

namespace Argengrill.Core
{
    public interface ITransient
    {
        bool IsTransient { get; set; }
    }
}