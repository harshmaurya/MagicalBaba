using MagicalBaba.BaseLibrary.DomainModel;
using MagicalBaba.BaseLibrary.Interfaces;

namespace MagicalBaba.BaseLibrary.Services
{
    public class MagicalService
    {
        public Configuration Configuration { get; set; }
        private Session _session;

        public MagicalService(IConfigurationService configurationService)
        {
            Configuration = configurationService.GetConfiguration();
        }

        public void CreateNewSession(IClient client)
        {
            _session = new Session(Configuration, client);
        }

        public string GetReply(CommunicationType communicationType)
        {
            return _session.GetReply(communicationType);
        }
    }
}
