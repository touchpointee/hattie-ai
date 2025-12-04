import { useState, useEffect, useRef } from 'react';
import React from 'react';
import { SignalRService } from '../../lib/signalr';

interface MessageType {
    id: string;
    role: 'user' | 'assistant';
    content: string;
}

interface Props {
    chatbotId: string; // This is the tenantId
    language?: 'en' | 'ar';
}

const translations = {
    en: {
        greeting: 'Hello! I am your AI Assistant.',
        description: "",
        today: 'Today',
        placeholder: 'Message...',
        privacy: 'Privacy'
    },
    ar: {
        greeting: 'مرحباً! أنا مساعد الذكاء الاصطناعي الخاص بك.',
        description: '',
        today: 'اليوم',
        placeholder: 'رسالة...',
        privacy: 'الخصوصية'
    }
};

export default function ChatInterface({ chatbotId, language = 'en' }: Props) {
    const t = translations[language];
    const [messages, setMessages] = useState<MessageType[]>([]);
    const [input, setInput] = useState('');
    const [loading, setLoading] = useState(false);
    const [sessionId, setSessionId] = useState<string | null>(null);
    const [tenantName, setTenantName] = useState('');
    const messagesEndRef = useRef<HTMLDivElement>(null);
    const signalRRef = useRef<SignalRService | null>(null);
    const currentAiMessageRef = useRef<string>("");

    useEffect(() => {
        const apiUrl = (window as any).HattieAI?.apiUrl || import.meta.env.VITE_API_URL || 'https://hattie.touchpointe.digital';
        fetch(`${apiUrl}/api/tenants/${chatbotId}`)
            .then(res => res.json())
            .then(data => {
                if (data && data.name) {
                    setTenantName(data.name);
                }
            })
            .catch(err => console.error("Failed to fetch tenant name:", err));

        const service = new SignalRService(chatbotId, language);
        signalRRef.current = service;

        service.setCallbacks({
            onSessionId: (id: string) => {
                setSessionId(id);
                console.log("Session ID received:", id);
            },
            onMessageStart: () => {
                currentAiMessageRef.current = "";
                setLoading(false); // Stop loading spinner, start showing stream
                // Add placeholder message for AI
                setMessages(prev => [...prev, {
                    id: Date.now().toString(),
                    role: 'assistant',
                    content: ''
                }]);
            },
            onMessageChunk: (chunk: string) => {
                currentAiMessageRef.current += chunk;

                // Check for specific backend error message that might be streamed as content
                if (currentAiMessageRef.current.includes("Error calling Gemini API") ||
                    currentAiMessageRef.current.includes("Your API key was reported as leaked")) {
                    currentAiMessageRef.current = "I apologize, but I'm currently unable to process your request due to a temporary service disruption. Please try again later.";
                }

                setMessages(prev => {
                    const newMessages = [...prev];
                    const lastMsg = newMessages[newMessages.length - 1];
                    if (lastMsg && lastMsg.role === 'assistant') {
                        lastMsg.content = currentAiMessageRef.current;
                    }
                    return newMessages;
                });
            },
            onMessageEnd: () => {
                console.log("Message stream ended.");
            },
            onError: (err: string) => {
                console.error("Chat error:", err);
                setMessages(prev => [...prev, {
                    id: Date.now().toString(),
                    role: 'assistant',
                    content: "I apologize, but I'm currently unable to process your request due to a temporary service disruption. Please try again later."
                }]);
                setLoading(false);
            }
        });

        service.start();

        return () => {
            if (service && typeof service.stop === 'function') {
                service.stop();
            }
        };
    }, [chatbotId, language]);

    useEffect(() => {
        scrollToBottom();
    }, [messages, loading]);

    function scrollToBottom() {
        messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
    }

    async function handleSend() {
        if (!input.trim() || loading) return;

        const userMessage = input.trim();
        setInput('');
        setLoading(true);

        // Add user message to UI
        const userMsg: MessageType = {
            id: Date.now().toString(),
            role: 'user',
            content: userMessage,
        };
        setMessages((prev) => [...prev, userMsg]);

        // Send via SignalR
        if (signalRRef.current) {
            await signalRRef.current.sendMessage(userMessage, sessionId);
        }
    }

    function handleKeyDown(e: React.KeyboardEvent) {
        if (e.key === 'Enter' && !e.shiftKey) {
            e.preventDefault();
            handleSend();
        }
    }

    return (
        <div className="hattie-chat-container">
            {/* Messages */}
            <div className="hattie-messages-container">
                {messages.length === 0 ? (
                    <div className="hattie-welcome-screen">
                        {/* Centered Logo */}
                        <div className="hattie-centered-logo">
                            <div className="hattie-logo-circle">
                                <img
                                    src="/hattie.png"
                                    alt="Logo"
                                    style={{
                                        width: '100%',
                                        height: 'auto',
                                        objectFit: 'contain'
                                    }}
                                />
                            </div>
                        </div>

                        {/* Today Divider */}
                        <div className="hattie-today-divider">
                            <span>{t.today}</span>
                        </div>

                        {/* Welcome Message */}
                        <div className="hattie-welcome-message-container" style={{ direction: language === 'ar' ? 'rtl' : 'ltr' }}>
                            <div className="hattie-bot-avatar-small">
                                <img
                                    src="/hattie.png"
                                    alt="Bot"
                                    style={{
                                        width: '28px',
                                        height: 'auto',
                                        objectFit: 'contain'
                                    }}
                                />
                            </div>
                            <div className="hattie-welcome-content">
                                <h3 className="hattie-welcome-greeting">{t.greeting}</h3>
                                <p className="hattie-welcome-description">
                                    {tenantName ? `${tenantName} AI Assistant` : 'AI Assistant'}
                                </p>
                            </div>
                        </div>
                    </div>
                ) : (
                    <>
                        {messages.map((msg, index) => (
                            <div key={index} className={`hattie-chat-message ${msg.role}`}>
                                {msg.role === 'assistant' && (
                                    <div className="hattie-chat-avatar bot-avatar">
                                        <img src="/hattie.png" alt="Bot" />
                                    </div>
                                )}
                                <div className={`hattie-chat-bubble ${msg.role}`}>
                                    {msg.content}
                                </div>
                            </div>
                        ))}
                        {loading && (
                            <div className="hattie-chat-message assistant">
                                <div className="hattie-chat-avatar bot-avatar">
                                    <img src="/hattie.png" alt="Bot" />
                                </div>
                                <div className="hattie-chat-bubble assistant">
                                    <div className="hattie-typing-indicator">
                                        <div className="hattie-typing-dot"></div>
                                        <div className="hattie-typing-dot"></div>
                                        <div className="hattie-typing-dot"></div>
                                    </div>
                                </div>
                            </div>
                        )}
                        <div ref={messagesEndRef} />
                    </>
                )}
            </div>

            {/* Input */}
            <div className="hattie-input-container" style={{ direction: language === 'ar' ? 'rtl' : 'ltr' }}>
                <div className="hattie-input-wrapper">
                    <input
                        type="text"
                        className="hattie-message-input"
                        value={input}
                        onChange={(e) => setInput(e.target.value)}
                        onKeyDown={handleKeyDown}
                        placeholder={t.placeholder}
                        disabled={loading}
                        style={{ textAlign: language === 'ar' ? 'right' : 'left' }}
                    />
                    <button
                        className="hattie-send-button"
                        onClick={handleSend}
                        disabled={!input.trim() || loading}
                        aria-label="Send message"
                    >
                        <svg width="20" height="20" viewBox="0 0 20 20" fill="currentColor">
                            <path d="M10 3L10 17M10 3L4 9M10 3L16 9" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" fill="none" />
                        </svg>
                    </button>
                </div>
                <div className="hattie-powered-by">
                    <a href="https://touchpointe.digital/?utm_source=referral&utm_medium=hattie_ai" target="_blank" rel="noopener noreferrer">
                        Powered by Touchpointe Digital
                    </a>
                </div>
            </div>
        </div>
    );
}
