'use client'

import { useState } from 'react'
import { CreatePoll } from '../remote/poll'

function CreateNewPollComponent({handlePollSubmitted}) {
    
    const [pollName, setPollName] = useState("")
    const [options, setOptions] = useState([])

    const handleOptionNameChange = (index: number, newValue: string) => {
        const newOptions = [...options]
        newOptions[index] = newValue
        setOptions(newOptions)
    }

    const handleSubmitPollClicked = async () => {
        const filteredOptions = options.filter(option => option.trim() !== "");
        try {
            await CreatePoll(pollName, filteredOptions)
            handlePollSubmitted()
        } catch(error) {
            console.error("Could not create poll: ", error.message)
        }
    }
    
    return (
        <div data-test="create-new-poll">
            <label>
                Poll Name:
                <input poll-add-test='poll-name-input'
                value={pollName}
                onChange= { e => setPollName(e.target.value) }
                placeholder='Poll Name'/>
            </label>

            <div poll-add-test='option-input-list'>
                { options.map( (option, index) => (
                    <input
                        key={"option-name-input-" +index}
                        poll-add-test="option-name-input"
                        value={option}
                        placeholder='Option'
                        onChange={ e => handleOptionNameChange(index, e.target.value)}/>
                ))}
            </div>
            <button 
                poll-add-test='option-add'
                onClick={() => { setOptions([...options,""] )}}>
                    Add Option
            </button>
            <button
                poll-add-test='submit-poll'
                onClick={() => handleSubmitPollClicked()}>
                    Submit
            </button>
        </div>
    )
}

export default function AddPollComponent({updatePolls}) {
    const [showComponent, setShowComponent] = useState(false)
    const handlePollSubmitted = () => {
        updatePolls()
        setShowComponent(false)
    }
    return (
        <div>
            <button
                data-test="poll-add-button"
                onClick={() => { setShowComponent(true) }}>
                    Add Poll
            </button>

            { showComponent && (
                <CreateNewPollComponent
                    handlePollSubmitted={() => handlePollSubmitted()}/>
            )}
        </div>
    )
}