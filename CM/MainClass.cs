using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CM
{
    internal class MainClass
    {
        public static string user;

        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }


        public static readonly string con_string = "Data Source = PROPC; Initial Catalog=KM;  TrustServerCertificate=True; Persist Security Info=False; User ID=sa; Password=1234;";
        public static SqlConnection con = new SqlConnection(con_string); 


        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;


                string qry = $"select * from users where username = '{user}' and userpass = '{pass}'";

                SqlCommand cmd = new SqlCommand(qry, con);  
                DataTable dt = new DataTable();  
                SqlDataAdapter da = new SqlDataAdapter(cmd);  
                da.Fill(dt);  

                if (dt.Rows.Count > 0)
                {
                    isValid = true;
                    USER = dt.Rows[0]["uName"].ToString();

                }

                
                return isValid;


        }


        public static int SQL(string qry, Hashtable ht) 
        {

            int res = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text; 

                foreach (DictionaryEntry item in ht)  
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                res = cmd.ExecuteNonQuery(); 

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
                con.Close();

            }

            return res;
        }


        public static void LoadDate(string qry, DataGridView gv, ListBox lb)    
        {
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);

            try
            {

                SqlCommand cmd = new SqlCommand(qry, con);          
                cmd.CommandType = CommandType.Text;                  
                SqlDataAdapter da = new SqlDataAdapter(cmd);        
                DataTable dt = new DataTable();                     
                da.Fill(dt);                                        

                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string colNam1 = ((DataGridViewColumn)lb.Items[i]).Name;           
                    gv.Columns[colNam1].DataPropertyName = dt.Columns[i].ToString();   
                }


                gv.DataSource = dt;      

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                con.Close();
            }
        }

        public static void BlurBackground(Form form)
        {
            Form Background = new Form();
            using (form)
            {
                Background.StartPosition = FormStartPosition.Manual;
                Background.FormBorderStyle = FormBorderStyle.None;
                Background.Opacity = 0.5d;                   
                Background.BackColor = Color.Black;
                Background.Size = fkmMain.instance.Size;     
                Background.Location = fkmMain.instance.Location;
                Background.ShowInTaskbar = false;            
                Background.Show();
                form.Owner = Background;               
                form.ShowDialog(Background);           
                Background.Dispose();                  
            }
        }


        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;

            foreach (DataGridViewRow row in gv.Rows)   
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public static void CBFill(string qry, ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }



    }
}
