using FootballExcerciseService.Models;

namespace FootballExcerciseService.Transformers
{
    public interface ITransformerFactory
    {
        ITransformer FetchTransformer(FileExtensionType fileExtensionType);
    }
}