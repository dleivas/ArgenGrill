using System.Collections.Generic;

namespace Argengrill.Core
{
    public interface IMergedData
    {
        bool MergedDataIgnore { get; set; }
        Dictionary<string, object> MergedDataValues { get; }
    }
}