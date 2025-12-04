import { useState, useEffect } from 'react';

import ChatInterface from './chat/ChatInterface';

interface ChatWidgetProps {
    chatbotId: string;
}

export default function ChatWidget({ chatbotId }: ChatWidgetProps) {
    const [isOpen, setIsOpen] = useState(false);
    const [mounted, setMounted] = useState(false);
    const [language, setLanguage] = useState<'en' | 'ar'>('en');

    useEffect(() => {
        setMounted(true);
    }, []);

    if (!mounted) return null;

    return (
        <>
            {/* Floating Button */}
            {!isOpen && (
                <button
                    className="hattie-floating-btn"
                    onClick={() => setIsOpen(true)}
                    aria-label="Open Chat"
                >
                    <span className="hattie-btn-text">Ask Hattie</span>
                    <div className="hattie-btn-icon">
                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M20 2H4C2.9 2 2 2.9 2 4V22L6 18H20C21.1 18 22 17.1 22 16V4C22 2.9 21.1 2 20 2Z" fill="currentColor" />
                        </svg>
                    </div>
                </button>
            )}

            {/* Chat Window */}
            <div className="hattie-widget-popup open" style={{ display: isOpen ? 'flex' : 'none' }}>
                {/* Header */}
                <div className="hattie-header">
                    <div className="hattie-logo-center">
                        <img src="/hattie.png" alt="Hattie AI" />
                        <span className="hattie-title">Hattie AI</span>
                    </div>

                    <div className="hattie-header-controls">
                        <button
                            onClick={() => setIsOpen(false)}
                            className="hattie-control-btn"
                            aria-label="Minimize"
                        >
                            —
                        </button>
                    </div>
                </div>

                {/* Language Selector */}
                <div className="hattie-language-selector">
                    <button
                        className={`hattie-lang-btn ${language === 'en' ? 'active' : ''}`}
                        onClick={() => setLanguage('en')}
                    >
                        English
                    </button>
                    <button
                        className={`hattie-lang-btn ${language === 'ar' ? 'active' : ''}`}
                        onClick={() => setLanguage('ar')}
                    >
                        العربية
                    </button>
                </div>

                {/* Chat Body */}
                <div style={{ flex: 1, overflow: 'hidden', display: 'flex', flexDirection: 'column' }}>
                    <ChatInterface chatbotId={chatbotId} language={language} />
                </div>
            </div>
        </>
    );
}
