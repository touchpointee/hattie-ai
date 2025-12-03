import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { useState, useEffect, useRef } from 'react';
import { SignalRService } from '../../lib/signalr';
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
export default function ChatInterface({ chatbotId, language = 'en' }) {
    const t = translations[language];
    const [messages, setMessages] = useState([]);
    const [input, setInput] = useState('');
    const [loading, setLoading] = useState(false);
    const [sessionId, setSessionId] = useState(null);
    const messagesEndRef = useRef(null);
    const signalRRef = useRef(null);
    const currentAiMessageRef = useRef("");
    useEffect(() => {
        const service = new SignalRService(chatbotId, language);
        signalRRef.current = service;
        service.setCallbacks({
            onSessionId: (id) => {
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
            onMessageChunk: (chunk) => {
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
            onError: (err) => {
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
        if (!input.trim() || loading)
            return;
        const userMessage = input.trim();
        setInput('');
        setLoading(true);
        // Add user message to UI
        const userMsg = {
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
    function handleKeyDown(e) {
        if (e.key === 'Enter' && !e.shiftKey) {
            e.preventDefault();
            handleSend();
        }
    }
    return (_jsxs("div", {
        className: "modal-chat-container", children: [_jsx("div", {
            className: "modal-messages-container", children: messages.length === 0 ? (_jsxs("div", {
                className: "modal-welcome-screen", children: [_jsx("div", {
                    className: "centered-logo", children: _jsx("div", {
                        className: "logo-circle", children: _jsx("img", {
                            src: "/hattie.png", alt: "Logo", style: {
                                width: '80px',
                                height: 'auto',
                                objectFit: 'contain'
                            }
                        })
                    })
                }), _jsx("div", { className: "today-divider", children: _jsx("span", { children: t.today }) }), _jsxs("div", {
                    className: "welcome-message-container", style: { direction: language === 'ar' ? 'rtl' : 'ltr' }, children: [_jsx("div", {
                        className: "bot-avatar-small", children: _jsx("img", {
                            src: "/hattie.png", alt: "Bot", style: {
                                width: '28px',
                                height: 'auto',
                                objectFit: 'contain'
                            }
                        })
                    }), _jsxs("div", { className: "welcome-message-content", children: [_jsx("h3", { className: "welcome-greeting", children: t.greeting }), _jsx("p", { className: "welcome-description", children: t.description })] })]
                })]
            })) : (_jsxs(_Fragment, { children: [messages.map((msg, index) => (_jsxs("div", { className: `chat-message ${msg.role}`, children: [msg.role === 'assistant' && (_jsx("div", { className: "chat-avatar bot-avatar", children: _jsx("img", { src: "/hattie.png", alt: "Bot" }) })), _jsx("div", { className: `chat-bubble ${msg.role}`, children: msg.content })] }, index))), loading && (_jsxs("div", { className: "chat-message assistant", children: [_jsx("div", { className: "chat-avatar bot-avatar", children: _jsx("img", { src: "/hattie.png", alt: "Bot" }) }), _jsx("div", { className: "chat-bubble assistant", children: _jsxs("div", { className: "typing-indicator", children: [_jsx("div", { className: "typing-dot" }), _jsx("div", { className: "typing-dot" }), _jsx("div", { className: "typing-dot" })] }) })] })), _jsx("div", { ref: messagesEndRef })] }))
        }), _jsx("div", { className: "modal-input-container", style: { direction: language === 'ar' ? 'rtl' : 'ltr' }, children: _jsxs("div", { className: "modal-input-wrapper", children: [_jsx("input", { type: "text", className: "modal-message-input", value: input, onChange: (e) => setInput(e.target.value), onKeyDown: handleKeyDown, placeholder: t.placeholder, disabled: loading, style: { textAlign: language === 'ar' ? 'right' : 'left' } }), _jsx("button", { className: "modal-send-button", onClick: handleSend, disabled: !input.trim() || loading, "aria-label": "Send message", children: _jsx("svg", { width: "20", height: "20", viewBox: "0 0 20 20", fill: "currentColor", children: _jsx("path", { d: "M10 3L10 17M10 3L4 9M10 3L16 9", stroke: "currentColor", strokeWidth: "2", strokeLinecap: "round", strokeLinejoin: "round", fill: "none" }) }) })] }) })]
    }));
}
