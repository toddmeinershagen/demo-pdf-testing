using System.Collections.Generic;

namespace James.Testing.Pdf
{
    public interface IContent
    {
        List<IPage> Pages { get; }
    }
}
