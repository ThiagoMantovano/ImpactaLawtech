using CRUD_Lawtech.Types;
using System;
using System.Windows.Forms;
using CRUD_Lawtech.BLL;
using CRUD_Lawtech.Utils;
using System.Security.Cryptography.X509Certificates;

namespace CRUD_Lawtech
{
    public partial class FormDetalhes : Form
    {
        public Contato Contato { get; private set; }

        bool flagInsercao;
        public FormDetalhes(Contato contato)
        {
            InitializeComponent();
            

            if (contato != null)
            {
                
                Contato = contato;

                textBoxCpf.Text = contato.CPF;
                textBoxNome.Text = contato.NomeCompleto;
                textBoxEmail.Text = contato.Email;
                flagInsercao = false;
            }
            else
            {
                flagInsercao = true;
                Contato = new Contato();
            }
        }

        private void bt_Salvar_Click(object sender, EventArgs e)
        {
            try 
            {
                if (checarCamposValidos())
                {
                    Contato.NomeCompleto = textBoxNome.Text;
                    Contato.Email = textBoxEmail.Text;
                    Contato.CPF = textBoxCpf.Text;

                    DialogResult = DialogResult.OK;

                    if (flagInsercao == true)
                        Log.GravarLog(false, "Contato de nome: " + Contato.NomeCompleto + " inserido com sucesso!");
                    else
                        Log.GravarLog(false, "Contato de nome: " + Contato.NomeCompleto + " alterado com sucesso!");

                    Close();
                }
            } 
            catch (Exception ex) 
            {
                Log.GravarLog(true, "Método: bt_Salvar_Click | Erro ao salvar contato ( " + ex.Message + " )");
            }
        }

        private void bt_Sair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxCpf_TextChanged(object sender, EventArgs e)
        {
            Cpf.FormatarTextBoxCPF(textBoxCpf);
        }

        private void textBoxCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            Cpf.ApenasNumerosTextBoxCPF(textBoxCpf, e);
        }
        private bool checarCamposValidos()
        {
            if (Cpf.ValidarCPF(textBoxCpf.Text) == false)
            {
                MessageBox.Show("Digite um CPF Válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Email.ValidarEmail(textBoxEmail.Text) == false)
            {
                MessageBox.Show("Digite um E-mail Válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (textBoxNome.Text.Length < 0)
            {
                MessageBox.Show("Nome Obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            Email.FormatarTextBoxEmail(textBoxEmail);
        }

    }
}
