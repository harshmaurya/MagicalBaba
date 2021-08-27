// @ts-nocheck
import { useEffect, useRef } from 'react';

/**
 * @param {number} delay
 */
function useTimeout(callback, delay) {
    const savedCallback = useRef();
    const savedDelay = useRef();
    const savedTimerId = useRef();
    const savedTriggerFunc = useRef();

    // Remember the latest callback.
    useEffect(() => {
        savedCallback.current = callback;
    }, [callback]);

    // Remember the interval.
    useEffect(() => {
        savedDelay.current = delay;

        const releaseTimer = () => {
            if (savedTimerId.current != undefined)
                clearTimeout(savedTimerId.current);
        }

        const configureTimer = () => {
            let id = setTimeout(() => {
                savedCallback.current();
            }, savedDelay.current);
            savedTimerId.current = id;
        }

        const triggerFunc = (action) => {
            if (action) {
                configureTimer();
            }
            else {
                releaseTimer();
            }
        }

        if (savedTriggerFunc.current === undefined)
            savedTriggerFunc.current = triggerFunc;

    }, [delay]);

    return savedTriggerFunc.current;
}

export default useTimeout;