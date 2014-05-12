using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using Octokit;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace adovipavto
{
    public partial class SendBugReportForm : Form
    {
        private readonly Exception _exeption;

        public SendBugReportForm(Exception exeption)
        {
            InitializeComponent();
            _exeption = exeption;
        }

        private const string OwnerName = "natashalysakova";
        private const string RepoName = "DiagnostikaPlus";
        GitHubClient github = new GitHubClient(new ProductHeaderValue("DiagnostikaPlus"))
        {
            Credentials = new Credentials("f441afccab8a65378e8ee44af0a2df832beb1210")
        };


        async private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                await github.Issue.Create(OwnerName, RepoName, new NewIssue("[AutomaticBagReport]" + _exeption.Message)
                {
                    Assignee = OwnerName,
                    Labels = {"UserBagReport", "Bug", "СРОЧНО!"},
                    Body = richTextBox1.Text
                });

            }
            catch (AuthorizationException)
            {
                MessageBox.Show("Невозможно отправить отчет об ошибке. Неверный токен. Обратитесь к системному администратору.");
            }
            finally
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_exeption.HelpLink != null) richTextBox1.AppendText(_exeption.HelpLink);
            richTextBox1.AppendText(_exeption.Message);
            if (_exeption.Source != null) richTextBox1.AppendText(_exeption.Source);
            if (_exeption.StackTrace != null) richTextBox1.AppendText(_exeption.StackTrace);
            richTextBox1.AppendText(_exeption.Data.Count.ToString());
            richTextBox1.AppendText(_exeption.HResult.ToString());
            if (_exeption.TargetSite != null) richTextBox1.AppendText(_exeption.TargetSite.ToString());

            timer1_Tick(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Height == 255)
            {
                this.Height = 425;
                button2.Text = "Подробности ↑↑↑";

            }
            else
            {
                this.Height = 255;
                button2.Text = "Подробности ↓↓↓";
            }
        }

        async private void timer1_Tick(object sender, EventArgs e)
        {
            Ping ping = new Ping();
            try
            {
                await ping.SendPingAsync("github.com");
                button1.Enabled = true;
            }
            catch (PingException)
            {
                button1.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}