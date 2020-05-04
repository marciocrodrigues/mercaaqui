using FluentValidation;
using MegaHack.Core.Models.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MegaHack.Core.Validators
{
    public class ProcessoValidator : AbstractValidator<ProcessoInput>
    {
        public string msg = "O campo {PropertyName} precisa ser fornecido";
        public ProcessoValidator()
        {
            RuleFor(p => p.ID_Cliente)
                .NotNull()
                .NotEmpty()
                .WithMessage(msg);

            RuleFor(p => p.ID_Comerciante)
                .NotNull()
                .NotEmpty()
                .WithMessage(msg);

            RuleFor(p => p.ID_Produto)
                .NotNull()
                .NotEmpty()
                .WithMessage(msg);

            RuleFor(c => c.Status)
                .NotNull()
                .NotEmpty()
                .WithMessage(msg);

            RuleFor(c => c.Quantidade)
                .NotNull()
                .NotEmpty()
                .WithMessage(msg)
                .Must(ValidarQuantidade).WithMessage("Fornecer um valor maior que zero.");
        }

        private bool ValidarQuantidade(int valor)
        {
            return valor > 0 ? true : false;
        }
    }
}
