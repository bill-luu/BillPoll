'use client'

import { useEffect, useState } from "react"
import { GetPolls } from "../remote/poll"
import AddPollComponent from "./add-poll"
import { Poll } from "../interfaces/interfaces"

export default function PollList() {
    const UpdatePolls = async () => {
        let polls: Poll[]
        try {
            polls = await GetPolls()
            setPolls(polls)
        } catch(error) {
            console.error("Error fetching polls:", error.message)
        }
    }
    
    const [polls, setPolls] = useState<Poll[]>([])

    useEffect(() => {
        UpdatePolls()
    }, [])

    return (
        <div>
            {polls?.length > 0 && (
                <ul data-test="poll-list">
                    {polls.map(poll => (
                        <li key={poll.id}> {poll.name} </li>
                    ))}
                </ul>
            )}
            <AddPollComponent updatePolls={UpdatePolls} />
        </div>
    )
}