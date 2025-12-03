import { jsx as _jsx } from "react/jsx-runtime";
import { useState, useEffect } from 'react';
import ChatWidget from './components/ChatWidget';
function App() {
    const [tenantId, setTenantId] = useState(null);
    useEffect(() => {
        // Get tenantId from URL query params
        const params = new URLSearchParams(window.location.search);
        const tid = params.get('tenantId');
        if (tid) {
            setTenantId(tid);
        }
        else {
            console.error("Tenant ID not found in URL");
        }
    }, []);
    if (!tenantId) {
        return null; // Don't render anything if no tenantId
    }
    return (_jsx(ChatWidget, { chatbotId: tenantId }));
}
export default App;
