using FluentValidation;
using MegaHack.Core.Models.Input;
using System;

namespace MegaHack.Core.Validators
{
    public class CadastroValidator : AbstractValidator<CadastroInput>
    {
        public string msg = "O campo {PropertyName} precisa ser fornecido";

        public CadastroValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Cep)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.DDD)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage(msg);

            RuleFor(c => c.Tipo)
                .Must(ValidarTipo)
                .WithMessage("Tipo invalido, passar um dos seguintes valores: 'CL = CLIENTE' 'CO = COMERCIANTE' EN = ENTREGADOR");

        }

        private bool ValidarTipo(string value)
        {
            return Array.Exists(new[] { "CO", "CL", "EN" }, (modAceite => string.Equals(modAceite, value, StringComparison.InvariantCulture)));
        }
    }
}