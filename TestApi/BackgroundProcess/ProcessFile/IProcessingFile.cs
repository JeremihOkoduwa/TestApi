using System.Threading.Tasks;

namespace TestApi.BackgroundProcess.ProcessFile
{
    public interface IProcessingFile
    {
        Task<(bool, string)> ReadCsv();
    }
}