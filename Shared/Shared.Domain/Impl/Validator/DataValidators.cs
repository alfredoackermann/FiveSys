﻿using System;
using System.Globalization;
using FluentValidation;

namespace FileSys.Shared.Core.Domain.Impl.Validator
{
    public static class DataValidators
    {
        public static IRuleBuilderOptions<T, string> DataValida<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => DateTime.TryParseExact(x, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)).
                WithMessage(Textos.Geral_Mensagem_Erro_DataInvalida);
        }
    }
}
