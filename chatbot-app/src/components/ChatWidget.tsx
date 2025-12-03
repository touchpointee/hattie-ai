import { useState, useEffect } from 'react';
import { createPortal } from 'react-dom';
import ChatInterface from './chat/ChatInterface';

interface ChatWidgetProps {
    isOpen: boolean;
    onClose: () => void;
    chatbotId: string;
}

export default function ChatWidget({ isOpen, onClose, chatbotId }: ChatWidgetProps) {
    const [isMinimized, setIsMinimized] = useState(true); // Start minimized
    const [mounted, setMounted] = useState(false);
    const [language, setLanguage] = useState<'en' | 'ar'>('en');

    useEffect(() => {
        setMounted(true);
    }, []);

    if (!mounted) return null;

    return createPortal(
        <div className={`chatbot-widget-popup ${isMinimized ? 'minimized' : ''}`}>
            {/* Header */}
            <div className="chatbot-header" onClick={() => setIsMinimized(!isMinimized)} style={{ cursor: 'pointer' }}>
                <div className="chatbot-logo-center">
                    <img src="/raco.png" alt="RACO Groups" />
                </div>

                <div className="chatbot-header-controls">
                    <button
                        onClick={(e) => { e.stopPropagation(); setIsMinimized(!isMinimized); }}
                        className="chatbot-control-btn"
                        aria-label={isMinimized ? "Maximize" : "Minimize"}
                    >
                        {isMinimized ? '+' : '—'}
                    </button>
                </div>
            </div>

            {/* Language Selector */}
            {!isMinimized && (
                <div className="language-selector">
                    <button
                        className={`lang-btn ${language === 'en' ? 'active' : ''}`}
                        onClick={() => setLanguage('en')}
                    >
                        English
                    </button>
                    <button
                        className={`lang-btn ${language === 'ar' ? 'active' : ''}`}
                        onClick={() => setLanguage('ar')}
                    >
                        العربية
                    </button>
                </div>
            )}

            {/* Chat Body */}
            {!isMinimized && (
                <div style={{ flex: 1, overflow: 'hidden', display: 'flex', flexDirection: 'column' }}>
                    <ChatInterface chatbotId={chatbotId} language={language} />
                </div>
            )}
        </div>,
        document.body
    );
}
