import { jsx as _jsx, jsxs as _jsxs, Fragment as _Fragment } from "react/jsx-runtime";
import { useState, useEffect } from 'react';
import { createPortal } from 'react-dom';
import ChatInterface from './chat/ChatInterface';
export default function ChatWidget({ chatbotId }) {
    const [isOpen, setIsOpen] = useState(false);
    const [mounted, setMounted] = useState(false);
    const [language, setLanguage] = useState('en');
    useEffect(() => {
        setMounted(true);
    }, []);
    if (!mounted)
        return null;
    return createPortal(_jsxs(_Fragment, { children: [!isOpen && (_jsxs("button", { className: "chatbot-floating-btn", onClick: () => setIsOpen(true), "aria-label": "Open Chat", children: [_jsx("span", { className: "chatbot-btn-text", children: "Ask Hattie" }), _jsx("div", { className: "chatbot-btn-icon", children: _jsx("svg", { width: "20", height: "20", viewBox: "0 0 24 24", fill: "none", xmlns: "http://www.w3.org/2000/svg", children: _jsx("path", { d: "M20 2H4C2.9 2 2 2.9 2 4V22L6 18H20C21.1 18 22 17.1 22 16V4C22 2.9 21.1 2 20 2Z", fill: "currentColor" }) }) })] })), _jsxs("div", { className: "chatbot-widget-popup open", style: { display: isOpen ? 'flex' : 'none' }, children: [_jsxs("div", { className: "chatbot-header", children: [_jsxs("div", { className: "chatbot-logo-center", children: [_jsx("img", { src: "/hattie.png", alt: "Hattie AI" }), _jsx("span", { className: "chatbot-title", children: "Hattie AI" })] }), _jsx("div", { className: "chatbot-header-controls", children: _jsx("button", { onClick: () => setIsOpen(false), className: "chatbot-control-btn", "aria-label": "Minimize", children: "\u2014" }) })] }), _jsxs("div", { className: "language-selector", children: [_jsx("button", { className: `lang-btn ${language === 'en' ? 'active' : ''}`, onClick: () => setLanguage('en'), children: "English" }), _jsx("button", { className: `lang-btn ${language === 'ar' ? 'active' : ''}`, onClick: () => setLanguage('ar'), children: "\u0627\u0644\u0639\u0631\u0628\u064A\u0629" })] }), _jsx("div", { style: { flex: 1, overflow: 'hidden', display: 'flex', flexDirection: 'column' }, children: _jsx(ChatInterface, { chatbotId: chatbotId, language: language }) })] })] }), document.body);
}
