import { jsx as _jsx } from "react/jsx-runtime";
import { useState, useEffect } from 'react';
import ChatWidget from './components/ChatWidget';
function App() {
    const [tenantId, setTenantId] = useState(null);
    useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        const scriptTenantId = document.querySelector('script[data-tenant-id]')?.dataset.tenantId;
        const tid = params.get('tenantId') || window.HattieAI?.tenantId || scriptTenantId || null;
        if (tid) {
            setTenantId(tid);
        }
        else {
            console.error("Tenant ID not found. Provide ?tenantId=..., window.HattieAI.tenantId, or script data-tenant-id.");
        }
    }, []);
    if (!tenantId) {
        return null; // Don't render anything if no tenantId
    }
    return (_jsx(ChatWidget, { chatbotId: tenantId }));
}
export default App;
