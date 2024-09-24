using System.Globalization;

namespace P1_pps.Models
{
    public static class Valida
    {

        public static bool IsDataNascimentoValida(string dataNascimento)
        {
            if (DateTime.TryParseExact(dataNascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
            {
                return data <= DateTime.Now;
            }
            return false; 
        }

        public static bool IsEmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsCpfValido(string cpf)
        {
            // Remove caracteres especiais, como pontos e hífens
            cpf = cpf.Replace(".", "").Replace("-", "");

            // Verifica se o CPF tem 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais (como "11111111111")
            bool todosDigitosIguais = true;
            for (int i = 1; i < 11 && todosDigitosIguais; i++)
            {
                if (cpf[i] != cpf[0])
                    todosDigitosIguais = false;
            }
            if (todosDigitosIguais)
                return false;

            // Calcula os dígitos verificadores do CPF
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            // Primeiro dígito verificador
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;

            // Segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            // Verifica se os dígitos calculados são iguais aos dígitos do CPF
            return cpf.EndsWith(digito);
        }
    }
}
