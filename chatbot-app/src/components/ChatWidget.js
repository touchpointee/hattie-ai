import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { useState, useEffect } from 'react';
import { createPortal } from 'react-dom';
import ChatInterface from './chat/ChatInterface';
export default function ChatWidget({ chatbotId }) {
    const [isMinimized, setIsMinimized] = useState(true); // Start minimized
    const [mounted, setMounted] = useState(false);
    const [language, setLanguage] = useState('en');
    useEffect(() => {
        setMounted(true);
    }, []);
    if (!mounted)
        return null;
    return createPortal(_jsxs("div", { className: `chatbot-widget-popup ${isMinimized ? 'minimized' : ''}`, children: [_jsxs("div", { className: "chatbot-header", onClick: () => setIsMinimized(!isMinimized), style: { cursor: 'pointer' }, children: [_jsx("div", { className: "chatbot-logo-center", children: _jsx("img", { src: "/raco.png", alt: "Hattie AI" }) }), _jsx("div", { className: "chatbot-header-controls", children: _jsx("button", { onClick: (e) => { e.stopPropagation(); setIsMinimized(!isMinimized); }, className: "chatbot-control-btn", "aria-label": isMinimized ? "Maximize" : "Minimize", children: isMinimized ? '+' : '—' }) })] }), !isMinimized && (_jsxs("div", { className: "language-selector", children: [_jsx("button", { className: `lang-btn ${language === 'en' ? 'active' : ''}`, onClick: () => setLanguage('en'), children: "English" }), _jsx("button", { className: `lang-btn ${language === 'ar' ? 'active' : ''}`, onClick: () => setLanguage('ar'), children: "\u0627\u0644\u0639\u0631\u0628\u064A\u0629" })] })), !isMinimized && (_jsx("div", { style: { flex: 1, overflow: 'hidden', display: 'flex', flexDirection: 'column' }, children: _jsx(ChatInterface, { chatbotId: chatbotId, language: language }) }))] }), document.body);
}
