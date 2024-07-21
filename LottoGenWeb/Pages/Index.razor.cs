using System.Net.Http.Json;
using LottoGenWeb.Models;
using System.Text.Json;
using System.Reflection;

namespace LottoGenWeb.Pages
{
    public partial class Index
    {
        private bool HideSpinner { get; set; } = true;
        private void ToggleSpinner(bool Visible)
        {
            HideSpinner = !Visible;
        }

        // data for prepopulating the child NumberGroup components
        readonly List<NumberGroup> NGs = [new NumberGroup(1, true, 1, 70, 5, 15, true, true), new NumberGroup(2, true, 1, 25, 1, 15, false, false), new NumberGroup(3, false, 1, 5, 1, 15, false, false)];
        readonly string[] colors = ["#03a9f4", "#e64a19", "#aa00ff"];
        readonly List<string> BGColors = [];
        public int NumberOfSets { get; set; } = 10;
        public int[][] Numbersets { get; set; } = [[0]]; //init with 1st array element (of parent array) to 0    // number sets returned as jagged array from api
        public int NumberSetCount { get; set; } = 0; // for display purposes
        protected override void OnInitialized() //so far, not calling async methods from here, so using synchronous method
        {
            ClearResults();
        }

        /// <summary>
        /// This sets the data retrieved from the child component(s) using the groupid-1 for the List index
        /// </summary>
        /// <param name = "n"></param>
        private void HandleGroupChange(NumberGroup n)
        {
            int index = n.GroupId - 1;
            NGs[index] = n;
        }

        protected void ClearResults()
        {
            NumberSetCount = 0;
            Numbersets = [[0]];
        }

        private async Task ProcessForm()
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
                    postData.NumberSet.Add(new Models.NumberGroupRequest()
                    { NumbersPerGroup = n.NumbersPerGroup, Min = n.MinValue, Max = n.MaxValue, Divergence = n.Divergence, OeCheck = n.CheckOEEnabled, SumCheck = n.CheckSumEnabled });
                    //the following is to create a list of background colors via numbergroup for final display
                    for (int i = 0; i < n.NumbersPerGroup; i++)
                    {
                        BGColors.Add(colors[colorIndex]);
                    }

                    colorIndex++;
                }
            }

            Numbersets = await GetNumbersets(postData);
            ToggleSpinner(false);
        }

        private async Task<int[][]> GetNumbersets(Models.Root postData)
        {
            int[][]? retval = null;
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(@"api/numbersets", postData);
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

            return retval ?? ([[0]]);
        }
    }
}