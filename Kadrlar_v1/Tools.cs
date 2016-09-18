using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using Word=Microsoft.Office.Interop.Word;
using Excel=Microsoft.Office.Interop.Excel;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Management;
using System.Text.RegularExpressions;

namespace Kadrlar
{
    class Tools
    {
        #region CONSTANTS AND VARIABLES
        public const string delimiter = "|";
        public const string delimiter2 = delimiter + delimiter;
        public static string currentFolderPath = AppDomain.CurrentDomain.BaseDirectory;
        public static SqlConnection dbWorkers;
        public static SqlConnection dbSettings;
        public static SqlConnection dbOrganizations;
        public static SqlConnection dbReferences;
        public static bool isFirstTime = false;

        public static string OrganizationForSql = "default";
        private static string organization = "default";
        public static string OrganizationTruncated = "default";
        public static string WorkersTableName = "workers_default";
        public static string Organization
        {
            get
            {
                return organization;
            }
            set
            {
                organization = value;
                OrganizationForSql = escapeStringSql(value);
                OrganizationForSql = OrganizationForSql.Substring(2, OrganizationForSql.Length - 3);
                OrganizationTruncated = truncateNonLetters(organization);
                WorkersTableName = "workers_" + OrganizationTruncated;

                execute(dbSettings, "update settings set value=N'" + OrganizationForSql + "' where name='organization';");
            }
        }
        #endregion



        public static void initializeDB()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Images");
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Documents"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Documents");
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Database"))
            {
                DirectoryInfo dir = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Database");
                File.WriteAllBytes(dir.FullName + "Organizations.mdf", Resources.Resource1.Organizations);
                File.WriteAllBytes(dir.FullName + "Organizations_log.ldf", Resources.Resource1.Organizations_log);
                File.WriteAllBytes(dir.FullName + "Settings.mdf", Resources.Resource1.Settings);
                File.WriteAllBytes(dir.FullName + "Settings_log.ldf", Resources.Resource1.Settings_log);
                File.WriteAllBytes(dir.FullName + "Workers.mdf", Resources.Resource1.Workers);
                File.WriteAllBytes(dir.FullName + "Workers_log.ldf", Resources.Resource1.Workers_log);
                File.WriteAllBytes(dir.FullName + "Directories.mdf", Resources.Resource1.Directories);
                File.WriteAllBytes(dir.FullName + "Directories_log.ldf", Resources.Resource1.Directories_log);
            }
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Templates"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Templates");
                DirectoryInfo dir = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Templates");
            }

            dbWorkers = getConnection("Workers");
            dbWorkers.Open();

            dbOrganizations = getConnection("Organizations");
            dbOrganizations.Open();

            dbSettings = getConnection("Settings");
            dbSettings.Open();

            dbReferences = getConnection("Directories");
            dbReferences.Open();

            execute(dbSettings, "if not exists (select * from sysobjects where name='settings' and xtype='u') create table settings(name nvarchar(200) primary key, value nvarchar(200));");
            SqlDataReader rd = read(dbSettings, "select value from settings where name='fisrttime';");
            if (isFirstTime = !rd.Read())
            {
                rd.Close();
                execute(dbSettings, "insert into settings(name, value) values ('fisrttime', 'false');");
                execute(dbSettings, "insert into settings(name, value) values ('organization', 'default');");
            }
            else rd.Close();

            rd = read(dbSettings, "select value from settings where name='organization'");
            if (rd.Read())
            {
                string org = rd[0].ToString();
                rd.Close();
                Organization = org;
            }
            else rd.Close();
        }



        public static SqlConnection getConnection(string databaseFileName)
        {
            SqlConnection res;
            res = new SqlConnection();
            res.ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"" + AppDomain.CurrentDomain.BaseDirectory + "Database\\" + databaseFileName + ".mdf\"; Integrated Security = True";
            return res;
        }

        public static void execute(SqlConnection con, string command)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
        }

        public static SqlDataReader read(SqlConnection con, string command)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = command;
            return cmd.ExecuteReader();
        }

        public static void loadDataToView(DataGridView view, SqlConnection con, string sqlcommand)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                DataTable table = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand, con.ConnectionString);
                dataAdapter.Fill(table);
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                view.BeginInvoke(new Action(() =>
                {
                    view.DataSource = table;
                    view.ClearSelection();
                    if (view.Columns.Count != 0)
                    {
                        int width = view.Width / view.Columns.Count;
                        foreach (DataGridViewColumn col in view.Columns)
                            col.Width = width;
                    }
                }));
            };
            worker.RunWorkerAsync();
        }



        public static string escapeStringSql(string data)
        {
            string res = "";
            foreach (char c in data)
            {
                if (c == '\'')
                    res += '\'';
                res += c;
            }
            return "N'" + res + '\'';
        }

        public static string escapeStringBookmark(string data)
        {
            string res = "";
            foreach (char c in data)
            {
                if (char.IsLetterOrDigit(c))
                    res += c;
            }
            return res;
        }

        public static string escapeStringRegex(string data)
        {
            string res = "";
            foreach (char c in data)
            {
                if (c == '\\' || c == '|')
                    res += '\\';
                res += c;
            }
            return res;
        }

        public static string truncateNonLetters(string data)
        {
            string res = "";
            foreach (char c in data)
                if (char.IsLetterOrDigit(c))
                    res += c;
            return res;
        }



        public static string[][] parseTable(string tableData)
        {
            string[] temp = Regex.Split(tableData, Tools.escapeStringRegex(Tools.delimiter2));
            string[][] res = new string[temp.Length][];
            for (int m = 0; m < res.Length; m++)
            {
                string[] tempRow = Regex.Split(temp[m], Tools.escapeStringRegex(Tools.delimiter));
                res[m] = new string[tempRow.Length];
                for (int p = 0; p < tempRow.Length; p++)
                    res[m][p] = tempRow[p];
            }
            return res;
        }

        public static string tableToString(DataGridView table)
        {
            string res = "", cols = "", tempString;

            foreach (DataGridViewColumn col in table.Columns)
                cols += col.HeaderText + Tools.delimiter;
            cols += Tools.delimiter;

            for (int r = 0; r < table.Rows.Count - 1; r++)
            {
                foreach (DataGridViewCell cell in table.Rows[r].Cells)
                {
                    tempString = (string)cell.Value;
                    if (cell.Value == null)
                        res += " " + Tools.delimiter;
                    else
                        res += tempString + Tools.delimiter;
                }
                res += Tools.delimiter;
            }
            if (!res.Equals(""))
                res = cols + res.Substring(0, res.Length - 2);

            return res;
        }



        public static void fillBookmarks(WordFile templateFile, params SqlDataReader[] dataSources)
        {
            // load column values for further search
            #region PREPARATION FOR FILLING
            Dictionary<string, string> data = new Dictionary<string, string>();
            for (int n = 0; n < dataSources.Length; n++)
                for (int m = 0; m < dataSources[n].FieldCount; m++)
                    data.Add(dataSources[n].GetName(m), (string)dataSources[n][m]);
            #endregion 

            // fill the blanks
            for (int n = 1; n < templateFile.Document.FormFields.Count; n++)
            {
                var field = templateFile.Document.FormFields[n];
                if (!data.ContainsKey(field.StatusText))
                    continue;

                string value = data[field.StatusText];

                #region IMAGE SETTER
                if (field.Name.StartsWith("image"))
                {
                    bool scaleMode = false;
                    float width = 0, height = 0;
                    string[] temp;

                    if (scaleMode = field.Name.Contains('_'))
                    {
                        temp = field.Name.Substring(field.Name.IndexOf('_') + 1).Split('_');
                        width = float.Parse(temp[0]);
                        height = float.Parse(temp[1]);
                    }

                    string fileName = AppDomain.CurrentDomain.BaseDirectory + @"Images\" + value;

                    setBmkImage(field, fileName, scaleMode, width, height);
                }
                #endregion

                #region TABLE SETTER
                else if (field.Name.StartsWith("table"))
                {
                    string tableName = field.Name;

                    // parse values of a table
                    string[][] table = parseTable(value);

                    setBmkTable(field, templateFile, table);
                }
                #endregion

                #region TEXT SETTER
                else
                {
                    setBmkText(field, templateFile, value);
                }
                #endregion
            }
        }

        public static bool fillTemplateFile(WordFile templateFile, string workerSqlWhereCondition)
        {
            string command = "select * from " + WorkersTableName + " " + workerSqlWhereCondition;

            SqlDataReader workerReader = read(dbWorkers, command);
            SqlDataReader orgReader = read(dbOrganizations, "select * from organizations where [Организация тўлиқ номи]=N'" + OrganizationForSql + '\'');

            if (workerReader.Read() && orgReader.Read())
            {
                fillBookmarks(templateFile, workerReader, orgReader);
                workerReader.Close();
                orgReader.Close();
            }
            else
            {
                workerReader.Close();
                orgReader.Close();
            }

            return true;
        }



        public static void setBmkImage(Word.FormField field, string fileName, bool scaleMode, float width, float height)
        {
            string bmName = field.Name;

            if (File.Exists(fileName))
            {
                var rng = field.Range;
                Word.InlineShape picture = field.Range.Fields[1].Result.InlineShapes.AddPicture(fileName);

                if (scaleMode)
                {
                    picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                    if (width < height)
                        picture.ScaleWidth = 100 * width / picture.Width;
                    else
                        picture.ScaleHeight = 100 * height / picture.Height;
                }
            }
        }

        public static void setBmkTable(Word.FormField field, WordFile wordFile, string[][] table)
        {
            var rng = field.Range.Fields[1].Result;
            // setting a name of a table
            //rng.InsertBefore(tableName);
            rng.InsertParagraphAfter();
            rng.Font.Size = 16;
            rng.SetRange(rng.End, rng.End);

            int rowsCount = table.Length;
            int colsCount = table.Length > 0 ? table[0].Length : 0;

            Word.Table newTable = wordFile.Document.Tables.Add(rng, rowsCount, colsCount);

            newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

            for (int r = 0; r < rowsCount; r++)
            {
                for (int c = 0; c < colsCount; c++)
                {
                    var cell = newTable.Cell(r + 1, c + 1);
                    cell.Range.Text = table[r][c];
                }
            }
            for (int c = 1; c <= colsCount; c++)
                newTable.Cell(1, c).Range.Font.Bold = -1;
        }

        public static void setBmkText(Word.FormField field, WordFile wordFile, string newText)
        {
            field.Range.Fields[1].Result.Text = newText;
        }



        public static bool isFileLocked(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }

        public static void displayForm(Form form, Form parent = null, bool isMdi = false)
        {
            bool open = form.Tag.Equals("demo");

            if (LicenseForm.activated || open)
                if (parent == null)
                {
                    form.Show();
                }
                else if (isMdi)
                {
                    if (!form.IsDisposed)
                    {
                        if (parent.MdiChildren.Length == 0)
                        {
                            form.MdiParent = parent;
                            form.Show();
                        }
                        else
                        {
                            foreach (Form tempForm in parent.MdiChildren)
                                tempForm.WindowState = FormWindowState.Normal;
                            MessageBox.Show("Илтимос аввал очилган ичги ойналарни ёпинг!", "Ойналарни ёпинг!");
                        }
                    }
                }
                else
                {
                    form.ShowDialog(parent);
                }
            else MessageBox.Show("Бу ойна лицензия талаб қилади,  илтимос аввал дастурни лицензияланг!", "Лицензияланг");
        }



        public static string getHardwareId()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }

            char drive = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)[0];
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();

            return cpuInfo + volumeSerial;
        }

        public static bool isThisComputer(string hardwareId)
        {
            return hardwareId.Equals(getHardwareId());
        }
    }

    public class Enc
    {
        private const string pubKeyData = "<?xml version=\"1.0\" encoding=\"utf-16\"?><RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Exponent>AQAB</Exponent>  <Modulus>5rrD92aHkA2pXo7/y6v9az7Srfd8AdGju+5fn5RoNs+ezF+cTxy+zXmIE62NqyP47C2RBDhirAtNzP+VnC+jgSFj9kHXh1flFB6MUeCMf7zWpnRDmOFT8k7xQplqDH6f90GwEsp3w1B1BGULiXF6InzDb6aPdPKnSiXeKMpQyuNZG4pPKpKPMVf8IW/g9J35yD+BXjYYeIOuHarrZMu1jFj3iRBtR5CBLrhM3FneVxz2PYxhxOLEYjvv4/1TLMiB0VKSvyXqVOAB77N05ruBXp1ABihzecBQSBI0HGGMTXsoGvwU9z/9zbvD/53OFyVwGMqcue5XwCXNIy4mX/nCLQ==</Modulus></RSAParameters>";
        private const string privKeyData = "<?xml version=\"1.0\" encoding=\"utf-16\"?><RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Exponent>AQAB</Exponent>  <Modulus>5rrD92aHkA2pXo7/y6v9az7Srfd8AdGju+5fn5RoNs+ezF+cTxy+zXmIE62NqyP47C2RBDhirAtNzP+VnC+jgSFj9kHXh1flFB6MUeCMf7zWpnRDmOFT8k7xQplqDH6f90GwEsp3w1B1BGULiXF6InzDb6aPdPKnSiXeKMpQyuNZG4pPKpKPMVf8IW/g9J35yD+BXjYYeIOuHarrZMu1jFj3iRBtR5CBLrhM3FneVxz2PYxhxOLEYjvv4/1TLMiB0VKSvyXqVOAB77N05ruBXp1ABihzecBQSBI0HGGMTXsoGvwU9z/9zbvD/53OFyVwGMqcue5XwCXNIy4mX/nCLQ==</Modulus>  <P>/y+hffFkA891fi4Q+Rm6SZrPAwmKGESdwsAKTLrfgtjnzm1qsr7BauX/hlKU7BDW/AhxwoZ90iupoaYJSfMPjoUzKHFsBZhDGixaeC+dxfj7epke2cntA9PIKbPB0tsl9Xf1IEcVwMqYSViHF9jddlKZROgOXOW1yw3iNQt9icE=</P>  <Q>53cqPRFhG9PcHc8ithjwKxXOa3a0vUpoWuHzuLCNLnOLP0nR0KE0OAvkWlHAGN9ThZ8e7AMIwTdPL7MLJzw7hUYqwNfcuG4JEA7mc2splSwWcSCiOvKq+nDc5uXbLjil4n+3XvDgnR6w5ZAsnvZKuFUxuTYDbWJu7nVghtYu220=</Q>  <DP>5CbPuexyQT3gjDnfjvdDqL8ySkLjJqNicVaFXujNIQ2Q4uzMspb+EvcjqBJ5dz3vGFLscsEQCTJkbVQnheg8hm3suUH/FFl31RTGpiHca74aCRiRjqKMuBlHIpHGvyCUJY1kqcEkX8RCt4Dg9587Eajzw7m97ayrBqqZDlgBm4E=</DP>  <DQ>V8MuJ8N8L4Hv9vl/7s2b2qpE/ygeNZuN38/GimOe1FsJFDEYNeO5mfqgVcKjdIrQ71w67D/mROSEyNA5TWIgK1NEiKQQU3mdRBJyPJcISEBULgaynGlAfP7oM0A6D9d4xl2omH36nHhzmEdHM+qswCHFXbVmLUZiS1YECHwTfi0=</DQ>  <InverseQ>1JhWxXxAp+F5diprWFMUDdDP8igk8+SBg79mlunIqxpZKPC3kyjQoGrMgR1dTb9cF5yTJ1JKo0jQoSCN4gAN7lHufnONYYYKYYprmrAkljj4IuxqDyWyONBamScWP1DPqtEFbJ5lVhFMI/W9hdpJfOzlStmlVqSzbRC4IS65l98=</InverseQ>  <D>AZmg+0nAPe0CVDAMsRZnwwMkBeXV+9M+3fJIwnwOfvbQpJ0zIc9SuW0S1wQYm+u7cbPdMhjMAmJvrzDEklk4OpCHN3F8c6lV4t5VXcn/X3FpUa0zvM8vPFp0zN/wftav+FCBzpeN3hJkRVwICPN7A9s2pcMTcp2uMFLz5/eDOOz4VLvvnhkMIYGE0CuEstIkBaZvmA97bNAcnOb1j1Wr21+WUAi2sg7+ZJCakYbjFRYwXcvpkctRYavO/eqgipgAl9T7CeC3SnI3CD7VGew/CZ4gPwlmfExXyUNMAD+psYQ4O451PObfT3/F7etArxSgDA8RxAXzCzaCstS4n9icgQ==</D></RSAParameters>";
        private static StringReader srdPub = new StringReader(pubKeyData);
        private static StringReader srdPriv = new StringReader(privKeyData);
        private static XmlSerializer xml = new XmlSerializer(typeof(RSAParameters));
        private static RSAParameters pubKey = (RSAParameters)xml.Deserialize(srdPub);
        private static RSAParameters privKey = (RSAParameters)xml.Deserialize(srdPriv);
        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);

        public static string encrypt(string input)
        {
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(pubKey);

            //we need some data to encrypt
            var plainTextData = input;

            //for encryption, always handle bytes...
            var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(plainTextData);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);

            //we might want a string representation of our cypher text... base64 will do
            var cypherText = Convert.ToBase64String(bytesCypherText);

            return cypherText;
        }

        public static string decrypt(string input)
        {
            //first, get our bytes back from the base64 string ...
            var bytesCypherText = Convert.FromBase64String(input);

            //we want to decrypt, therefore we need a csp and load our private key
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privKey);

            //decrypt and strip pkcs#1.5 padding
            var bytesPlainTextData = csp.Decrypt(bytesCypherText, false);

            //get our original plainText back...
            var plainTextData = System.Text.Encoding.Unicode.GetString(bytesPlainTextData);

            return plainTextData;
        }
    }

    public class Set
    {
        private const string path = @"HKEY_CURRENT_USER\Software\Kadrlar";

        public static T get<T>(string key)
        {
            try
            {
                return (T)Registry.GetValue(path, key, default(T));
            }
            catch
            {
                return default(T);
            }
        }

        public static void set<T>(string key, T value)
        {
            Registry.SetValue(path, key, value);
        }

        public static void delete(params string[] keys)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\Kadrlar", true);
            foreach (string key in keys)
            {
                try
                {
                    reg.DeleteValue(key);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), e.Message);
                }
            }
        }
    }
}
