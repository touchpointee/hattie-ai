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
        description: "I'm here to answer questions based on our knowledge base.",
        today: 'Today',
        placeholder: 'Message...',
        privacy: 'Privacy'
    },
    ar: {
        greeting: 'مرحباً! أنا مساعد الذكاء الاصطناعي الخاص بك.',
        description: 'أنا هنا للإجابة على الأسئلة بناءً على قاعدة معارفنا.',
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
    const messagesEndRef = useRef<HTMLDivElement>(null);
    const signalRRef = useRef<SignalRService | null>(null);
    const currentAiMessageRef = useRef<string>("");

    useEffect(() => {
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
                setMessages(prev => [...prev, {
                    id: Date.now().toString(),
                    role: 'assistant',
                    content: `Error: ${err}`
                }]);
                setLoading(false);
            }
        });

        service.start();

        return () => {
            // Cleanup connection if needed
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
        <div className="modal-chat-container">
            {/* Messages */}
            <div className="modal-messages-container">
                {messages.length === 0 ? (
                    <div className="modal-welcome-screen">
                        {/* Centered Logo */}
                        <div className="centered-logo">
                            <div className="logo-circle">
                                <img
                                    src="/raco.png"
                                    alt="Logo"
                                    style={{
                                        width: '80px',
                                        height: 'auto',
                                        objectFit: 'contain'
                                    }}
                                />
                            </div>
                        </div>

                        {/* Today Divider */}
                        <div className="today-divider">
                            <span>{t.today}</span>
                        </div>

                        {/* Welcome Message */}
                        <div className="welcome-message-container" style={{ direction: language === 'ar' ? 'rtl' : 'ltr' }}>
                            <div className="bot-avatar-small">
                                <img
                                    src="/raco.png"
                                    alt="Bot"
                                    style={{
                                        width: '28px',
                                        height: 'auto',
                                        objectFit: 'contain'
                                    }}
                                />
                            </div>
                            <div className="welcome-message-content">
                                <h3 className="welcome-greeting">{t.greeting}</h3>
                                <p className="welcome-description">
                                    {t.description}
                                </p>
                            </div>
                        </div>
                    </div>
                ) : (
                    <>
                        {messages.map((msg, index) => (
                            <div key={index} className={`chat-message ${msg.role}`}>
                                {msg.role === 'assistant' && (
                                    <div className="chat-avatar bot-avatar">
                                        <img src="/raco.png" alt="Bot" />
                                    </div>
                                )}
                                <div className={`chat-bubble ${msg.role}`}>
                                    {msg.content}
                                </div>
                            </div>
                        ))}
                        {loading && (
                            <div className="chat-message assistant">
                                <div className="chat-avatar bot-avatar">
                                    <img src="/raco.png" alt="Bot" />
                                </div>
                                <div className="chat-bubble assistant">
                                    <div className="typing-indicator">
                                        <div className="typing-dot"></div>
                                        <div className="typing-dot"></div>
                                        <div className="typing-dot"></div>
                                    </div>
                                </div>
                            </div>
                        )}
                        <div ref={messagesEndRef} />
                    </>
                )}
            </div>

            {/* Input */}
            <div className="modal-input-container" style={{ direction: language === 'ar' ? 'rtl' : 'ltr' }}>
                <div className="modal-input-wrapper">
                    <input
                        type="text"
                        className="modal-message-input"
                        value={input}
                        onChange={(e) => setInput(e.target.value)}
                        onKeyDown={handleKeyDown}
                        placeholder={t.placeholder}
                        disabled={loading}
                        style={{ textAlign: language === 'ar' ? 'right' : 'left' }}
                    />
                    <button
                        className="modal-send-button"
                        onClick={handleSend}
                        disabled={!input.trim() || loading}
                        aria-label="Send message"
                    >
                        <svg width="20" height="20" viewBox="0 0 20 20" fill="currentColor">
                            <path d="M10 3L10 17M10 3L4 9M10 3L16 9" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" fill="none" />
                        </svg>
                    </button>
                </div>

            </div>
        </div>
    );
}
