namespace PPObjects
{
    public class PPBannedUserException : PPException
    {
        public string UserNameBaned { get; set; }

        public PPBannedUserException(string userNameBaned)
        {
            UserNameBaned = userNameBaned;
        }
    }
}