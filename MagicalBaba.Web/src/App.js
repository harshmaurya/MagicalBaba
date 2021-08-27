import React, { useState } from 'react';
import './App.css';
import './components/BabaSide/BabaWindow';
import './components/ChatSide/ChatWindow';
import BabaWindow from './components/BabaSide/BabaWindow';
import ChatWindow from './components/ChatSide/ChatWindow';
import ChatTextProvider from './components/GamePlay/ChatTextProvider';

function App() {
  const [currentStage, setcurrentStage] = useState(0);
  const [chatTextProvider] = useState(new ChatTextProvider());

  function incrementStage() {
    var newStage = currentStage + 1;
    setcurrentStage(newStage);
    return newStage;
  }

  return (
    <div>
      <div className="split left">
        <BabaWindow stage={currentStage}></BabaWindow>
      </div>
      <div className="split right">
        <ChatWindow textProvider={chatTextProvider} incrementStageFunc={incrementStage}></ChatWindow>
      </div>
    </div>
  );
}

export default App;
