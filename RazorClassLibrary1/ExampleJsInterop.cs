using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorClassLibrary1
{
    public class ExampleJsInterop
    {
        static IDictionary<Guid, TaskCompletionSource<Location>> _pending =
            new Dictionary<Guid, TaskCompletionSource<Location>>();

        public async Task<Location>GetLocationAsync()
        {
            var tcs = new TaskCompletionSource<Location>();
            var taskid = Guid.NewGuid();

            _pending.Add(taskid, tcs);
            var result = await jSRuntime.InvokeAsync<Location>("ppedv.GetLocation", taskid); //finde den Task
            return await tcs.Task;
        }
        [JSInvokable]
        public static void ReceiveResponse(
         string taskid,
        decimal latitude,
        decimal longitude,
        decimal accuracy)
        {
            TaskCompletionSource<Location> pendingTask;
            var id = Guid.Parse(taskid);

            pendingTask = _pending.First(x => x.Key == id).Value;
            pendingTask.SetResult(new Location
            {
                Latitude = Convert.ToDecimal(latitude),
                Longitude = Convert.ToDecimal(longitude),
                Accuracy = Convert.ToDecimal(accuracy)
            });

        }


        private readonly IJSRuntime jSRuntime;

        public ExampleJsInterop(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }
        public static ValueTask<string> Prompt(IJSRuntime jsRuntime, string message)
        {
            // Implemented in exampleJsInterop.js
            
            return jsRuntime.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
        }
    }
}
