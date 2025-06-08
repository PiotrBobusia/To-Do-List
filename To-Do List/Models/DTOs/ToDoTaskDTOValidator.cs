using System.Reflection.Metadata.Ecma335;
using FluentValidation;

namespace To_Do_List.Models.DTOs
{
    public class ToDoTaskDTOValidator : AbstractValidator<ToDoTaskDTO>
    {
        public ToDoTaskDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("The title cannot be empty.");

            RuleFor(x => x.Date).Must(dateValidation).WithMessage("The date cannot be in the past.");
        }

        private bool dateValidation(DateOnly date)
        {
            return date >= DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
