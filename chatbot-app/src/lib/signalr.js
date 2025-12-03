import * as signalR from "@microsoft/signalr";
export class SignalRService {
    constructor(tenantId, language = 'en') {
        Object.defineProperty(this, "connection", {
            enumerable: true,
            configurable: true,
            writable: true,
            value: void 0
        });
        Object.defineProperty(this, "callbacks", {
            enumerable: true,
            configurable: true,
            writable: true,
            value: {}
        });
        const apiUrl = import.meta.env.VITE_API_URL || 'https://localhost:7157';
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${apiUrl}/hattieHub?tenantId=${tenantId}&language=${language}`)
            .withAutomaticReconnect()
            .build();
        this.registerHandlers();
    }
    registerHandlers() {
        this.connection.on("ReceiveSessionId", (sessionId) => {
            if (this.callbacks.onSessionId)
                this.callbacks.onSessionId(sessionId);
        });
        this.connection.on("ReceiveMessageStart", () => {
            if (this.callbacks.onMessageStart)
                this.callbacks.onMessageStart();
        });
        this.connection.on("ReceiveMessageChunk", (chunk) => {
            if (this.callbacks.onMessageChunk)
                this.callbacks.onMessageChunk(chunk);
        });
        this.connection.on("ReceiveMessageEnd", () => {
            if (this.callbacks.onMessageEnd)
                this.callbacks.onMessageEnd();
        });
        this.connection.on("ReceiveError", (error) => {
            console.error("SignalR Error:", error);
            if (this.callbacks.onError)
                this.callbacks.onError(error);
        });
    }
    setCallbacks(callbacks) {
        this.callbacks = callbacks;
    }
    async start() {
        try {
            await this.connection.start();
            console.log("SignalR Connected.");
        }
        catch (err) {
            console.error("SignalR Connection Error: ", err);
            setTimeout(() => this.start(), 5000);
        }
    }
    async sendMessage(message, sessionId) {
        try {
            await this.connection.invoke("SendMessage", message, sessionId);
        }
        catch (err) {
            console.error("SendMessage Error: ", err);
        }
    }
}
