import { useState, useEffect } from 'react';
import ChatWidget from './components/ChatWidget';

function App() {
    const [tenantId, setTenantId] = useState<string | null>(null);
    const [isChatOpen, setIsChatOpen] = useState(false);

    useEffect(() => {
        // Get tenantId from URL query params
        const params = new URLSearchParams(window.location.search);
        const tid = params.get('tenantId');
        if (tid) {
            setTenantId(tid);
        } else {
            console.error("Tenant ID not found in URL");
        }
    }, []);

    if (!tenantId) {
        return null; // Don't render anything if no tenantId
    }

    return (
        <ChatWidget
            isOpen={true} // Always "open" in the sense that the button is visible. The widget handles its own minimized state.
            onClose={() => setIsChatOpen(false)} // This might need adjustment if ChatWidget expects to be controlled externally
            chatbotId={tenantId} // We use chatbotId prop to pass tenantId
        />
    );
}

export default App;
