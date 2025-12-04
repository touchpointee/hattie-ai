import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import ReactMarkdown from 'react-markdown';
import remarkGfm from 'remark-gfm';
export default function Message({ role, content }) {
    return (_jsxs("div", { className: `chat-message ${role}`, children: [role === 'assistant' && (_jsx("div", { className: "chat-avatar bot-avatar", children: _jsx("img", { src: "/raco.png", alt: "Bot" }) })), _jsx("div", { className: `chat-bubble ${role}`, children: _jsx(ReactMarkdown, { remarkPlugins: [remarkGfm], children: content }) }), role === 'user' && (_jsx("div", { className: "chat-avatar user-avatar", children: _jsx("svg", { width: "24", height: "24", viewBox: "0 0 24 24", fill: "currentColor", children: _jsx("path", { d: "M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z" }) }) }))] }));
}
