using FluentValidation;

namespace MyTodoCleanApp.Application.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty().WithMessage("Sarlavha bo'sh bo'lishi mumkin emas.")
            .MinimumLength(3).WithMessage("Sarlavha kamida 3 ta belgidan iborat bo'lishi kerak.");

        RuleFor(v => v.Note)
            .MaximumLength(500).WithMessage("Eslatma 500 ta belgidan oshmasligi kerak.");

        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("Foydalanuvchi ID-si majburiy.");
    }
}