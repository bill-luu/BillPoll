import Image from 'next/image'
import PollsComponent from './component'

export default function Home({polls}) {
  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-24">
      <PollsComponent></PollsComponent>
    </main>
  )
}
