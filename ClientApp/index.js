/**
 * socket is a an object that will be used to establish a connection with the server using WebSocket.
 */
const socket = new WebSocket('ws://localhost:8085');
const messagesDiv = document.getElementById('messages');

/**
 * This is opening a connection to the server using socket object.
 */
socket.onopen = () => {
    console.log('Connected to WebSocket server');
    appendMessage('Connected to server');
};

/**
 * @summary This is triggered when any messages are sent from the server.
 * @param event It is an event that is coming from the server.
 */
socket.onmessage = (event) => {
    console.log('Message from server:', event.data);
    appendMessage(`Server: ${event.data}`);
};

/**
 * This method detects whether the socket is open or not.
 */
socket.onclose = () => {
    console.log('Disconnected from WebSocket server');
    appendMessage('Disconnected from server');
};


const messageInput = document.getElementById('messageInput');
messageInput.addEventListener("keypress", function(event){
    // If the user presses the "Enter" key on the keyboard
  if (event.key === "Enter") {
    // Cancel the default action, if needed
    event.preventDefault();
    // Trigger the button element with a click
    document.getElementById("btnSend").click();
  }
});

/**
 * @summary This method takes the message from the UI and sends it to the socket
 */
function sendMessage() {
    const input = document.getElementById('messageInput');
    const message = input.value;
    socket.send(message);
    appendMessage(`You: ${message}`);
    input.value = '';
}

/**
 * @summary This method takes message input and append it to the UI element and renders the message.
 * @param message It is the message that is to be displayed in the UI 
 */
function appendMessage(message) {
    const p = document.createElement('p');
    p.textContent = message;
    messagesDiv.appendChild(p);
}