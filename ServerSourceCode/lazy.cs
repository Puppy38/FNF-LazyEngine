using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

class LauncherForm : Form
{
    private PictureBox logoBox;
    private Button startButton;
    private TextBox portBox;
    private Label versionLabel;
    private Label portLabel;

    private HttpListener server;
    private Thread serverThread;
    private bool running = false;

    public LauncherForm()
    {
        Text = "Server Launcher";
        Width = 460;
        Height = 340;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Logo
        logoBox = new PictureBox();
        logoBox.Size = new Size(400, 200);
        logoBox.Location = new Point(20, 10);
        logoBox.SizeMode = PictureBoxSizeMode.StretchImage;

        string logoPath = Path.Combine(baseDir, "assets", "logo.png");
        if (File.Exists(logoPath))
            logoBox.Image = Image.FromFile(logoPath);

        // Version from file
        string versionPath = Path.Combine(baseDir, "assets", "version.txt");
        string version = File.Exists(versionPath)
            ? File.ReadAllText(versionPath).Trim()
            : "unknown";

        versionLabel = new Label();
        versionLabel.Text = "Version: " + version;
        versionLabel.AutoSize = true;
        versionLabel.Location = new Point(20, 215);

        // Port input
        portLabel = new Label();
        portLabel.Text = "Port:";
        portLabel.Location = new Point(20, 240);
        portLabel.AutoSize = true;

        portBox = new TextBox();
        portBox.Text = "5500";
        portBox.Location = new Point(60, 237);
        portBox.Width = 80;

        // Start button
        startButton = new Button();
        startButton.Text = "Start Game";
        startButton.Size = new Size(120, 35);
        startButton.Location = new Point(160, 235);
        startButton.Click += StartGame;

        Controls.Add(logoBox);
        Controls.Add(versionLabel);
        Controls.Add(portLabel);
        Controls.Add(portBox);
        Controls.Add(startButton);
    }

    private void StartGame(object sender, EventArgs e)
    {
        if (running) return;

        int port;
        if (!int.TryParse(portBox.Text, out port))
        {
            MessageBox.Show("Invalid port number");
            return;
        }

        StartServer(port);

        string url = "http://localhost:" + port + "/index.html";
        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }

    private void StartServer(int port)
    {
        running = true;

        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        server = new HttpListener();
        server.Prefixes.Add("http://localhost:" + port + "/");
        server.Start();

        serverThread = new Thread(() =>
        {
            while (server.IsListening)
            {
                try
                {
                    var context = server.GetContext();
                    string filePath = Path.Combine(baseDir, context.Request.Url.AbsolutePath.TrimStart('/'));

                    if (File.Exists(filePath))
                    {
                        byte[] data = File.ReadAllBytes(filePath);
                        context.Response.OutputStream.Write(data, 0, data.Length);
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                    }

                    context.Response.OutputStream.Close();
                }
                catch { }
            }
        });

        serverThread.IsBackground = true;
        serverThread.Start();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        try
        {
            if (server != null)
            {
                server.Stop();
            }
        }
        catch { }
        base.OnFormClosing(e);
    }
}

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new LauncherForm());
    }
}