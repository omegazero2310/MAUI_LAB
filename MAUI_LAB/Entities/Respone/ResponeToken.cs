namespace MAUI_LAB.Entities.Respone
{
    public class ResponeToken
    {
        public string Token
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public Guid Id { get; set; }
        public TimeSpan Validaty
        {
            get;
            set;
        }
        public string RefreshToken
        {
            get;
            set;
        }
        public string EmailId
        {
            get;
            set;
        }
        public Guid GuidId
        {
            get;
            set;
        }
        public DateTime ExpiredTime
        {
            get;
            set;
        }
    }
}
