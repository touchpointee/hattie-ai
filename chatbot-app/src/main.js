import { jsx as _jsx } from "react/jsx-runtime";
import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import './index.css';
// Auto-create root if it doesn't exist (for embedding)
let root = document.getElementById('root');
if (!root) {
    root = document.createElement('div');
    root.id = 'hattie-ai-root';
    document.body.appendChild(root);
}
ReactDOM.createRoot(root).render(_jsx(React.StrictMode, { children: _jsx(App, {}) }));
