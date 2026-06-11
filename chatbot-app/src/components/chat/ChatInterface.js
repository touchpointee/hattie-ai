import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { useState, useEffect, useRef } from 'react';
import { SignalRService } from '../../lib/signalr';
const translations = {
    en: { greeting: 'Hello! I am your AI Assistant.', today: 'Today', placeholder: 'Message...', privacy: 'Privacy' },
    ar: { greeting: 'مرحباً! أنا مساعد الذكاء الاصطناعي الخاص بك.', today: 'اليوم', placeholder: 'رسالة...', privacy: 'الخصوصية' },
    es: { greeting: '¡Hola! Soy tu asistente de IA.', today: 'Hoy', placeholder: 'Mensaje...', privacy: 'Privacidad' },
    fr: { greeting: 'Bonjour ! Je suis votre assistant IA.', today: "Aujourd'hui", placeholder: 'Message...', privacy: 'Confidentialité' },
    de: { greeting: 'Hallo! Ich bin Ihr KI-Assistent.', today: 'Heute', placeholder: 'Nachricht...', privacy: 'Datenschutz' },
    it: { greeting: 'Ciao! Sono il tuo assistente IA.', today: 'Oggi', placeholder: 'Messaggio...', privacy: 'Privacy' },
    pt: { greeting: 'Olá! Sou seu assistente de IA.', today: 'Hoje', placeholder: 'Mensagem...', privacy: 'Privacidade' },
    ru: { greeting: 'Привет! Я ваш ИИ-помощник.', today: 'Сегодня', placeholder: 'Сообщение...', privacy: 'Конфиденциальность' },
    zh: { greeting: '你好！我是你的AI助手。', today: '今天', placeholder: '消息...', privacy: '隐私' },
    ja: { greeting: 'こんにちは！私はあなたのAIアシスタントです。', today: '今日', placeholder: 'メッセージ...', privacy: 'プライバシー' },
    ko: { greeting: '안녕하세요! 저는 AI 어시스턴트입니다.', today: '오늘', placeholder: '메시지...', privacy: '개인정보' },
    hi: { greeting: 'नमस्ते! मैं आपका एआई सहायक हूँ।', today: 'आज', placeholder: 'संदेश...', privacy: 'गोपनीयता' },
    tr: { greeting: 'Merhaba! Ben AI Asistanınızım.', today: 'Bugün', placeholder: 'Mesaj...', privacy: 'Gizlilik' },
    nl: { greeting: 'Hallo! Ik ben uw AI-assistent.', today: 'Vandaag', placeholder: 'Bericht...', privacy: 'Privacy' },
    pl: { greeting: 'Cześć! Jestem Twoim asystentem AI.', today: 'Dzisiaj', placeholder: 'Wiadomość...', privacy: 'Prywatność' },
    sv: { greeting: 'Hej! Jag är din AI-assistent.', today: 'Idag', placeholder: 'Meddelande...', privacy: 'Integritet' },
    da: { greeting: 'Hej! Jeg er din AI-assistent.', today: 'I dag', placeholder: 'Besked...', privacy: 'Privatliv' },
    fi: { greeting: 'Hei! Olen tekoälyavustajasi.', today: 'Tänään', placeholder: 'Viesti...', privacy: 'Tietosuoja' },
    no: { greeting: 'Hei! Jeg er din AI-assistent.', today: 'I dag', placeholder: 'Melding...', privacy: 'Personvern' },
    cs: { greeting: 'Ahoj! Jsem váš AI asistent.', today: 'Dnes', placeholder: 'Zpráva...', privacy: 'Soukromí' },
    el: { greeting: 'Γεια σας! Είμαι o AI βοηθός σας.', today: 'Σήμερα', placeholder: 'Μήνυμα...', privacy: 'Απόρρητο' },
    hu: { greeting: 'Szia! Én vagyok az AI asszisztense.', today: 'Ma', placeholder: 'Üzenet...', privacy: 'Adatvédelem' },
    ro: { greeting: 'Salut! Sunt asistentul tău AI.', today: 'Astăzi', placeholder: 'Mesaj...', privacy: 'Confidențialitate' },
    th: { greeting: 'สวัสดี! ฉันคือผู้ช่วย AI ของคุณ', today: 'วันนี้', placeholder: 'ข้อความ...', privacy: 'ความเป็นส่วนตัว' },
    vi: { greeting: 'Xin chào! Tôi là trợ lý AI của bạn.', today: 'Hôm nay', placeholder: 'Tin nhắn...', privacy: 'Quyền riêng tư' },
    id: { greeting: 'Halo! Saya asisten AI Anda.', today: 'Hari ini', placeholder: 'Pesan...', privacy: 'Privasi' },
    ms: { greeting: 'Helo! Saya pembantu AI anda.', today: 'Hari ini', placeholder: 'Mesej...', privacy: 'Privasi' },
    he: { greeting: 'שלום! אני עוזר ה-AI שלך.', today: 'היום', placeholder: 'הודעה...', privacy: 'פרטיות' },
    fa: { greeting: 'سلام! من دستیار هوش مصنوعی شما هستم.', today: 'امروز', placeholder: 'پیام...', privacy: 'حریم خصوصی' },
    ur: { greeting: 'ہیلو! میں آپ کا AI اسسٹنٹ ہوں۔', today: 'آج', placeholder: 'پیغام...', privacy: 'رازداری' }
};
export default function ChatInterface({ chatbotId, language = 'en' }) {
    // robust fallback: exact match -> region match -> english -> hardcoded default
    const langCode = language ? language.split('-')[0] : 'en';
    const t = translations[language] || translations[langCode] || translations['en'] || {
        greeting: 'Hello! I am your AI Assistant.',
        today: 'Today',
        placeholder: 'Message...',
        privacy: 'Privacy'
    };
    const [messages, setMessages] = useState([]);
    const [input, setInput] = useState('');
    const [loading, setLoading] = useState(false);
    const [sessionId, setSessionId] = useState(null);
    const [tenantName, setTenantName] = useState('');
    const [welcomeMessage, setWelcomeMessage] = useState('');
    const messagesEndRef = useRef(null);
    const signalRRef = useRef(null);
    const currentAiMessageRef = useRef("");
    const inactivityTimeoutRef = useRef(null);
    const resetInactivityTimer = () => {
        if (inactivityTimeoutRef.current) {
            clearTimeout(inactivityTimeoutRef.current);
        }
        inactivityTimeoutRef.current = setTimeout(() => {
            console.log("Session cleared due to 10 minutes of inactivity.");
            setSessionId(null);
            setMessages([]);
        }, 10 * 60 * 1000);
    };
    let apiUrl = window.HattieAI?.apiUrl || import.meta.env.VITE_API_URL;
    // Safety check: If in production mode but URL is localhost, force production URL
    if (import.meta.env.PROD && apiUrl?.includes('localhost') && window.location.hostname !== 'localhost') {
        apiUrl = 'https://hattie.touchpointe.digital';
    }
    const logoUrl = import.meta.env.DEV ? '/hattie.png' : `${apiUrl}/hattie.png`;
    useEffect(() => {
        resetInactivityTimer();
        fetch(`${apiUrl}/api/tenants/${chatbotId}`)
            .then(res => res.json())
            .then(data => {
            if (data) {
                if (data.name)
                    setTenantName(data.name);
                if (data.welcomeMessage)
                    setWelcomeMessage(data.welcomeMessage);
            }
        })
            .catch(err => console.error("Failed to fetch tenant name:", err));
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
                resetInactivityTimer();
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
            onError: (err) => {
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
            if (inactivityTimeoutRef.current) {
                clearTimeout(inactivityTimeoutRef.current);
            }
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
            resetInactivityTimer();
            await signalRRef.current.sendMessage(userMessage, sessionId);
        }
    }
    function handleKeyDown(e) {
        if (e.key === 'Enter' && !e.shiftKey) {
            e.preventDefault();
            handleSend();
        }
    }
    return (_jsxs("div", { className: "hattie-chat-container", children: [_jsx("div", { className: "hattie-messages-container", children: messages.length === 0 ? (_jsxs("div", { className: "hattie-welcome-screen", children: [_jsx("div", { className: "hattie-centered-logo", children: _jsx("div", { className: "hattie-logo-circle", children: _jsx("img", { src: logoUrl, alt: "Logo", style: {
                                        width: '100%',
                                        height: 'auto',
                                        objectFit: 'contain'
                                    } }) }) }), _jsx("div", { className: "hattie-today-divider", children: _jsx("span", { children: t.today }) }), _jsxs("div", { className: "hattie-welcome-message-container", style: { direction: ['ar', 'he', 'fa', 'ur'].includes(language) ? 'rtl' : 'ltr' }, children: [_jsx("div", { className: "hattie-bot-avatar-small", children: _jsx("img", { src: logoUrl, alt: "Bot", style: {
                                            width: '28px',
                                            height: 'auto',
                                            objectFit: 'contain'
                                        } }) }), _jsxs("div", { className: "hattie-welcome-content", children: [_jsx("h3", { className: "hattie-welcome-greeting", children: welcomeMessage || t.greeting }), _jsx("p", { className: "hattie-welcome-description", children: tenantName ? `${tenantName} AI Assistant` : 'AI Assistant' })] })] })] })) : (_jsxs(_Fragment, { children: [messages.map((msg, index) => (_jsxs("div", { className: `hattie-chat-message ${msg.role}`, children: [msg.role === 'assistant' && (_jsx("div", { className: "hattie-chat-avatar bot-avatar", children: _jsx("img", { src: logoUrl, alt: "Bot" }) })), _jsx("div", { className: `hattie-chat-bubble ${msg.role}`, children: msg.content })] }, index))), loading && (_jsxs("div", { className: "hattie-chat-message assistant", children: [_jsx("div", { className: "hattie-chat-avatar bot-avatar", children: _jsx("img", { src: logoUrl, alt: "Bot" }) }), _jsx("div", { className: "hattie-chat-bubble assistant", children: _jsxs("div", { className: "hattie-typing-indicator", children: [_jsx("div", { className: "hattie-typing-dot" }), _jsx("div", { className: "hattie-typing-dot" }), _jsx("div", { className: "hattie-typing-dot" })] }) })] })), _jsx("div", { ref: messagesEndRef })] })) }), _jsxs("div", { className: "hattie-input-container", style: { direction: ['ar', 'he', 'fa', 'ur'].includes(language) ? 'rtl' : 'ltr' }, children: [_jsxs("div", { className: "hattie-input-wrapper", children: [_jsx("input", { type: "text", className: "hattie-message-input", value: input, onChange: (e) => setInput(e.target.value), onKeyDown: handleKeyDown, placeholder: t.placeholder, disabled: loading, style: { textAlign: ['ar', 'he', 'fa', 'ur'].includes(language) ? 'right' : 'left' } }), _jsx("button", { className: "hattie-send-button", onClick: handleSend, disabled: !input.trim() || loading, "aria-label": "Send message", children: _jsxs("svg", { width: "20", height: "20", viewBox: "0 0 20 20", fill: "currentColor", children: [_jsx("path", { d: "M22 2L11 13", stroke: "currentColor", strokeWidth: "2", strokeLinecap: "round", strokeLinejoin: "round" }), _jsx("path", { d: "M22 2L15 22L11 13L2 9L22 2Z", stroke: "currentColor", strokeWidth: "2", strokeLinecap: "round", strokeLinejoin: "round" })] }) })] }), _jsx("div", { className: "hattie-powered-by", children: _jsx("a", { href: "https://touchpointe.digital/?utm_source=referral&utm_medium=hattie_ai", target: "_blank", rel: "noopener noreferrer", children: "Powered by Touchpointe Digital" }) })] })] }));
}
