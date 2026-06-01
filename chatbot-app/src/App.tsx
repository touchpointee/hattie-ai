import { useState, useEffect } from 'react';
import ChatWidget from './components/ChatWidget';

function App() {
    const [tenantId, setTenantId] = useState<string | null>(null);

    useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        const scriptTenantId = document.querySelector<HTMLScriptElement>('script[data-tenant-id]')?.dataset.tenantId;
        const tid = params.get('tenantId') || window.HattieAI?.tenantId || scriptTenantId || null;

        if (tid) {
            setTenantId(tid);
        } else {
            console.error("Tenant ID not found. Provide ?tenantId=..., window.HattieAI.tenantId, or script data-tenant-id.");
        }
    }, []);

    if (!tenantId) {
        return null; // Don't render anything if no tenantId
    }

    return (
        <ChatWidget
            chatbotId={tenantId} // We use chatbotId prop to pass tenantId
        />
    );
}

export default App;
