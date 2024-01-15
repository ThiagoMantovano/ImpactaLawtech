using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CRUD_Lawtech.BLL
{
    public class Cpf
    {
        public static bool ValidarCPF(string cpf)
        {
            // Remove caracteres não numéricos
            string numerosCpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Verifica se o CPF tem 11 dígitos
            if (numerosCpf.Length != 11)
            {
                return false;
            }

            // Verifica se todos os dígitos são iguais (caso contrário, o CPF seria inválido)
            if (numerosCpf.Distinct().Count() == 1)
            {
                return false;
            }

            // Calcula os dígitos verificadores
            int[] cpfArray = numerosCpf.Select(c => int.Parse(c.ToString())).ToArray();
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += cpfArray[i] * (10 - i);
            }
            int resto = soma % 11;
            int digitoVerificador1 = (resto < 2) ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += cpfArray[i] * (11 - i);
            }
            resto = soma % 11;
            int digitoVerificador2 = (resto < 2) ? 0 : 11 - resto;

            // Verifica se os dígitos verificadores calculados são iguais aos fornecidos no CPF
            return (digitoVerificador1 == cpfArray[9] && digitoVerificador2 == cpfArray[10]);
        }
        public static void FormatarTextBoxCPF(TextBox textBoxCpf)
        {
            // Remove qualquer caractere não numérico
            string cpf = Regex.Replace(textBoxCpf.Text, "[^0-9]", "");
            textBoxCpf.ForeColor = System.Drawing.SystemColors.ControlText;
            // Formata o CPF apenas se houver números suficientes
            if (cpf.Length >= 3)
                cpf = cpf.Insert(3, ".");
            if (cpf.Length >= 7)
                cpf = cpf.Insert(7, ".");
            if (cpf.Length >= 11)
                cpf = cpf.Insert(11, "-");
            if (cpf.Length == 14)
            {
                if (Cpf.ValidarCPF(cpf))
                    textBoxCpf.ForeColor = System.Drawing.Color.MediumSeaGreen;
                else
                    textBoxCpf.ForeColor = System.Drawing.Color.Firebrick;
            }

            // Atualiza o texto do TextBox
            textBoxCpf.Text = cpf;

            // Move o cursor para o final do texto
            textBoxCpf.SelectionStart = textBoxCpf.Text.Length;
        }
        public static void ApenasNumerosTextBoxCPF(TextBox textBoxCpf, KeyPressEventArgs e) 
        {
            // Se a tecla Backspace for pressionada, remove o último caractere
            if (e.KeyChar == 8)
            {
                string cpf = Regex.Replace(textBoxCpf.Text, "[^0-9]", "");
                if (cpf.Length > 0)
                {
                    cpf = cpf.Substring(0, cpf.Length - 1);
                }

                // Atualiza o texto do TextBox
                textBoxCpf.Text = cpf;

                // Move o cursor para o final do texto
                textBoxCpf.SelectionStart = textBoxCpf.Text.Length;

                e.Handled = true; // Impede que o caractere Backspace seja inserido no TextBox
            }
            else
            {
                // Permite apenas números
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        
    }
}
