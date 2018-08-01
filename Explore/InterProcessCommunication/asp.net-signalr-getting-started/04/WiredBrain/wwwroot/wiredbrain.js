
//, signalR.HttpTransportType.LongPolling

let connection = null;

// Force the lack of support for WebSocket
//WebSocket = undefined;
// Force the lack of support for EventSource
//EventSource = undefined;

//setupConnection = () => {
//    connection = new signalR.HubConnectionBuilder()
//        .withUrl("/coffeehub")
//        .build();

// Tell SignalR to use LongPolling.  This is second parameter
setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/coffeehub", signalR.HttpTransportType.LongPolling)
        .build();

    // Create handlers of various function calls first

    // First parameter is name of function
    // Second parameter is function that is called when function called

    connection.on("ReceiveOrderUpdate", (update) => {
            const statusDiv = document.getElementById("status");
            statusDiv.innerHTML = update;
        }
    );

    // This is a complex parameter

    connection.on("NewOrder", function(order) {
            var statusDiv = document.getElementById("status");
            statusDiv.innerHTML = "Someone ordered an " + order.product;
        }
    );

    connection.on("finished", function() {
            connection.stop();
        }
    );

    // Then Open Connection and catch errors

    connection.start()
        .catch(err => console.error(err.toString())); 
};

// Call function to start Connection as soon as page loads.
// In a production app, don't use unstructred JavaScript like this
// Use a component based library like e.g. React, Angular, or View

setupConnection();

// AJAX request to controller when Submit button is clicked.

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    const product = document.getElementById("product").value;
    const size = document.getElementById("size").value;

    fetch("/Coffee",
        {
            method: "POST",
            body: JSON.stringify({ product, size }),
            headers: {
               'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => connection.invoke("GetUpdateForOrder", id));
});