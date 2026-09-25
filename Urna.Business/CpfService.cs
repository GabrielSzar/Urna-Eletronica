using Urna.Core.Interfaces;

namespace Urna.Business;

public class CpfService : ICpfService
{
    public bool ValidarCpf(string cpf)
    {      
        cpf = cpf.Replace(".", "").Replace("-", "");
        var primeiroPesos = new int[]{10,9,8,7,6,5,4,3,2 };
        var segundoPesos = new int[]{11,10,9,8,7,6,5,4,3,2 };
        
        if (cpf.Length != 11)
        {
            return false;
        }
        if (cpf.All(c => c == cpf[0]))
        {   // se todos os caracteres são iguais o primeiro caracter
            return false;
        }
        if (CalcDigitoVerificador(cpf, primeiroPesos) == false)
        {
            return false;
        }

        if (CalcDigitoVerificador(cpf, segundoPesos) == false)
        {
            return false;
        }

        return true;
    }
    
    private static bool CalcDigitoVerificador(string cpf, int[] pesos)
    {
        int somaResultado = 0;
        int indexDigito = pesos.Length;
        int digitoVerificador = cpf[indexDigito] - '0';
        for (int i = 0; i < indexDigito; i++)
        {
            somaResultado += pesos[i] * cpf[i] - '0';
        }
        
        int resto = somaResultado % 11;
        
        if (resto == 0 || resto == 1)
        {   // dígito verificador 
            if (digitoVerificador != 0)
            {
                return false;
            }
        }

        if (resto >= 2 && resto <= 10)
        {
            if (digitoVerificador != 11 - resto)
            {
                return false;
            }
        }

        return true;
    }
}