using System.Net.Http.Json;
using LottoGenWeb.Models;
using System.Text.Json;
using LottoGenWeb.Services;
using Microsoft.AspNetCore.Components;

namespace LottoGenWeb.Pages;

public partial class Index
{
    [Inject]
    public required IHttpClientWrapper ClientWrapper { get; set; }  // This will directly inject the service

    internal bool HideSpinner { get; set; } = true;

    internal void ToggleSpinner(bool Visible)
    {
        HideSpinner = !Visible;
    }

    internal readonly List<NumberGroup> NGs =
    [
        new NumberGroup(1, true, 1, 70, 5, 15, true, true),
        new NumberGroup(2, true, 1, 25, 1, 15, false, false),
        new NumberGroup(3, false, 1, 5, 1, 15, false, false)
    ];

    readonly string[] colors = ["#03a9f4", "#e64a19", "#aa00ff"];
    readonly List<string> BGColors = [];

    public int NumberOfSets { get; set; } = 10;
    public int[][] Numbersets { get; set; } = [[0]];
    public int NumberSetCount { get; set; } = 0;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ClearResults();
    }

    internal void HandleGroupChange(NumberGroup n)
    {
        int index = n.GroupId - 1;
        NGs[index] = n;
    }

    internal void ClearResults()
    {
        NumberSetCount = 0;
        Numbersets = [[0]];
    }

    internal async Task ProcessForm()
    {
        ToggleSpinner(true);
        ClearResults();
        Models.Root postData = new() { Sets = NumberOfSets };
        BGColors.Clear();
        int colorIndex = 0;
        foreach (NumberGroup n in NGs)
        {
            if (n.Enabled)
            {
                postData.NumberSet.Add(new Models.NumberGroupRequest
                {
                    NumbersPerGroup = n.NumbersPerGroup,
                    Min = n.MinValue,
                    Max = n.MaxValue,
                    Divergence = n.Divergence,
                    OeCheck = n.CheckOEEnabled,
                    SumCheck = n.CheckSumEnabled
                });

                for (int i = 0; i < n.NumbersPerGroup; i++)
                {
                    BGColors.Add(colors[colorIndex]);
                }

                colorIndex++;
            }
        }

        try
        {
            Numbersets = await GetNumbersets(postData);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in ProcessForm: " + ex.ToString());
        }
        finally
        {
            ToggleSpinner(false);
        }
    }

    internal async Task<int[][]> GetNumbersets(Models.Root postData)
    {
        int[][]? retval = null;
        try
        {
            HttpResponseMessage response = await ClientWrapper.PostAsJsonAsync(@"api/numbersets", postData);
            if (response.IsSuccessStatusCode)
            {
                string? responseData = await response.Content.ReadAsStringAsync();
                if (responseData != null)
                {
                    retval = JsonSerializer.Deserialize<int[][]>(responseData);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error! " + ex.ToString());
        }

        return retval ?? [[0]];
    }
}
