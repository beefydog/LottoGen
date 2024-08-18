using Microsoft.AspNetCore.Components;
using LottoGenWeb.Models;

namespace LottoGenWeb.Controls;

public partial class BlockNumberGroup
{
    [Parameter]
    public NumberGroup? Ng { get; set; }

    protected override void OnInitialized()
    {
    }

    [Parameter]
    public EventCallback<NumberGroup> OnItemChange { get; set; }

    private async Task HandleChange()
    {
        await OnItemChange.InvokeAsync(Ng);
    }
}