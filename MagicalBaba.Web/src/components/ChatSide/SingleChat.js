import React from 'react';


/**
 * @param {{ text: string; }} props
 */
function SingleChat(props) {

    return (
        <div>
            {props.text}
        </div>
    );
}

export default SingleChat;