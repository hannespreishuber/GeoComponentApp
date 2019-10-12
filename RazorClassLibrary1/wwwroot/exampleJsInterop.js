// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.exampleJsFunctions = {
  showPrompt: function (message) {
    return prompt(message, 'Type anything here');
  }
};
window.ppedv = {
    GetLocation: function GetLocation(taskid) {
        console.log("GetLocation");
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                console.log(position);
                //                      double latitude,                        double longitude,                            double accuracy)

                DotNet.invokeMethodAsync('RazorClassLibrary1', 'ReceiveResponse', taskid, position.coords.latitude, position.coords.longitude, position.coords.accuracy).then(
                    data => alert(data), reason => alert(reason));
            });

        }
        else {
            return "No location";
        }
    }
};
