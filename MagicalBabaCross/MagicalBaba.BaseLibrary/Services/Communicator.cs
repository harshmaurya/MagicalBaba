using MagicalBaba.BaseLibrary.DomainModel;
using MagicalBaba.BaseLibrary.Interfaces;

namespace MagicalBaba.BaseLibrary.Services
{
    internal class Communicator
    {
        private readonly IInterceptor _interceptor;
        private readonly Configuration _configuration;
        private CommunicationType _currentCommunicationType = CommunicationType.NotStarted;

        public Communicator(IInterceptor interceptor, IClient client, Configuration configuration)
        {
            _interceptor = interceptor;
            _configuration = configuration;
            client.OnTextEntered += OnClientTextEntered;
        }

        void OnClientTextEntered(object sender, TextEnteredEventArgs e)
        {
            if (_currentCommunicationType != CommunicationType.NotStarted)
            {
                e.OutputText = e.InputText;
                return;
            }
            e.OutputText = _interceptor.Intercept(e.InputText);
        }

        public string GetReply(CommunicationType communicationType)
        {
            _currentCommunicationType = communicationType;
            switch (communicationType)
            {
                case CommunicationType.WakeUp:
                {
                    return _configuration.HelpText;
                }
                case CommunicationType.GetAnswer:
                {
                    var answer = _interceptor.GetSecretText();
                    return string.IsNullOrEmpty(answer) ? _configuration.FailureText : answer;
                }
                default:
                    return string.Empty;
            }
        }
    }
}