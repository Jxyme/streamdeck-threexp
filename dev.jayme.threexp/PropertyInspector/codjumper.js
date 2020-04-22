document.addEventListener('websocketCreate', function () {
    console.log("Websocket created!");
    showHideSettings(actionInfo.payload.settings);

    websocket.addEventListener('message', function (event) {
        console.log("Got message event!");

        // Received message from Stream Deck
        var jsonObj = JSON.parse(event.data);

        if (jsonObj.event === 'sendToPropertyInspector') {
            var payload = jsonObj.payload;
            showHideSettings(payload);
        }
        else if (jsonObj.event === 'didReceiveSettings') {
            var payload = jsonObj.payload;
            showHideSettings(payload.settings);
        }
    });
});

function showHideSettings(payload) {
    console.log("Show Hide Settings Called");
    setLengthOfCycle("none");
    if (payload['keyCycle'] == 1) {
        setLengthOfCycle("");
    }
}

function setLengthOfCycle(displayValue) {
    var dvLengthOfCycleSettings = document.getElementById('dvLengthOfCycleSettings');
    dvLengthOfCycleSettings.style.display = displayValue;
}