using FluentValidation;
using MegaHack.Core.Models.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MegaHack.Core.Validators
{
    public class ProdutoValidator : AbstractValidator<ProdutoInput>
    {
        public string msg = "O campo {PropertyName} precisa ser fornecido";
        public ProdutoValidator()
        {
            RuleFor(p => p.ID_Comerciante)
                .NotEmpty()
                .NotNull()
                .WithMessage(msg);

            RuleFor(p => p.Descricao)
                .NotEmpty()
                .NotNull()
                .WithMessage(msg);

            RuleFor(p => p.Quantidade)
                .NotEmpty()
                .NotNull()
                .WithMessage(msg);

            RuleFor(p => p.Preco)
                .NotNull()
                .NotEmpty()
                .WithMessage(msg);
        }
    }
}
