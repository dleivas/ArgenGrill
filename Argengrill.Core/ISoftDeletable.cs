using System;
using System.Collections.Generic;

namespace Argengrill.Core
{
    public interface ISoftDeletable
    {
        bool Deleted { get; }
    }
}