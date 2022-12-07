namespace TO_BDD.Providers
{
    public class LoginStateProvider
    {
        private bool isUserLoggedIn = false;

        public void LogUserIn() => isUserLoggedIn = true;

        public void LogUserOut() => isUserLoggedIn = false;

        public bool IsUserLoggedIn() => isUserLoggedIn;
    }
}
