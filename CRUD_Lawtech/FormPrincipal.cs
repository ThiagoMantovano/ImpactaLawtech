using CRUD_Lawtech.DAL;
using System;
using System.Configuration;
using System.Windows.Forms;
using CRUD_Lawtech.Utils;
namespace CRUD_Lawtech
{
    public partial class FormPrincipal : Form
    {
        private bool aplicandoFiltro = false;
        DML dml = new DML(ConfigurationManager.ConnectionStrings["CRUD_Lawtech.Properties.Settings.LawtechConnectionString"].ConnectionString);
        public FormPrincipal()
        {

            InitializeComponent();

            comboBox1.SelectedIndex = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.contatosTableAdapter.Fill(this.contatosImpacta.contatos);
            }
            catch (Exception ex)
            {
                Log.GravarLog(true, "Erro ao recuperar dados ( " + ex.Message + " )");
            }

            filtro.TextChanged += textBox1_TextChanged;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!aplicandoFiltro)
            {
                AplicarFiltro();
            }
        }

        private void AplicarFiltro()
        {
            try
            {
                string filtroTexto = filtro.Text.Trim();
                string categoria = comboBox1.SelectedItem.ToString();
                string filtroQuery = DML.FiltrarPorCategoria(categoria, filtroTexto);

                if (filtroQuery == "")
                {
                    contatosBindingSource.RemoveFilter();
                }
                else
                {
                    contatosBindingSource.Filter = filtroQuery;
                }
            }
            catch (Exception ex)
            {
                Log.GravarLog(true, "Método: AplicarFiltro | Erro ao aplicar filtro ( " + ex.Message + " )");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void bt_Incluir_Click(object sender, EventArgs e)
        {
            try
            {
                var formDetalhes = new FormDetalhes(null); // Passa null para indicar que é uma adição
                if (formDetalhes.ShowDialog() == DialogResult.OK)
                {
                    dml.Inserir(formDetalhes.Contato);
                    AtualizarDataGridView();
                }
            }
            catch (Exception ex)
            {
                Log.GravarLog(true, "Método: bt_Incluir_Click | Erro ao incluir contato ( " + ex.Message + " )");
            }
        }

        private void bt_Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {

                    int id = (int)dataGridView1.SelectedRows[0].Cells["id"].Value;
                    var contato = dml.ObterPorId(id);
                    var formDetalhes = new FormDetalhes(contato);
                    if (formDetalhes.ShowDialog() == DialogResult.OK)
                    {
                        dml.Atualizar(formDetalhes.Contato);
                        AtualizarDataGridView();
                    }

                }
            }
            catch (Exception ex)
            {
                Log.GravarLog(true, "Método: bt_Alterar_Click | Erro ao alterar contato ( " + ex.Message + " )");
            }
        }

        private void bt_Excluir_Click(object sender, EventArgs e)
        {
            try 
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int id = (int)dataGridView1.SelectedRows[0].Cells["id"].Value;
                    dml.Excluir(id);
                    Log.GravarLog(false, "Contato de id: " + id + " excluído com sucesso!");
                    AtualizarDataGridView();
                }
            } 
            catch (Exception ex) 
            {
                Log.GravarLog(true, "Método: bt_Excluir_Click | Erro ao excluir contato ( " + ex.Message + " )");
            }
            
        }
        private void AtualizarDataGridView()
        {
            try
            {
                this.contatosTableAdapter.Fill(this.contatosImpacta.contatos);
            }
            catch (Exception ex)
            {
                Log.GravarLog(true, "Método: AtualizarDataGridView | Erro ao atualizar a tabela ( " + ex.Message + " )");
            }
            
        }
    }
}
