using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Cephalopod.Client.Pages;

public class FluentValidationValidator : ComponentBase
{
    [CascadingParameter]
    private EditContext? EditContext { get; set; }

    [Inject]
    private IServiceProvider? ServiceProvider { get; set; }

    protected override void OnInitialized()
    {
        if (EditContext == null)
        {
            throw new InvalidOperationException($"{nameof(FluentValidationValidator)} requires a cascading parameter of type {nameof(EditContext)}");
        }

        EditContext.AddFluentValidation(ServiceProvider!);
    }
}