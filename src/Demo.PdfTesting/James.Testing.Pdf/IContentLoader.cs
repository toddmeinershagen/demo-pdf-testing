using System.Threading.Tasks;

namespace James.Testing.Pdf
{
    public interface IContentLoader
    {
        Task<IContent> ExtractFromAsync(string path);
    }
}
