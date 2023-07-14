namespace FLogger
{
    public class FLogger
    {
        private static string _logPath = string.Empty;
        private static string _filePath = string.Empty;
        private static string _logSeparetor = "".PadLeft(100, '=');
        /// <summary>
        /// Por padrão o nome do arquivo sempre será FLogger mas podendo ser alterado.
        /// </summary>
        public string FileName { get; set; } = "Flogger";

        public FLogger(string logPath, string filename = null)
        {
            _logPath = logPath;

            if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);

            if (!string.IsNullOrEmpty(filename))
                FileName = filename;

            var absolutPath = $"{FileName}{DateTime.Now.ToString("yyyy-MM-dd")}.log";

            _filePath = Path.Combine(_logPath, absolutPath);

            Write($"Inicializando log {DateTime.Now}");
        }

        /// <summary>
        /// Escrevo um texto no arquivo, é gerado um stream no momento e depois fechado o arquivo.
        /// </summary>
        /// <param name="lineMessage">A linha de texto que será inserida no arquivo.</param>
        public void Write(string lineMessage)
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

        /// <summary>
        /// Escrevo um Exception no arquivo, é gerado um stream no momento e depois fechado o arquivo.
        /// </summary>
        /// <param name="exception">A Exception que será inserida no arquivo.</param>
        public void Write(Exception exception)
        {
            Write($"Data: {exception.Data}");
            Write($"Erro: {exception.Message}");
            Write($"InnerException: {(exception.InnerException is null ? "Não informado." : exception.InnerException.ToString())}");
            Write($"StackTrace: {(exception.StackTrace ?? "Não informado.")}");
            Write($"Source: {exception.Source}");
            Write($"HelpLink: {exception.HelpLink}");

            Dispose();
        }

        /// <summary>
        /// Encerro o log.
        /// </summary>
        public void Dispose()
        {
            Write($"Encerramento do log {DateTime.Now}");
            Write(_logSeparetor);
        }
    }
}