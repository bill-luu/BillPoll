
import React from "react"
import { GetPolls } from "../remote/poll"
import PollList from "../components/poll-list"
import { Poll } from "../interfaces/interfaces"

export default async function PollsComponent() {
    let polls: Poll[]
    
    try {
        polls = await GetPolls()
    } catch(error) {
        console.error("Error fetching polls:", error.message)
    }


    return (
        <PollList></PollList>
    )
}