using MagicalBaba.BaseLibrary.DomainModel;
using MagicalBaba.BaseLibrary.Interfaces;

namespace MagicalBaba.BaseLibrary.Services
{
    internal class Session
    {
        private readonly Communicator _communicator;
        private readonly Interceptor _interceptor;

        public Session(Configuration configuration, IClient client)
        {
            _interceptor = new Interceptor(configuration.SecretHotkey, configuration.QuestionText);
            _communicator = new Communicator(_interceptor, client, configuration);
        }

        public string GetReply(CommunicationType communicationType)
        {
            return _communicator.GetReply(communicationType);
        }

        public IInterceptor Interceptor => _interceptor;
    }
}
