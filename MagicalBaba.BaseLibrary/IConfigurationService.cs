namespace MagicalBaba.BaseLibrary
{
    public interface IConfigurationService
    {
        Configuration GetConfiguration();
        void Reset();
        void SaveConfiguration(Configuration configuration);
    }
}