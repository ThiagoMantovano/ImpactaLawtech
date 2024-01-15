using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CRUD_Lawtech.BLL
{
    public class Email
    {
        public static bool ValidarEmail(string email)
        {
            // Padrão de expressão regular para validar endereço de e-mail
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Criar objeto Regex com o padrão
            Regex regex = new Regex(pattern);

            // Testar se o e-mail corresponde ao padrão
            return regex.IsMatch(email);
        }
        public static void FormatarTextBoxEmail(TextBox textBoxEmail) 
        {
            if (!Email.ValidarEmail(textBoxEmail.Text))
                textBoxEmail.ForeColor = System.Drawing.Color.Firebrick;
            else
                textBoxEmail.ForeColor = System.Drawing.Color.MediumSeaGreen;
        }
    }
}
