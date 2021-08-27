import React, { useState, useRef, useEffect } from 'react';
import ChatCollection from './ChatCollection';
import ChatTextProvider from '../GamePlay/ChatTextProvider';
import useTimeout from '../../utils/useTimeout';
import './ChatWindow.css';

/**
 * @param {{ textProvider: ChatTextProvider; incrementStageFunc:any}} props
 */
function ChatWindow(props) {

    const [chatHistory, setchatHistory] = useState([]);
    const [input, setInput] = useState('');
    const [speechSetting, setSpeechSetting] = useState(null);
    const timerInterval = 1500;
    /** @type {(arg0: boolean) => void} */
    const timerTrigger = useTimeout(ShowBabaResponse, timerInterval);

    let textProvider = props.textProvider;
    let incrementStageFunc = props.incrementStageFunc;


    function ConfigureSpeech() {
        if (speechSetting === null) {
            let speechSynth = new SpeechSynthesisUtterance();
            speechSynth.lang = "hi-IN";
            speechSynth.rate = 0.9;
            speechSynth.volume = 0.6;
            speechSynth.pitch = 0.5;
            setSpeechSetting(speechSynth);
        }
    }

    useEffect(() => {
        ConfigureSpeech();
    }, []);

    /**
     * @param {string} input
     */
    const InputChangedHandler = (input) => {
        return textProvider.ConvertUserText(input);
    }

    const onFormSubmit = e => {
        e.preventDefault();
        HandleSendMessage();
    }

    return (
        <div className="chat-window">
            <ChatCollection chatHistory={chatHistory}></ChatCollection>
            <div>
                <form onSubmit={onFormSubmit} className="user-input">
                    <input className="input-text" type="text" value={input} onChange={e => setInput(InputChangedHandler(e.target.value))}></input>
                    <button className="send-button" type="submit">Send</button>
                </form>
            </div>
        </div>
    );


    function HandleSendMessage() {
        let newStage = incrementStageFunc();
        textProvider.SetStage(newStage);
        let updatedChats = chatHistory.concat(input);
        setInput("");
        setchatHistory(updatedChats);
        timerTrigger(true);
    }

    function ShowBabaResponse() {
        let babaResponse = textProvider.GetBabaMessage();
        speakMessage(babaResponse);
        let updatedChats = chatHistory.concat(babaResponse);
        setchatHistory(updatedChats);
        incrementStageFunc();
    }

    /**
     * @param {string} message
     */
    function speakMessage(message) {
        speechSetting.text = message;
        speechSynthesis.speak(speechSetting);
    }
}

export default ChatWindow;