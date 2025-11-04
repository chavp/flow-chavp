
import Link from 'next/link'

export default async function Page() {

  return (
    <ul>
      <li><Link href="/blog/001">001</Link></li>
      <li><Link href="/blog/002">002</Link></li>
    </ul>
  )
}