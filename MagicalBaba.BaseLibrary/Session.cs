namespace MagicalBaba.BaseLibrary
{
    internal class Session
    {
        private readonly Communicator _communicator;

        public Session(Configuration configuration, IClient client)
        {
            _communicator = new Communicator(new BasicInterceptor(configuration.SecretHotkey, configuration.QuestionText), client, configuration);
        }

        public string GetReply(CommunicationType communicationType)
        {
            return _communicator.GetReply(communicationType);
        }
    }


    class Communicator
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
