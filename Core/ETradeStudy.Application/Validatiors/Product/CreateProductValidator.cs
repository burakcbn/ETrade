using ETradeStudy.Application.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Validatiors.Product
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {

            RuleFor(p => p.ProductName).NotEmpty().NotNull().
            WithMessage("İsim bilgisi boş geçilemez");
            RuleFor(p => p.ProductName).Length(2)
                .WithMessage("Ürün adı 2 karakterden uzun olmalı");

            RuleFor(p => p.Price).NotEmpty().NotNull().
            WithMessage("Ücret bilgisi boş geçilemez")
            .Must(p => p > 0).
            WithMessage("Ürün fiyatı en az 1 olabilir");

            RuleFor(p => p.Stock).NotEmpty().NotNull().
                WithMessage("Stock bilgisi boş olamaz").
                Must(p => p >= 0).
                WithMessage("Stock bilgisi en az 0 olabilir");
        }
    }
}
