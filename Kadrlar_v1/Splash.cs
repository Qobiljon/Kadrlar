using BackendlessAPI;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Kadrlar
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void log(string message)
        {
            try
            {
                Invoke(new Action(() => { label2.Text = message; }));
            }
            catch
            {

            }
        }

        private void finish()
        {
            Invoke(new Action(() =>
                {
                    Close();
                }));
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            BackgroundWorker exec = new BackgroundWorker();
            exec.DoWork += (object snd, DoWorkEventArgs ev) =>
                {
                    int time = Environment.TickCount;

                    log("Базалар ва файллар тикланмоқда...");
                    try
                    {
                        Tools.initializeDB();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("База ва файлларни тиклашда хатолик рўй берди.\nИлтимос яна бир бор уриниб кўринг!", "Ҳатолик рўй берди");
                        Program.runMainForm = false;
                        Application.Exit();
                    }
                    if (Environment.TickCount - time < 300)
                        System.Threading.Thread.Sleep(200);
                    time = Environment.TickCount;


                    log("Сервер билан боғланилмоқда...");
                    try
                    {
                        Backendless.InitApp("9662BBB6-1389-526B-FFBD-E807D393C900", "68C5C900-4162-BD2C-FFA5-8CF38598B300", "v1");
                    }
                    catch
                    {
                        MessageBox.Show("Сервер билан боғланишда хатолик рўй берди.\nИлтимос компюторингизда интернет алоқани текшириб кўринг!", "Ҳатолик рўй берди");
                        Program.runMainForm = false;
                        Application.Exit();
                    }
                    if (Environment.TickCount - time < 300)
                        System.Threading.Thread.Sleep(200);
                    time = Environment.TickCount;


                    log("Масофадан системага кирилмоқда...");
                    try
                    {
                        string email, password;
                        if ((email = Set.get<string>("__cur_u_l")) != default(string) && (password = Set.get<string>("__cur_u_lp")) != default(string))
                        {
                            email = Enc.decrypt(email);
                            password = Enc.decrypt(password);
                            Backendless.UserService.Login(email, password);
                            LicenseForm.activated = !Backendless.UserService.CurrentUser.Properties["license"].Equals("0");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Сервер билан маълумот алмашишда ҳатолик рўй берди.\nИлтимос компюторингизда интернет алоқани текшириб кўринг!", "Ҳатолик рўй берди");
                        Program.runMainForm = false;
                        Application.Exit();
                    }

                    log("Кадрлар дастури ишга туширилди!");
                    System.Threading.Thread.Sleep(400);

                    finish();
                };
            exec.RunWorkerAsync();
        }
    }
}
