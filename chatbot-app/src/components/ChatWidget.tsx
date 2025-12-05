import { useState, useEffect } from 'react';

import ChatInterface from './chat/ChatInterface';

interface ChatWidgetProps {
    chatbotId: string;
}

export default function ChatWidget({ chatbotId }: ChatWidgetProps) {
    const [isOpen, setIsOpen] = useState(false);
    const [mounted, setMounted] = useState(false);
    const [languages, setLanguages] = useState<{ code: string; name: string }[]>([]);
    const [language, setLanguage] = useState<string>('en');

    let apiUrl = (window as any).HattieAI?.apiUrl || import.meta.env.VITE_API_URL || 'https://hattie.touchpointe.digital';

    // Safety check: If in production mode but URL is localhost, force production URL
    if (import.meta.env.PROD && apiUrl.includes('localhost')) {
        apiUrl = 'https://hattie.touchpointe.digital';
    }

    const logoUrl = import.meta.env.DEV ? '/hattie.png' : `${apiUrl}/hattie.png`;

    useEffect(() => {
        setMounted(true);
        // Fetch tenant info to get supported languages
        if (chatbotId) {
            fetch(`${apiUrl}/api/Tenants/${chatbotId}`)
                .then(res => res.json())
                .then(data => {
                    if (data.supportedLanguages && data.supportedLanguages.length > 0) {
                        setLanguages(data.supportedLanguages);
                        // Set default language if current is not in list
                        if (!data.supportedLanguages.some((l: any) => l.code === language)) {
                            setLanguage(data.supportedLanguages[0].code);
                        }
                    } else {
                        // Fallback
                        setLanguages([{ code: 'en', name: 'English' }]);
                    }
                })
                .catch(err => {
                    console.error("Failed to fetch tenant info:", err);
                    // Fallback
                    setLanguages([{ code: 'en', name: 'English' }]);
                });
        }
    }, [chatbotId, apiUrl]);

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
                        <img src={logoUrl} alt="Hattie AI" />
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
                    {languages.map(lang => (
                        <button
                            key={lang.code}
                            className={`hattie-lang-btn ${language === lang.code ? 'active' : ''}`}
                            onClick={() => setLanguage(lang.code)}
                        >
                            {lang.name}
                        </button>
                    ))}
                </div>

                {/* Chat Body */}
                <div style={{ flex: 1, overflow: 'hidden', display: 'flex', flexDirection: 'column' }}>
                    <ChatInterface chatbotId={chatbotId} language={language} />
                </div>
            </div>
        </>
    );
}
