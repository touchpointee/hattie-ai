import { jsx as _jsx } from "react/jsx-runtime";
import { useState, useEffect } from 'react';
import ChatWidget from './components/ChatWidget';
function App() {
    const [tenantId, setTenantId] = useState(null);
    useEffect(() => {
        // 1. Try to get tenantId from URL query params (for direct link)
        const params = new URLSearchParams(window.location.search);
        let tid = params.get('tenantId');
        // 2. If not in URL, check global configuration object (for embedding)
        if (!tid && window.HattieAI && window.HattieAI.tenantId) {
            tid = window.HattieAI.tenantId;
        }
        if (tid) {
            setTenantId(tid);
        }
        else {
            console.error("Tenant ID not found in URL or window.HattieAI config");
        }
    }, []);
    if (!tenantId) {
        return null; // Don't render anything if no tenantId
    }
    return (_jsx(ChatWidget, { chatbotId: tenantId }));
}
export default App;
