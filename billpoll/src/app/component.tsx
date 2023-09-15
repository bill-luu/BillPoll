import React from "react"

export default async function PollsComponent() {

    const res = await fetch("http://localhost:5295/poll", {cache: 'no-store'})
    const polls = await res.json()

    return (
        <div>
            {polls?.length > 0 && (
                <ul data-test="poll-list">
                    {polls.map(poll => (
                        <li key={poll.id}> {poll.name} </li>
                    ))}
                </ul>
            )}
        </div>
    )
}