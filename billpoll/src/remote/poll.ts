import { PollCreate } from "../interfaces/interfaces";

const SERVER_URL = "http://localhost:5295";

export const GetPolls = async (): Promise<any> => {
  const res = await fetch(SERVER_URL+"/poll", { cache: "no-store" });
  if (!res.ok) {
            throw new Error(
              `Could not get response: ${res.status} ${res.statusText}`
            );
  }

  return res.json()
};


export const CreatePoll = async(name: string, options: string[]) : Promise<any> => {
    let toCreate: PollCreate = { name: name, options: [] };
    options.forEach((option) => {
        toCreate.options.push({ name: option });
    });

    const res = await fetch(SERVER_URL + "/poll", {
      method: "POST",
      body: JSON.stringify(toCreate),
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!res.ok) {
        throw new Error(
          `Failed to create poll: ${res.status} ${res.statusText}`
        );
    }

     return res.json();
}