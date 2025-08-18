using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace Cephalopod.Client.Pages;

public static class EditContextFluentValidationExtensions
{
    public static EditContext AddFluentValidation(this EditContext editContext, IServiceProvider serviceProvider)
    {
        if (editContext == null)
        {
            throw new ArgumentNullException(nameof(editContext));
        }

        var messages = new ValidationMessageStore(editContext);

        editContext.OnValidationRequested += (sender, eventArgs) =>
            ValidateModel((EditContext)sender!, messages, serviceProvider);

        editContext.OnFieldChanged += (sender, eventArgs) =>
            ValidateField(editContext, messages, eventArgs.FieldIdentifier, serviceProvider);

        return editContext;
    }

    private static void ValidateModel(EditContext editContext,
        ValidationMessageStore messages, IServiceProvider serviceProvider)
    {
        var validator = serviceProvider.GetService(typeof(IValidator<>)
            .MakeGenericType(editContext.Model.GetType())) as IValidator;

        if (validator == null) return;

        var context = new ValidationContext<object>(editContext.Model);
        var result = validator.Validate(context);

        messages.Clear();
        foreach (var error in result.Errors)
        {
            messages.Add(editContext.Field(error.PropertyName), error.ErrorMessage);
        }

        editContext.NotifyValidationStateChanged();
    }

    private static void ValidateField(EditContext editContext,
        ValidationMessageStore messages, FieldIdentifier fieldIdentifier,
        IServiceProvider serviceProvider)
    {
        var validator = serviceProvider.GetService(typeof(IValidator<>)
            .MakeGenericType(editContext.Model.GetType())) as IValidator;

        if (validator == null) return;

        var context = new ValidationContext<object>(editContext.Model);
        var result = validator.Validate(context);

        messages.Clear(fieldIdentifier);
        foreach (var error in result.Errors
                     .Where(e => e.PropertyName == fieldIdentifier.FieldName))
        {
            messages.Add(fieldIdentifier, error.ErrorMessage);
        }

        editContext.NotifyValidationStateChanged();
    }
}