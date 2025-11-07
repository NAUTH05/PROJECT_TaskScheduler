namespace MyProject
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            while (true)
            {
                if (AuthManager.LoadToken())
                {
                    var mainForm = new MainForm(AuthManager.UserName, AuthManager.UserId);
                    Application.Run(mainForm);
                    
                    if (!AuthManager.IsLoggedIn())
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    var loginForm = new Login();
                    var result = loginForm.ShowDialog();
                    
                    if (result == DialogResult.OK)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}