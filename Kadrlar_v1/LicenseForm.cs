using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kadrlar
{
    public partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();

            string email, password;

            if (activated)
                panelDone.BringToFront();
            else if ((email = Set.get<string>("__cur_u_l")) != default(string) && (password = Set.get<string>("__cur_u_lp")) != default(string))
            {
                panelLicense.BringToFront();
            }
            else
            {
                panelLogin.BringToFront();
            }
        }



        #region VARIABLES
        private BackendlessUser user;
        public static bool activated = false;
        #endregion



        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            string login = emailTextBox.Text;
            string password = passwordTextBox.Text;
            panelLogin.Enabled = false;
            Cursor = Cursors.WaitCursor;
            
            Backendless.UserService.Login(login, password, new AsyncCallback<BackendlessUser>(
                user =>
                {
                    if (!Tools.isThisComputer((string)user.Properties["computer"]))
                        buttonLogout.PerformClick();
                    else
                    {
                        this.user = user;
                        MessageBox.Show("Сиз тизимга мувофаққиятли кирдингиз: " + login, "Бажарилди!");

                        Invoke(new Action(() =>
                        {
                            if (saveCheckBox.Checked)
                            {
                                Set.set<string>("__cur_u_l", Enc.encrypt(login));
                                Set.set<string>("__cur_u_lp", Enc.encrypt(password));
                            }

                            passwordTextBox.Clear();
                            Cursor = Cursors.Default;

                            if (user.Properties["license"].Equals("0"))
                            {
                                panelLicense.BringToFront();
                                codeTextBox.Focus();
                            }
                            else
                            {
                                panelDone.BringToFront();
                                activated = true;
                            }
                        }));
                    }
                },
                error =>
                {
                    MessageBox.Show("Введенные учетные данные не верны, перепроверьте их пожалуйста!\nПричина(en): " + error.Message, "Опаньки!");
                    Invoke(new Action(() =>
                    {
                        panelLogin.Enabled = true;
                        Cursor = Cursors.Default;
                    }));
                }));
        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            string login = emailTextBox.Text;
            string password = passwordTextBox.Text;
            panelLogin.Enabled = false;
            Cursor = Cursors.WaitCursor;

            BackendlessUser tempUser = new BackendlessUser();
            tempUser.Email = login;
            tempUser.Password = password;
            tempUser.Properties["computer"] = Tools.getHardwareId();
            string temp = Microsoft.VisualBasic.Interaction.InputBox("Илтимос паролни қайтадан ёзинг!", "Парол хавфсизлиги", "");
            if (!temp.Equals(password))
            {
                MessageBox.Show("Пароллар бири бирига мос келмади, илтимос қайтадан киритинг! ", "Ҳатолик");
                return;
            }

            Backendless.UserService.Register(tempUser, new AsyncCallback<BackendlessUser>(
                user =>
                {
                    MessageBox.Show("Сиз системада муваффаққиятли профиль яратдингиз: " + login + Environment.NewLine + "Ва энди сиз системага киришингиз мумкин!", "Бажарилди!");
                    Invoke(new Action(() =>
                    {
                        panelLogin.Enabled = true;
                        Cursor = Cursors.Default;
                    }));
                },
                error =>
                {
                    MessageBox.Show("Илтимос, далаларни қайта текшириб кўринг!\nҲатолик(en): " + error.Message, "Ҳатолик!");
                    Invoke(new Action(() =>
                    {
                        panelLogin.Enabled = true;
                        Cursor = Cursors.Default;
                    }));
                }));
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Set.delete("__cur_u_l", "__cur_u_lp");
            activated = false;
            passwordTextBox.Clear();
            emailTextBox.Clear();
            codeTextBox.Clear();
            panelLogin.BringToFront();
            Cursor = Cursors.WaitCursor;
            Backendless.UserService.Logout(new AsyncCallback<object>(
                responce =>
                {
                    panelLogin.Enabled = true;
                    Invoke(new Action(() =>
                    {
                        Cursor = Cursors.Default;
                    }));
                },
                error =>
                {
                    MessageBox.Show("Илтимос, яна бир бор уриниб кўринг!\nҲатолик(en): " + error.Message, "Ҳатолик!");
                    Invoke(new Action(() =>
                    {
                        panelLicense.BringToFront();
                        Cursor = Cursors.Default;
                    }));
                }));
        }

        private void buttonLicense_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            panelLicense.Enabled = false;

            string code = codeTextBox.Text;
            Backendless.Data.Of("License").Find(new AsyncCallback<BackendlessAPI.Data.BackendlessCollection<Dictionary<string, object>>>(
                responce =>
                {
                    for (int n = 0; n < responce.Data.Count; n++)
                    {
                        if (code.Equals(Enc.decrypt((string)responce.Data[n]["Code"])))
                        {
                            string temp = (string)responce.Data[n]["Code"];
                            user.Properties["license"] = temp;
                            Backendless.UserService.Update(user, new AsyncCallback<BackendlessUser>(
                                res =>
                                {
                                    MessageBox.Show("Сизнинг калитингиз топилди ва сиз билан муваффаққиятли интеграцияланди!", "Муваффаққият!");
                                    Invoke(new Action(() =>
                                    {
                                        Cursor = Cursors.Default;
                                        panelDone.BringToFront();
                                        activated = true;
                                    }));
                                },
                                err =>
                                {
                                    MessageBox.Show(err.Message + "\n\n" + err.Detail, "ERR");
                                }));

                            Backendless.Data.Of("License").Remove(responce.Data[n]);
                            return;
                        }
                    }
                    MessageBox.Show("Калитингизда муаммо топилди. Илтимос, қайтадан уриниб кўринг!", "Ҳатолик!");
                    panelLicense.Enabled = true;
                    Cursor = Cursors.Default;
                },
                error =>
                {
                    MessageBox.Show("Илтимос, далаларни қайта текшириб кўринг!\nҲатолик(en): " + error.Message, "Ҳатолик!");
                    Invoke(new Action(() =>
                    {
                        panelLicense.Enabled = true;
                        Cursor = Cursors.Default;
                    }));
                }));
        }
    }

    class License
    {
        public License()
        {

        }

        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = Enc.encrypt(value);
            }
        }

        private string code;
    }
}
