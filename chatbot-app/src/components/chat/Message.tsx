import ReactMarkdown from 'react-markdown';
import remarkGfm from 'remark-gfm';

interface Props {
    role: 'user' | 'assistant';
    content: string;
}

export default function Message({ role, content }: Props) {
    return (
        <div className={`chat-message ${role}`}>
            {role === 'assistant' && (
                <div className="chat-avatar bot-avatar">
                    <img src="/raco.png" alt="Bot" />
                </div>
            )}
            <div className={`chat-bubble ${role}`}>
                <ReactMarkdown remarkPlugins={[remarkGfm]}>
                    {content}
                </ReactMarkdown>
            </div>
            {role === 'user' && (
                <div className="chat-avatar user-avatar">
                    <svg width="24" height="24" viewBox="0 0 24 24" fill="currentColor">
                        <path d="M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z"/>
                    </svg>
                </div>
            )}
        </div>
    );
}
