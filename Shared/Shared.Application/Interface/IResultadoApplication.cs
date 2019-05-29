using FluentValidation.Results;

namespace FileSys.Shared.Application.Interface
{
    public interface IResultadoApplication
    {
        bool Successo { get; }
        string Mensagem { get; }
        string UrlRedirect { get; }

        IResultadoApplication ExecutadoComSuccesso();
        IResultadoApplication ExecutadoComErro(string message = null);
        IResultadoApplication ExibirMensagem(string message);
        IResultadoApplication RedirecionarPara(string url);
        IResultadoApplication Resultado(ValidationResult validate);
        object Retorno();
    }

    public interface IResultadoApplication<TData> : IResultadoApplication
    {
        TData Data { get; }

        IResultadoApplication<TData> DefinirData(TData data);
    }
}
