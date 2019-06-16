namespace MagicalBaba.BaseLibrary
{
    public class MagicalService
    {
        public Configuration Configuration { get; set; }
        private Session _session;

        public MagicalService(Configuration configuration)
        {
            Configuration = configuration;
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
