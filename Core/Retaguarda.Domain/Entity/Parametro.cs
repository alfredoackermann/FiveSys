using System;
using System.Globalization;
using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Parametro : EntityCrud<Parametro>
    {
        public override AbstractValidator<Parametro> Validador => new ParametroValidator();

        public string Nome { get; set; }

        public ParametroTipoEnum Tipo { get; set; }

        public string Valor { get; set; }

        public string Descricao { get; set; }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(Parametro data)
        {
            Nome = data.Nome;
            Descricao = data.Descricao;
            Valor = data.Valor;
            Tipo = data.Tipo;
        }
    }

    internal class ParametroValidator : AbstractValidator<Parametro>
    {
        public ParametroValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Valor)
                .NotNull()
                .MaximumLength(20);

            RuleFor(x => x.Valor)
                .NumeroValido()
                .When(x => x.Tipo == ParametroTipoEnum.Numero);

            RuleFor(x => x.Valor)
                .DataValida()
                .When(x => x.Tipo == ParametroTipoEnum.Data);

            RuleFor(x => x.Descricao)
                .NotNull()
                .MaximumLength(100);
        }
    }

    //public static class ParametroExtension
    //{
    //    public static double Numero(this Parametro parametro)
    //    {
    //        if (parametro.Tipo == ParametroTipoEnum.Numero && double.TryParse(parametro.Valor, out double numeroPadrao))
    //            return numeroPadrao;

    //        throw new InvalidCastException("O parâmetro não é do tipo número");
    //    }
    //    public static DateTime Data(this Parametro parametro)
    //    {
    //        var dataValida = DateTime.TryParseExact(parametro.Valor, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataPadrao);

    //        if (parametro.Tipo == ParametroTipoEnum.Data && dataValida)
    //            return dataPadrao;

    //        throw new InvalidCastException("O parâmetro não é do tipo data");
    //    }
    //}
}
