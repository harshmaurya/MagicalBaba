using MagicalBaba.BaseLibrary.DomainModel;

namespace MagicalBaba.BaseLibrary.Interfaces
{
    public interface IConfigurationService
    {
        Configuration GetConfiguration();
        void Reset();
        void SaveConfiguration(Configuration configuration);
    }
}