import { useEffect, useState } from 'react'
import { ILeaderboardResponse } from '../../services/interfaces/Responses/ILeaderboardResponse'
import { getLeaderboard } from '../../services/LeaderboardService'

interface Props {
	leaderboard: ILeaderboardResponse[] | null
}

export function LeaderboardPage({ leaderboard }: Props) {
	const [data, setData] = useState<ILeaderboardResponse[] | null>(leaderboard)

	useEffect(() => {
		async function fetchData() {
			setData((await getLeaderboard()))
		}
		fetchData()
	}, [])

	return (
		<>
			<h1 className='text-white mb-10'>Leaderboard</h1>

			{data && data.map((item: ILeaderboardResponse, index: number) => (
				<div key={index} className='relative flex justify-between items-end border-b border-gray-500 mb-5'>
					<div className='absolute left-0 text-green-500 p-2'>#{item.place}</div>
					<div className='ml-[40px] text-white p-2 font-normal'>{item.userName}</div>
					<div className='ml-auto text-green-300 p-2'>{item.points}</div>
				</div>
			))}

		</>
	)
}