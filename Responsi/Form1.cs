using Npgsql;
using System.Data;
using System.Windows.Forms;

namespace Responsi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=2022;Username=postgres;Password=informatika;Database=responsi";
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private DataGridViewRow r;

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                dgvData.DataSource = null;
                sql = "select * from st_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                dgvData.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Gagal menampilkan data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = "select * from st_insert(:_id_karyawan, :_nama, :_id_dep :_nama_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_karyawan", tbIDKaryawan.Text);
                cmd.Parameters.AddWithValue("_nama", tbNama.Text);
                cmd.Parameters.AddWithValue("_id_karyawan", tbIDDep.Text);
                cmd.Parameters.AddWithValue("_nama_dep", tbNamaDep.Text);
                if ((int)cmd.ExecuteScalar()==1)
                {
                    MessageBox.Show("Success!", "Data berhasil di simpan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbIDKaryawan.Text = tbNama.Text = tbIDDep.Text = tbNamaDep.Text = null;
                    btnLoad.PerformClick();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Gagal menampilkan data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Alert!", "Mohon pilih Baris", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            try
            {
                conn.Open();
                sql = "select * from st_update(:_id_karyawan, :_nama, :_id_dep :_nama_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["_id_karyawan"].Value.ToString());
                cmd.Parameters.AddWithValue("_nama", tbNama.Text);
                cmd.Parameters.AddWithValue("_id_karyawan", tbIDDep.Text);
                cmd.Parameters.AddWithValue("_nama_dep", tbNamaDep.Text);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Success!", "Data berhasil di update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbIDKaryawan.Text = tbNama.Text = tbIDDep.Text = tbNamaDep.Text = null;
                    btnLoad.PerformClick();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Gagal menampilkan data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbIDKaryawan.Text = tbNama.Text = tbIDDep.Text = tbNamaDep.Text = null;
                btnLoad.PerformClick();
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //r = r.Cells[RowIndex];

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Alert!", "Mohon pilih Baris", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            try
            {
                conn.Open();
                sql = "select * from st_delete(:_id_karyawan)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["_id_karyawan"].Value.ToString());
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Success!", "Data berhasil di hapus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbIDKaryawan.Text = tbNama.Text = tbIDDep.Text = tbNamaDep.Text = null;
                    btnLoad.PerformClick();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Gagal menampilkan data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbIDKaryawan.Text = tbNama.Text = tbIDDep.Text = tbNamaDep.Text = null;
                btnLoad.PerformClick();
            }
        }
    }
}