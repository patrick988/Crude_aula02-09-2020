using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Text;

namespace Crude_aula02_09_2020
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
       
        }

        SqlConnection sqlCon = null;
        private string strCon = @"Password=papa1212;Persist Security Info=True;User ID=sa;Initial Catalog=BDCrud;Data Source=DESKTOP-O0QHQ28\SQLEXPRESS";
            private string strSql = string.Empty;


        private void lblNome_Click(object sender, EventArgs e)
        {

        }

        private void btninserir_Click(object sender, EventArgs e)
        {

            strSql = "insert into Agenda  (nome, Telefone) values (@Nome, @Telefone)";

            sqlCon = new SqlConnection(strCon);

            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            try
            {
                sqlCon.Open();
                //passagem de parametros
                comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = textname.Text;
                comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = textelefone.Text;

                comando.ExecuteNonQuery();
                MessageBox.Show("Registro efetuado com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                textname.Clear();
                textelefone.Clear();
                textname.Focus();

            }




        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (textid.Text == string.Empty)
            {
                strSql = "select * from agenda order by id";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(strSql, sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgagenda.DataSource = dt;

            }
            else
            {

                strSql = "select * from agenda where id=@id";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);

                comando.Parameters.Add("@id", SqlDbType.Int).Value = textid.Text;


                try
                {


                    if (textid.Text == string.Empty)
                    {
                        throw new Exception("Digite um ID");


                    }

                    sqlCon.Open();

                    SqlDataReader dr = comando.ExecuteReader();


                    if (dr.HasRows == false)
                    {
                        throw new Exception("Id não cadastrado");

                    }

                    if (dr.Read())
                    {
                        textid.Text = Convert.ToString(dr["id"]);
                        textname.Text = Convert.ToString(dr["Nome"]);
                        textelefone.Text = Convert.ToString(dr["Telefone"]);

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                    textname.Focus();
                }


            }

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            strSql = "update agenda set Nome=@Nome, Telefone=@Telefone where id=@id";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@id", SqlDbType.VarChar).Value = textid.Text;
            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = textname.Text;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = textelefone.Text;


            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro atualizado!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                textname.Clear();
                textelefone.Clear();
                textname.Focus();

                strSql = "select * from agenda order by id";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comm = new SqlCommand(strSql, sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(strSql, sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgagenda.DataSource = dt;

            }



        }

        private void lblid_Click(object sender, EventArgs e)
        {
            //fdsfsdfsdfsdfsdfsdlkkç
        }

        private void lblTelefone_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // programa para deletar registros
            if (MessageBox.Show("Deseja Excluir o registro?", "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)==DialogResult.No)
            {
                MessageBox.Show("Operação Cancelada");
            }
            else
            {
                strSql = "Delete from agenda where id = @id";
                sqlCon = new SqlConnection(strCon);

                SqlCommand comando = new SqlCommand(strSql, sqlCon);
                comando.Parameters.Add("@id", SqlDbType.Int).Value = textid.Text;

                try
                {
                    sqlCon.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro apagado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                    textid.Text = "";
                    textname.Clear();
                    textelefone.Clear();
                    textname.Focus();

                }
            }

        }

        private void dgagendaclick(object sender, DataGridViewCellEventArgs e)
        {
            textid.Text = dgagenda.CurrentRow.Cells[0].Value.ToString();
            textname.Text = dgagenda.CurrentRow.Cells[1].Value.ToString();
            textelefone.Text = dgagenda.CurrentRow.Cells[2].Value.ToString();
        }

        private void textid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
