import React from 'react';
import SingleChat from './SingleChat';
import utils from '../../utils';

/**
 * @param {{ chatHistory: string[]; }} props
 */
function ChatCollection(props) {
    let chatHistory = props.chatHistory;
    return (
        <div>
            {utils.range(0, chatHistory.length - 1).map(chatId => (
                <div key={chatId} className="speech-bubble">
                    <SingleChat text={chatHistory[chatId]}></SingleChat>
                </div>
            ))}
        </div>
    )
}

export default ChatCollection;