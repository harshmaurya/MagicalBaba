import Configuration from "./Configuration";

class ChatTextProvider {

    currentStage = 0;
    inputText = "";
    outputText = "";
    currentOutputPosition = 0;
    maskText = false;
    hiddenMessage = "";

    /**
     * @param {number} stage
     */
    SetStage(stage) {

        this.currentStage = stage;
        this.outputText = "";
        this.currentOutputPosition = 0;
        this.maskText = false;
        if (stage === 1) {
            this.ParseHiddenMessage();
        }
    }

    /**
     * @param {string} input
     */
    ConvertUserText(input) {

        if (this.currentStage === 0)
            return this.GetWakeupText(input);
        return input;
    }

    GetBabaMessage() {
        if (this.currentStage === 1)
            return Configuration.wakeupResponse;
        if (this.currentStage === 3) {
            if (this.hiddenMessage === "") return Configuration.defaultAnswer;
            return this.hiddenMessage;
        }
    }

    /**
     * @param {string} input
     */
    GetWakeupText(input) {

        let existingText = input.length > 1 ? input.substring(0, input.length - 1) : "";
        let newText = input.charAt(input.length - 1);
        let newTextReplacement = newText;

        this.hiddenMessage = this.hiddenMessage.concat(newText);
        if (this.currentOutputPosition === 0 && input === Configuration.hotkey) {
            this.outputText = Configuration.wakeuptext;
            this.maskText = true;
            newTextReplacement = "";
        }
        else if (this.maskText) {
            if (this.currentOutputPosition <= this.outputText.length)
                newTextReplacement = this.outputText[this.currentOutputPosition - 1];
            else
                newTextReplacement = "";
        }

        this.currentOutputPosition++;

        return existingText.concat(newTextReplacement);
    }

    ParseHiddenMessage() {
        let splitMessages = this.hiddenMessage.split(Configuration.hotkey);
        this.hiddenMessage = splitMessages.length > 1 ? splitMessages[1] : "";
    }

}

export default ChatTextProvider;

