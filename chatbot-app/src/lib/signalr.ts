import * as signalR from "@microsoft/signalr";

export class SignalRService {
    private connection: signalR.HubConnection;
    private callbacks: any = {};

    constructor(tenantId: string, language: string = 'en') {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`https://localhost:7157/hattieHub?tenantId=${tenantId}&language=${language}`)
            .withAutomaticReconnect()
            .build();

        this.registerHandlers();
    }

    private registerHandlers() {
        this.connection.on("ReceiveSessionId", (sessionId: string) => {
            if (this.callbacks.onSessionId) this.callbacks.onSessionId(sessionId);
        });

        this.connection.on("ReceiveMessageStart", () => {
            if (this.callbacks.onMessageStart) this.callbacks.onMessageStart();
        });

        this.connection.on("ReceiveMessageChunk", (chunk: string) => {
            if (this.callbacks.onMessageChunk) this.callbacks.onMessageChunk(chunk);
        });

        this.connection.on("ReceiveMessageEnd", () => {
            if (this.callbacks.onMessageEnd) this.callbacks.onMessageEnd();
        });

        this.connection.on("ReceiveError", (error: string) => {
            console.error("SignalR Error:", error);
            if (this.callbacks.onError) this.callbacks.onError(error);
        });
    }

    public setCallbacks(callbacks: any) {
        this.callbacks = callbacks;
    }

    public async start() {
        try {
            await this.connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.error("SignalR Connection Error: ", err);
            setTimeout(() => this.start(), 5000);
        }
    }

    public async sendMessage(message: string, sessionId: string | null) {
        try {
            await this.connection.invoke("SendMessage", message, sessionId);
        } catch (err) {
            console.error("SendMessage Error: ", err);
        }
    }
}
