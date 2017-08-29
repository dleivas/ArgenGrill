using System;

namespace Argengrill.Core.Infrastructure
{
    public interface IOrdered
    {
        // TODO: (MC) Make Nullable!
        int Ordinal { get; }
    }
}