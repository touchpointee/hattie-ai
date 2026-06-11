import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { useState, useEffect } from 'react';
import ChatInterface from './chat/ChatInterface';
export default function ChatWidget({ chatbotId }) {
    const [isOpen, setIsOpen] = useState(false);
    const [mounted, setMounted] = useState(false);
    const [languages, setLanguages] = useState([]);
    const [language, setLanguage] = useState('en');
    let apiUrl = window.HattieAI?.apiUrl || import.meta.env.VITE_API_URL || window.location.origin;
    // Safety check: If in production mode but URL is localhost, force production URL
    // (This is for when we build and deploy to a host that isn't the chatbot host)
    if (import.meta.env.PROD && apiUrl?.includes('localhost') && window.location.hostname !== 'localhost') {
        apiUrl = 'https://hattie.touchpointe.digital';
    }
    const logoUrl = import.meta.env.DEV && !(window.HattieAI?.apiUrl) ? '/hattie.png' : `${apiUrl}/hattie.png`;
    useEffect(() => {
        setMounted(true);
        // Fetch tenant info to get supported languages
        if (chatbotId && apiUrl) {
            fetch(`${apiUrl}/api/Tenants/${chatbotId}`)
                .then(res => res.json())
                .then(data => {
                if (data.supportedLanguages && data.supportedLanguages.length > 0) {
                    setLanguages(data.supportedLanguages);
                    // Set default language if current is not in list
                    if (!data.supportedLanguages.some((l) => l.code === language)) {
                        setLanguage(data.supportedLanguages[0].code);
                    }
                }
                else {
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
    if (!mounted)
        return null;
    return (_jsxs(_Fragment, { children: [!isOpen && (_jsxs("button", { className: "hattie-floating-btn", onClick: () => setIsOpen(true), "aria-label": "Open Chat", children: [_jsx("span", { className: "hattie-btn-text", children: "Ask Hattie" }), _jsx("div", { className: "hattie-btn-icon", children: _jsx("svg", { width: "20", height: "20", viewBox: "0 0 24 24", fill: "none", xmlns: "http://www.w3.org/2000/svg", children: _jsx("path", { d: "M20 2H4C2.9 2 2 2.9 2 4V22L6 18H20C21.1 18 22 17.1 22 16V4C22 2.9 21.1 2 20 2Z", fill: "currentColor" }) }) })] })), _jsxs("div", { className: "hattie-widget-popup open", style: { display: isOpen ? 'flex' : 'none' }, children: [_jsxs("div", { className: "hattie-header", children: [_jsxs("div", { className: "hattie-logo-center", children: [_jsx("img", { src: logoUrl, alt: "Hattie AI" }), _jsx("span", { className: "hattie-title", children: "Hattie AI" })] }), _jsx("div", { className: "hattie-header-controls", children: _jsx("button", { onClick: () => setIsOpen(false), className: "hattie-control-btn", "aria-label": "Minimize", children: "\u2014" }) })] }), _jsx("div", { className: "hattie-language-selector", children: languages.map(lang => (_jsx("button", { className: `hattie-lang-btn ${language === lang.code ? 'active' : ''}`, onClick: () => setLanguage(lang.code), children: lang.name }, lang.code))) }), _jsx("div", { style: { flex: 1, overflow: 'hidden', display: 'flex', flexDirection: 'column' }, children: _jsx(ChatInterface, { chatbotId: chatbotId, language: language }) })] })] }));
}
