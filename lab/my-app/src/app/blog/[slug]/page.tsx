import Link from 'next/link'

export default async function Page(props: PageProps<'/blog/[slug]'>) {
  const { slug } = await props.params

  return (
    <div>
        <h1>Hello: {slug} </h1>
    </div>
  )
}