using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLogger
{
    public class FLogger
    {
        private static string _logPath = string.Empty;
        private static string _filePath = string.Empty;
        private static string _logSeparetor = "".PadLeft(100, '=');

        public FLogger()
        {
            _logPath = "C:\\";

            if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);

            var fileName = "teste" + DateTime.Now.ToString("yyyy-MM-dd");

            _filePath = Path.Combine(_logPath, $"{fileName}.log");

            WriteLine("Inicializando LOG: " + DateTime.Now.ToString());
            WriteLine("Caminho do arquivo: " + _filePath);
        }

        /// <summary>
        /// Escrevo um texto no arquivo, é gerado um stream no momento e depois fechamo o arquivo
        /// </summary>
        /// <param name="lineMessage">A linha de texto que será inserida no arquivo</param>
        public void WriteLine(string lineMessage)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(_filePath))
                    writer.WriteLine(lineMessage);
            }
            catch (FileNotFoundException fnfex)
            {
                //identificando um problema ao gravar o log na pasta, disparo uma exceção de acesso não autorizado
                throw new Exception("Erro ao tentar escrever no arquivo de log.", fnfex);
            }
            catch (DirectoryNotFoundException dnfex)
            {
                //identificando um problema ao gravar o log na pasta, disparo uma exceção de acesso não autorizado
                throw new Exception("Erro ao tentar escrever no arquivo de log.", dnfex);
            }
            catch (UnauthorizedAccessException uax)
            {
                //identificando um problema ao gravar o log na pasta, disparo uma exceção de acesso não autorizado
                throw new Exception("Erro ao tentar escrever no arquivo de log.", uax);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}