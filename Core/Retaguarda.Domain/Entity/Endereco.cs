using FileSys.Shared.Core.Domain.Impl.Entity;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using FluentValidation;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Entity
{
    public class Endereco : EntityCrud<Endereco>
    {
        public override AbstractValidator<Endereco> Validador => new EnderecoValidator();

        public string Localizacao { get; set; }

        public int? Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public UfEnum? Uf { get; set; }

        public string Cep { get; set; }

        public string Complemento { get; set; }

        public int? CodigoMunicipio { get; set; }

        public int? Ibge { get; set; }

        public string MetadataId { get; set; }

        public override ResultadoValidacao Validar()
        {
            return ExecutarValidacaoPadrao(this);
        }

        public override void PreencherDados(Endereco data)
        {
            Localizacao = data.Localizacao;
            Numero = data.Numero;
            Bairro = data.Bairro;
            Cidade = data.Cidade;
            Uf = data.Uf;
            Cep = data.Cep;
            Complemento = data.Complemento;
            CodigoMunicipio = data.CodigoMunicipio;
            Ibge = data.Ibge;
        }
    }

    internal class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(x => x.MetadataId)
                .NotNull();

            RuleFor(x => x.Localizacao)
                .NotNull()
                .MaximumLength(255);

            RuleFor(x => x.Numero);

            RuleFor(x => x.Bairro)
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.Cidade)
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.Cep)
                .MinimumLength(9)
                .MaximumLength(9);

            RuleFor(x => x.Complemento)
                .MaximumLength(100);
        }
    }
}
