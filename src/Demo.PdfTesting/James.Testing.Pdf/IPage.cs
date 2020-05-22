using System.Collections.Generic;

namespace James.Testing.Pdf
{
    public interface IPage
    {
        IList<ILine> Lines { get;  }
    }
}
