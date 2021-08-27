import React from 'react';
import './BabaWindow.css';

/**
 * @param {{ stage: number; }} props
 */
function BabaWindow(props) {
    let currentStage = props.stage;
    let style = currentStage === 0 ? "baba-container animated" : "baba-container";
    let babaImage = currentStage === 0 ? "./babasleeping.png" : "./babawaked.png";
    return (
        <div className="baba-background">
            <div className={style}>
                <img className="baba-image" src={babaImage}></img>
            </div>
        </div>);
}

export default BabaWindow;