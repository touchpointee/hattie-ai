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
    language?: string;
}

const translations: Record<string, any> = {
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

export default function ChatInterface({ chatbotId, language = 'en' }: Props) {
    // robust fallback: exact match -> region match -> english -> hardcoded default
    const langCode = language ? language.split('-')[0] : 'en';
    const t = translations[language] || translations[langCode] || translations['en'] || {
        greeting: 'Hello! I am your AI Assistant.',
        today: 'Today',
        placeholder: 'Message...',
        privacy: 'Privacy'
    };
    const [messages, setMessages] = useState<MessageType[]>([]);
    const [input, setInput] = useState('');
    const [loading, setLoading] = useState(false);
    const [sessionId, setSessionId] = useState<string | null>(null);
    const [tenantName, setTenantName] = useState('');
    const messagesEndRef = useRef<HTMLDivElement>(null);
    const signalRRef = useRef<SignalRService | null>(null);
    const currentAiMessageRef = useRef<string>("");

    let apiUrl = (window as any).HattieAI?.apiUrl || import.meta.env.VITE_API_URL || 'https://hattie.touchpointe.digital';

    // Safety check: If in production mode but URL is localhost, force production URL
    if (import.meta.env.PROD && apiUrl.includes('localhost')) {
        apiUrl = 'https://hattie.touchpointe.digital';
    }

    const logoUrl = import.meta.env.DEV ? '/hattie.png' : `${apiUrl}/hattie.png`;

    useEffect(() => {
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
                                    src={logoUrl}
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
                        <div className="hattie-welcome-message-container" style={{ direction: ['ar', 'he', 'fa', 'ur'].includes(language) ? 'rtl' : 'ltr' }}>
                            <div className="hattie-bot-avatar-small">
                                <img
                                    src={logoUrl}
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
                                        <img src={logoUrl} alt="Bot" />
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
                                    <img src={logoUrl} alt="Bot" />
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
            <div className="hattie-input-container" style={{ direction: ['ar', 'he', 'fa', 'ur'].includes(language) ? 'rtl' : 'ltr' }}>
                <div className="hattie-input-wrapper">
                    <input
                        type="text"
                        className="hattie-message-input"
                        value={input}
                        onChange={(e) => setInput(e.target.value)}
                        onKeyDown={handleKeyDown}
                        placeholder={t.placeholder}
                        disabled={loading}
                        style={{ textAlign: ['ar', 'he', 'fa', 'ur'].includes(language) ? 'right' : 'left' }}
                    />
                    <button
                        className="hattie-send-button"
                        onClick={handleSend}
                        disabled={!input.trim() || loading}
                        aria-label="Send message"
                    >
                        <svg width="20" height="20" viewBox="0 0 20 20" fill="currentColor">
                            <path d="M22 2L11 13" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
                            <path d="M22 2L15 22L11 13L2 9L22 2Z" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
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
